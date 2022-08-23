using AutoMapper;
using AutoMapper.QueryableExtensions;
using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.BusinessLogicLayer.Validation;
using Camp.Common.DTOs;
using Camp.Common.Exceptions;
using Camp.Common.Models;
using Camp.DataAccess.Entities;
using Camp.DataAccess.Repositories;
using Camp.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Camp.BusinessLogicLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public LoginService(IUserRepository userRepository,
                            ITokenService tokenService,
                            IPasswordHasher passwordHasher,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<UserWithTokenDto> SignUpAsync(RegisterUserModel registerUser,
                                                        int roleId)
        {
            new RegisterValidation().ValidateModel(registerUser, roleId);
            new CredentialsValidation().Validate(registerUser.Login, registerUser.Password);

            await GuardAgainstLoginAlreadyExists(registerUser.Login);

            var user = _mapper.Map<User>(registerUser);
            user.RoleId = roleId;
            user.IsVerify = false;
            user.IsLock = false;
            user.CreateDate = DateTime.Now;
            user.PasswordHash = _passwordHasher.GetHashString(registerUser.Password);

            var createdUser = await _userRepository.AddUser(user);

            return await SignInAsync(createdUser.Login, registerUser.Password);
        }

        public async Task<UserWithTokenDto> SignInAsync(string login, string password)
        {
            var passwordHash = _passwordHasher.GetHashString(password);

            var user = await _userRepository
                .GetUserByCredentials(login, passwordHash)
                .SingleOrDefaultAsync()
                ?? throw new NotFoundException("User not found", "User_Exists");

            UserDto? parent = await GetParentDto(user.ParentId);

            var profileDto = _mapper.Map<ProfileDto>(user);
            profileDto.Parent = parent;

            var token = _tokenService
                .CreateAccessToken(user.Id.ToString(), user.Name, user.Role.Name);

            var accessExpiresInDate = token.ValidTo;
            var accessExpiresInTimestamp = ((DateTimeOffset)accessExpiresInDate)
                .ToUnixTimeSeconds();

            var userWithToken = new UserWithTokenDto
            {
                AccessToken = _tokenService.WriteAccessToken(token),
                AccessExpiresIn = accessExpiresInTimestamp,
                Profile = profileDto
            };

            return userWithToken;
        }

        private async Task<UserDto?> GetParentDto(int? parentId)
        {
            if (!IsParentExists(parentId))
                return null;

            return await _userRepository
                      .GetUserById((int)parentId)
                      .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                      .SingleOrDefaultAsync();
        }

        private bool IsParentExists(int? parentId)
        {
            if (parentId is null)
                return false;
            return true;
        }

        private async Task GuardAgainstLoginAlreadyExists(string login)
        {
            var userWithSameLogin = await _userRepository
                .GetUserByLogin(login)
                .FirstOrDefaultAsync();

            if (userWithSameLogin is not null)
                throw new ValidateException("This login already exists.", "Login_Exists");
        }
    }
}
