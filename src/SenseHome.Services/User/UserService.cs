using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.User;
using SenseHome.Repositories.User;
using SenseHome.Services.UserExtension;

namespace SenseHome.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserExtensionService userExtensionService;

        public UserService(
            IUserRepository userRepository,
            IUserExtensionService userExtensionService,
            IMapper mapper) : base(mapper)
        {
            this.userRepository = userRepository;
            this.userExtensionService = userExtensionService;
        }

        public async Task<bool> AddLog(string id)
        {
            return await userRepository.AddLog(id, DateTime.Now);
        }

        public async Task<UserDto> CreateUserAsync(UserInsertDto userDto)
        {
            var existingUser = await userRepository.GetByNameOrDefaultAsync(userDto.Name);
            if(existingUser != null)
            {
                throw new BadRequestException("a user already exist with this name");
            }
            var userToCreate = mapper.Map<DomainModels.User>(userDto);
            userToCreate.Password = userExtensionService.GetUserHashedPassword(userDto.Password);
            var createdUser = await userRepository.CreateAsync(userToCreate);
            return mapper.Map<UserDto>(createdUser);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await userRepository.GetAsync(id);
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UserUpdateDto user)
        {
            var userToUpdate = await userRepository.GetOrDefaultAsync(user.Id);
            if(userToUpdate == null)
            {
                throw new NotFoundException("No user found with this id");
            }
            //checking whether user is request for update the name
            if(userToUpdate.Name != user.Name)
            {
                var isRequestUserNameExist = userRepository.GetAsQueryable()
                                                           .Where(u => u.Name == user.Name)
                                                           .Any();
                if (isRequestUserNameExist)
                {
                    throw new BadRequestException("A user already exist with this username");
                }
            }
            
            userToUpdate.Type = user.Type;
            userToUpdate.Name = user.Name;
            userToUpdate.LastModifedDate = DateTime.Now;
            var updatedUser = await userRepository.UpdateAsync(userToUpdate);
            var userDto = mapper.Map<UserDto>(updatedUser);
            return userDto;
        }
    }
}