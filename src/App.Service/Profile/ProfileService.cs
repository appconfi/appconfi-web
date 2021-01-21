using App.Domain;
using App.Service.Profile;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using System;
using System.Threading.Tasks;

namespace App.Service.Concretes.Profile
{
    public class ProfileService : IProfileService
    {
        readonly IRepository<User, Guid> _userRepository;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.Repository<User, Guid>();
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            Guard.IsNotNull(user, "user-not-found");

            return new UserDto
            {
                FullName = user.FullName,
                Id = user.Id
            };
        }
    }
}
