using Camp.Common.DTOs;

namespace Camp.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<CuratorsListDto> GetCurators();

        Task<SquadsListDto> GetNotLockSquadsVerify();

        Task<dynamic> GetMe(int id);
    }
}
