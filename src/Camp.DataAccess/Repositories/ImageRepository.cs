using Camp.DataAccess.Data;
using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Image> AddImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public IQueryable<Image> GetImageById(int imageId)
        {
            return _context
                .Images
                .Where(image => image.Id == imageId);
        }
    }
}
