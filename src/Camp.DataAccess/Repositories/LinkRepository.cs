using Camp.DataAccess.Data;
using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace Camp.DataAccess.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly DataContext _context;

        public LinkRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddLinkAsync(Link link)
        {
            _context.Links.Add(link);
            await _context.SaveChangesAsync();
        }

        public async Task AddLinkCheckAsync(UserLink userLink)
        {
            _context.UserLinks.Add(userLink);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLink(Link link)
        {
            _context.Links.Update(link);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Link> GetLinkByUrl(string url)
        {
            return _context.Links
                .Where(l => l.Url == url);
        }

        public IQueryable<UserLink> GetChecksByLinkId(int linkId)
        {
            //return _context.UserLinks
            //    .Include(ul => ul.Link)
            //    .Where(ul => ul.Link.Url == url);

            return _context.UserLinks
                .Where(ul => ul.LinkId == linkId);
        }

        public async Task BeginTransactionAsync()
        {
            await _context
                .Database
                .BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context
                .Database
                .CommitTransactionAsync();
        }
    }
}
