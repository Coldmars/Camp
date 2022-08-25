using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Camp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Camp.DataAccess.Enums;
using static Camp.DataAccess.Enums.Roles;
using Camp.Common.Exceptions;

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
                .GetUsersByRole(((int)Role.Curator))
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return curatorsListDto;
        }

        public async Task<SquadsListDto> GetNotLockSquadsVerify()
        {
            var squadsListDto = new SquadsListDto();

            squadsListDto.Squads = await _userRepository
                .GetUsersByRole(((int)Role.Squad))
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

        public async Task<SquadProfilesListDto> GetSquadsByUserId(int userId)
        {
            var squadsListDto = new SquadProfilesListDto();

            squadsListDto.Squads = await _userRepository
                .GetUsersByRole((int)Role.Squad)
                .Where(s => s.ParentId == userId)
                .ProjectTo<SquadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return squadsListDto;
        }

        public async Task<VolunteerProfilesListDto> GetVolunteersByUserId(int userId)
        {
            var volunteersListDto = new VolunteerProfilesListDto();

            var squadVerifyStatus = await _userRepository
                .GetUserById(userId)
                .Select(x => x.IsVerify)
                .SingleOrDefaultAsync();

            if (!squadVerifyStatus)
                throw new ForbiddenException("Squad must be verify", "User_Verify");

            volunteersListDto.Volunteers = await _userRepository
                .GetUsersByRole((int)Role.Volunteer)
                .Where(s => s.ParentId == userId)
                .ProjectTo<VolunteerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return volunteersListDto;
        }

        public async Task Verify(int userId, 
                                 int verifiableUserId, 
                                 bool verifityFlag)
        {
            var parent = await _userRepository
                .GetUserById(userId)
                .SingleOrDefaultAsync();

            if (!parent.IsVerify)
                throw new ForbiddenException("Squad must be verify", "User_Verify");

            var parentUserRole = parent.Role.Name;

            var user = await _userRepository
                .GetUserById(verifiableUserId)
                .SingleOrDefaultAsync()
                ?? throw new NotFoundException("User not fouund", "User_Exists");

            if (user.ParentId != userId)
                throw new ForbiddenException("You may verify your entity only", "Verify_Error");

            CheckRoleCompatibility(parentUserRole, user.Role.Name);

            user.IsVerify = verifityFlag;
            await _userRepository.UpdateUserAsync(user);
        }

        private void CheckRoleCompatibility(string parentRole, string childrenRole)
        {
            if (parentRole == Role.Curator.ToString())
            {
                if (childrenRole != Role.Squad.ToString())
                    throw new ForbiddenException("You may verify only squads", "Verify_Error");
            }
            if (parentRole == Role.Squad.ToString())
            {
                if (childrenRole != Role.Volunteer.ToString())
                    throw new ForbiddenException("You may verify only volunteers", "Verify_Error");
            }
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
