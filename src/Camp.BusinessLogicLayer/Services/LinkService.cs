using Camp.BusinessLogicLayer.Services.Interfaces;
using Camp.Common.DTOs;
using Camp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Camp.DataAccess.Entities;
using Role = Camp.DataAccess.Enums.Roles.Role;
using Camp.Common.Exceptions;
using Camp.BusinessLogicLayer.Validation;
using Camp.DataAccess.Enums;

namespace Camp.BusinessLogicLayer.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _links;
        private readonly IUserRepository _users;
        private readonly IReportRepository _reports;
        private readonly IImageRepository _images;

        public LinkService(ILinkRepository linkRepository, 
                           IUserRepository userRepository,
                           IReportRepository reports,
                           IImageRepository images)
        {
            _links = linkRepository;
            _users = userRepository;
            _reports = reports;
            _images = images;
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

        public async Task BlockLink(int userId,
                                    string url,
                                    string comment,
                                    TypesEnum.Type type,
                                    List<GenresEnum.Genres> genres,
                                    int imageId)
        {
            var typeId = (int)type;
            var genreIds = genres.Select(g => (int)g);
            var IsGenresInvalid = genreIds.Any(g => g == 0);

            if (typeId == 0)
                throw new ValidateException("Invalid type", "Invalid_Type");
            if (IsGenresInvalid)
                throw new ValidateException("Invalid genres", "Invalid_Genres");

            // Validate report here 

            var link = await _links
                .GetLinkByUrl(url)
                .SingleOrDefaultAsync();

            if (link is null)
                throw new ForbiddenException("You must check the link before blocking", "Link_Block");
            if (link.IsLock)
                throw new ForbiddenException("This link have already blocked", "Link_Block");

            var image = await _images
                .GetImageById(imageId)
                .SingleOrDefaultAsync();

            if (image is null)
                throw new ValidateException("Image does not exist", "Report_Validate");

            var user = GetVerifiedVolunteer(userId);

            var blockTime = DateTime.Now;

            link.IsLock = true;
            link.LockDate = blockTime;
            await _links.UpdateLink(link);

            var report = new Report
            {
                Url = url,
                UserId = userId,
                Comment = comment,
                TypeId = typeId,
                ImageId = imageId,
                Time = blockTime
            };

            await _reports.AddReport(report);

            await AddGenresToReport(genreIds, report.Id);
        }

        private async Task<User> GetVerifiedVolunteer(int id)
        {
            var volunteer = await _users
                .GetUserById(id)
                .SingleOrDefaultAsync()
                ?? throw new NotFoundException("User not found", "User_Exists");

            if (volunteer.IsVerify is false)
                throw new ForbiddenException("User must be verify", "User_Verify");

            return volunteer;
        }

        private async Task<Link> GetLinkOrCreateIfNull(string url)
        {
            var link = await _links
                .GetLinkByUrl(url)
                .SingleOrDefaultAsync();

            if (link == null)
            {
                link = CreateLink(url);
                await _links.AddLinkAsync(link);
            }

            return link;
        }

        private async Task<List<UserLink>> GetCheckRecords(int linkId)
        {
            return await _links
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
            await _links.BeginTransactionAsync();
            await _links.AddLinkCheckAsync(volunteerCheckRecord);
            await _links.AddLinkCheckAsync(squadCheckRecord);
            await _links.CommitTransactionAsync();
        }

        private async Task AddGenresToReport(IEnumerable<int> genreIds, int reportId)
        {
            foreach (var genreId in genreIds)
            {
                var reportGenre = new ReportGenre
                {
                    ReportId = reportId,
                    GenreId = genreId
                };

                await _reports.AddReportGenre(reportGenre);
            }
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
