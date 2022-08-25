using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Camp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Camp.DataAccess.Enums;
using static Camp.DataAccess.Enums.Roles;
using Camp.Common.Exceptions;
using Camp.DataAccess.Entities;
using Role = Camp.DataAccess.Enums.Roles.Role;

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
            var linkCheckDto = new LinkCheckDto();
            var link = await _linkRepository.GetLinkByUrl(url).SingleOrDefaultAsync();

            if (link == null)
            {
                link = new Link
                {
                    Url = url,
                    IsLock = false,
                    LockDate = null
                };

                await _linkRepository.AddLinkAsync(link);
            }

            var query = await _linkRepository.GetChecksByLinkId(link.Id)
                .Where(ul => ul.CheckedByRoleId == ((int)Role.Volunteer))
                .OrderByDescending(d => d.CheckDate)
                .ToListAsync();


            linkCheckDto.CheckCount = query.Count;
            if (linkCheckDto.CheckCount > 0)
                linkCheckDto.LastCheckAt = query.FirstOrDefault().CheckDate;
            else 
                linkCheckDto.LastCheckAt = null;
            linkCheckDto.BlockAt = link.LockDate;

            var squadId = await _userRepository
                .GetUserById(volunteerId)
                .Select(u => u.ParentId)
                .SingleOrDefaultAsync();

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
    }
}
