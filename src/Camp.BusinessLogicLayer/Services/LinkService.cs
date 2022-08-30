using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Camp.DataAccess.Entities;
using Role = Camp.DataAccess.Enums.Roles.Role;
using Camp.Common.Exceptions;
using Camp.BusinessLogicLayer.Validation;

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
            new UrlValidation().Validate(url);

            var checkTime = DateTime.Now;

            var volunteer = await GetVerifiedVolunteer(volunteerId);
            var squadId = volunteer.ParentId;

            var link = await GetLinkOrCreateIfNull(url);
            var linkChecks = await GetCheckRecords(link.Id);
            var linkCheckDto = SetFieldsFrom(linkChecks);

            var volunteersCheckRecord = CreateCheckRecordForValunteer(volunteerId, 
                                                                      link.Id, 
                                                                      checkTime);
            var squadCheckRecord = CreateCheckRecordForSquad(squadId, 
                                                             link.Id, 
                                                             checkTime);

            await SaveCheckRecords(volunteersCheckRecord, squadCheckRecord);

            return linkCheckDto;
        }

        private async Task<User> GetVerifiedVolunteer(int id)
        {
            var volunteer = await _userRepository
                .GetUserById(id)
                .SingleOrDefaultAsync()
                ?? throw new NotFoundException("User not found", "User_Exists");

            if (volunteer.IsVerify is false)
                throw new ForbiddenException("User must be verify", "User_Verify");

            return volunteer;
        }

        private async Task<Link> GetLinkOrCreateIfNull(string url)
        {
            var link = await _linkRepository
                .GetLinkByUrl(url)
                .SingleOrDefaultAsync();

            if (link == null)
            {
                link = CreateLink(url);
                await _linkRepository.AddLinkAsync(link);
            }

            return link;
        }

        private async Task<List<UserLink>> GetCheckRecords(int linkId)
        {
            return await _linkRepository
                .GetChecksByLinkId(linkId)
                .Where(ul =>
                       ul.CheckedByRoleId == ((int)Role.Volunteer))
                .OrderByDescending(d => d.CheckDate)
                .ToListAsync();
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

        private async Task SaveCheckRecords(UserLink volunteerCheckRecord,
                                           UserLink squadCheckRecord)
        {
            await _linkRepository.BeginTransactionAsync();
            await _linkRepository.AddLinkCheckAsync(volunteerCheckRecord);
            await _linkRepository.AddLinkCheckAsync(squadCheckRecord);
            await _linkRepository.CommitTransactionAsync();
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

        private UserLink CreateCheckRecordForValunteer(int volunteerId,
                                                       int linkId,
                                                       DateTimeOffset checkTime)
        {
            return new UserLink
            {
                UserId = volunteerId,
                LinkId = linkId,
                CheckDate = checkTime,
                CheckedByRoleId = ((int)Role.Volunteer)
            };
        }

        private UserLink CreateCheckRecordForSquad(int? squadId,
                                                   int linkId,
                                                   DateTimeOffset checkTime)
        {
            return new UserLink
            {
                UserId = (int)squadId,
                LinkId = linkId,
                CheckDate = checkTime,
                CheckedByRoleId = ((int)Role.Squad)
            };
        }
    }
}
