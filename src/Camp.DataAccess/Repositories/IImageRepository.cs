using Camp.DataAccess.Entities;

namespace Camp.DataAccess.Repositories
{
    public interface IImageRepository
    {
        Task<Image> AddImage(Image image);

        IQueryable<Image> GetImageById(int imageId);
    }
}
