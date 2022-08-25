using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Camp.DataAccess.Entities;
using Role = Camp.DataAccess.Enums.Roles.Role;
using Camp.Common.Exceptions;

namespace Camp.BusinessLogicLayer.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IUserRepository _userRepository;

        public LinkService(ILinkRepository linkRepository, 
                           IUserRepository userRepository)
        {
            _linkRepository = linkRepository;
            _userRepository = userRepository;
        }

        public async Task<LinkCheckDto> CheckLinkAsync(int volunteerId, string url)
        {
            var volunteer = await _userRepository
                .GetUserById(volunteerId)
                .SingleOrDefaultAsync()
                ?? throw new NotFoundException("User not found", "User_Exists");

            if (volunteer.IsVerify is false)
                throw new ForbiddenException("User must be verify", "User_Verify");

            var linkCheckDto = new LinkCheckDto();
            var link = await _linkRepository
                .GetLinkByUrl(url)
                .SingleOrDefaultAsync();

            if (link == null)
            {
                link = CreateLink(url);
                await _linkRepository.AddLinkAsync(link);
            }

            var linkChecks = await _linkRepository
                .GetChecksByLinkId(link.Id)
                .Where(ul => 
                       ul.CheckedByRoleId == ((int)Role.Volunteer))
                .OrderByDescending(d => d.CheckDate)
                .ToListAsync();

            linkCheckDto = SetFieldsFrom(linkChecks);

            var squadId = volunteer.ParentId;

            var checkTime = DateTime.Now;

            var volunteersCheckRecord = new UserLink
            {
                UserId = volunteerId,
                LinkId = link.Id,
                CheckDate = checkTime,
                CheckedByRoleId = ((int)Role.Volunteer)
            };

            var squadCheckRecord = new UserLink
            {
                UserId = (int)squadId,
                LinkId = link.Id,
                CheckDate = checkTime,
                CheckedByRoleId = ((int)Role.Squad)
            };

            await _linkRepository.BeginTransactionAsync();
            await _linkRepository.AddLinkCheckAsync(volunteersCheckRecord);
            await _linkRepository.AddLinkCheckAsync(squadCheckRecord);
            await _linkRepository.CommitTransactionAsync();

            return linkCheckDto;
        } 
        
        private LinkCheckDto SetFieldsFrom(List<UserLink> linkChecks)
        {
            var dto = new LinkCheckDto();

            dto.CheckCount = linkChecks.Count;
            if (dto.CheckCount > 0)
                dto.LastCheckAt = linkChecks.FirstOrDefault().CheckDate;
            else
                dto.LastCheckAt = null;
            dto.BlockAt = null;

            return dto;
        }

        private Link CreateLink(string url)
        {
            return new Link
            {
                Url = url,
                IsLock = false,
                LockDate = null
            };
        } 
    }
}
