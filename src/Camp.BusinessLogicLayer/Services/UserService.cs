using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Camp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Camp.DataAccess.Enums;

namespace Camp.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, 
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CuratorsListDto> GetCurators()
        {
            var curatorsListDto = new CuratorsListDto();

            curatorsListDto.Curators = await _userRepository
                .GetUsersByRole(((int)Roles.Role.Curator))
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return curatorsListDto;
        }

        public async Task<SquadsListDto> GetNotLockSquadsVerify()
        {
            var squadsListDto = new SquadsListDto();

            squadsListDto.Squads = await _userRepository
                .GetUsersByRole(((int)Roles.Role.Squad))
                .Where(s => !s.IsLock)
                .Where(s => (bool)s.IsVerify)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return squadsListDto;
        }

        public async Task<dynamic> GetMe(int id)
        {
            var user = await _userRepository
                .GetUserById(id)
                .SingleOrDefaultAsync();

            UserDto? parent = await GetParentDto(user.ParentId);

            var profileDto = _mapper.Map<ProfileDto>(user);
            profileDto.Parent = parent;

            return new { Profile = profileDto };
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
    }
}
