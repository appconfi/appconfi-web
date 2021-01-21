using App.Domain;
using App.Service.Common;
using App.SharedKernel;
using App.SharedKernel.Exceptions;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Accounts
{
    public class AccountManager : IAccountManager
    {
        readonly IRepository<User, Guid> userRepository;
        readonly IRepository<Account, Guid> accountRepository;
        readonly IEmailService emailService;
        private readonly IUrlService urlService;
        const string PrivateSecret = "AjBVgPR8wG6610uIPsmv";
        const string SeparatorSring = "<@@@>";

        public IUnitOfWork UnitOfWork { get; }

        public AccountManager(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IUrlService urlService)
        {
            UnitOfWork = unitOfWork;
            userRepository = unitOfWork.Repository<User, Guid>();
            accountRepository = unitOfWork.Repository<Account, Guid>();
            this.emailService = emailService;
            this.urlService = urlService;
        }

        public async Task<Guid> RegisterExternal(LoginExternalDto loginExternalDto)
        {
            var account = await accountRepository.FirstOrDefaultAsync(new DirectSpecification<Account>(x => x.Email == loginExternalDto.Email));
            if (account == null)
            {
                await RegisterUser(new RegisterUserDto
                {
                    Email = loginExternalDto.Email,
                    FirstName = loginExternalDto.FirstName,
                    LastName = loginExternalDto.LastName,
                    Password = Guid.NewGuid().ToString()
                });
                await UnitOfWork.SaveAsync();
                await VerifyEmail(loginExternalDto.Email);
            }
            var users = UnitOfWork.Repository<User, Guid>();
            var user = await users.FirstOrDefaultAsync(new DirectSpecification<User>(x => x.Account.Email == loginExternalDto.Email));

            if (account == null)
            {
                //Accept invitations
                await AcceptInvitation(loginExternalDto.Email, user);
            }

            return user.Id;
        }

        public async Task RegisterUser(RegisterUserDto registerDto)
        {
            Guard.IsValidEmail(registerDto.Email);
            Guard.IsFalse(await accountRepository.AnyAsync(new DirectSpecification<Account>(x => x.Email == registerDto.Email)), "This account already exists");

            var user = User.Register(registerDto.Email, registerDto.Password, registerDto.FirstName, registerDto.LastName);

            userRepository.Insert(user);

            //Accept invitations
            await AcceptInvitation(registerDto.Email, user);

            await UnitOfWork.SaveAsync();

            var token = Security.EncodeText($"{SeparatorSring}{user.Account.Email}{SeparatorSring}{DateTime.UtcNow.ToShortDateString()}{SeparatorSring}", PrivateSecret);

            await emailService.SendEmailAsync(new WelcomeEmail(user.FullName, $"{urlService.GetBaseUrl()}/account/verify?token={token}")
            {
                To = registerDto.Email
            });
        }

        async Task AcceptInvitation(string email, User user)
        {
            try
            {
                var invitations = UnitOfWork.Repository<Invitation, Guid>();
                var userInvitations = await invitations.GetAsync(Invitation.WithEmail(email), "Application.Users");
                foreach (var invitation in userInvitations)
                {
                    invitation.Application.GrantPermissionForUser(user, invitation.Permission);
                    invitations.Delete(invitation);
                }
            }
            catch
            {
            }

        }

        public async Task ForgotPassword(string email)
        {
            Guard.IsValidEmail(email);

            var user = await userRepository.FirstOrDefaultAsync(new DirectSpecification<User>(x => x.Account.Email == email), "Account");
            if (user == null)
                return;

            var codeEmail = $"{SeparatorSring}{user.Account.Email}{SeparatorSring}{SeparatorSring}{DateTime.UtcNow}";
            var secureToken = Security.EncodeText(codeEmail, PrivateSecret);

            var link = $"{urlService.GetBaseUrl()}/account/reset?token={secureToken}";
            await emailService.SendEmailAsync(new ForgotPasswordEmail(link)
            {
                To = user.Account.Email,
                Subject = "Reset your password in Appconfi"
            });
        }

        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var codeEmail = Security.DecodeText(resetPasswordDto.Token, PrivateSecret);
            var parts = codeEmail.Split(SeparatorSring).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Guard.IsTrue(parts.Length == 2, "Invalid token");

            var email = parts[0];
            var date = DateTime.Parse(parts[1]);

            Guard.IsValidEmail(email);
            Guard.GreaterOrEqualThan(date, DateTime.UtcNow.AddMinutes(-10));

            var user = await userRepository.FirstOrDefaultAsync(new DirectSpecification<User>(x => x.Account.Email == email), "Account");
            if (user == null)
                return;

            user.Account.ResetPassword(resetPasswordDto.Password);

            await UnitOfWork.SaveAsync();
        }

        public async Task Verify(string token)
        {
            Guard.IsNotNullOrEmpty(token, "invalid token");

            var codeEmail = Security.DecodeText(token, PrivateSecret);
            var parts = codeEmail.Split(SeparatorSring).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (parts.Count() != 2)
                throw new BadRequestException("invalid token");

            var email = parts[0];
            var date = parts[1];

            Guard.IsDate(date, "invalid token");
            Guard.IsValidEmail(email);

            await VerifyEmail(email);

        }

        async Task VerifyEmail(string email)
        {
            var account = await accountRepository.FirstOrDefaultAsync(new DirectSpecification<Account>(x => x.Email == email));
            if (account == null)
                return;

            account.Verify();
            await UnitOfWork.SaveAsync();

        }
    }
}
