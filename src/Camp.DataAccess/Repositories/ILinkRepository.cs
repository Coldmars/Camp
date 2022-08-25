using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public interface ILinkRepository
    {
        Task AddLinkAsync(Link link);


        Task AddLinkCheckAsync(UserLink userLink);


        IQueryable<Link> GetLinkByUrl(string url);


        IQueryable<UserLink> GetChecksByLinkId(int linkId);

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();
    }
}
