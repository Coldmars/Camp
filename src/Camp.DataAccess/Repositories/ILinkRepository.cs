using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public interface ILinkRepository
    {
        Task AddLinkAsync(Link link);

        Task AddLinkCheckAsync(UserLink userLink);

        Task UpdateLink(Link link);

        IQueryable<Link> GetLinkByUrl(string url);

        IQueryable<UserLink> GetChecksByLinkId(int linkId);

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();
    }
}
