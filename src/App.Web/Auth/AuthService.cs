using App.Domain;
using App.Service.Common;
using App.SharedKernel.Exceptions;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Auth
{
    public class AuthService : IAuthService
    {
        private const bool EMAIL_VERIFICATION_REQUIERED = false;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly JWTConfig jwtSecret;
        IRepository<User, Guid> userRepository;

        public AuthService(
            IHttpContextAccessor httpContextAccessor,
            IOptions<JWTConfig> options,
            IUnitOfWork unitOfWork)
        {
            userRepository = unitOfWork.Repository<User, Guid>();
            jwtSecret = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get current login user id 
        /// </summary>
        /// <returns></returns>
        public Guid CurrentUserId()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null)
                throw new UnauthorizedAccessException();
            var claimUserId = user.FindFirst(ClaimTypes.NameIdentifier);



            if (claimUserId == null || !Guid.TryParse(claimUserId.Value, out Guid result))
                throw new UnauthorizedAccessException();

            return result;
        }

        public async Task<User> CurrentUser(string include = null)
        {

            var userId = CurrentUserId();
            return await userRepository.FirstOrDefaultAsync(new DirectSpecification<User>(x => x.Id == userId), include);
        }

        /// <summary>
        /// Generate login token
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns></returns>
        public async Task<string> Login(string email, string password)
        {
            var user = await userRepository.FirstOrDefaultAsync(new DirectSpecification<User>(x => x.Account.Email == email));
            if (user == null || !user.Account.IsValidPassword(password))
                throw new EntityNotFoundException("Invalid combination email or password");

            Guard.IsTrue(user.Account.IsVerified, $"Verify this account. Check your email {email}");

            return BuildToken(user.FullName, user.Account.Email, user.Id.ToString(), jwtSecret.Secret, jwtSecret.Issuer);
        }

        static string BuildToken(string name, string email, string id, string jwtKey, string jwtIssuer)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, name),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("user_id", id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireLimit = TimeSpan.FromDays(100).TotalMinutes;

            var token = new JwtSecurityToken(jwtIssuer,
            jwtIssuer,
            claims,
            expires: DateTime.Now.AddMinutes(expireLimit),
            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LogingResult> ValidCredentials(string email, string password)
        {
            var user = await userRepository.FirstOrDefaultAsync(new DirectSpecification<User>(x => x.Account.Email == email), "Account");

            if (user == null || !user.Account.IsValidPassword(password))
                return new LogingResult
                {
                    ValidCredentials = false,
                    Error = "Invalid email or password"
                };
            if (!user.Account.IsVerified && EMAIL_VERIFICATION_REQUIERED)
                return new LogingResult
                {
                    ValidCredentials = false,
                    Error = "Verify your account email address"
                };

            return new LogingResult
            {
                ValidCredentials = true,
                UserId = user.Id
            };

        }
    }
}