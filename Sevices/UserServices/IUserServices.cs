using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.UserModels;

namespace BackEnd_KanBan.Sevices.UserServices;

public interface IUserServices {
    Task<Response<List<User>>> GetAllAsync();

    Task<Response<User>> GetByIdAsync(Guid Id);

    Task<Response<User>> DeleteByIdAsync(Guid Id);

    Task<Response<User>> NewUserAsync(UserRequests user);

    Task<Response<User>> UpdateByIdAsync(UserRequests user, Guid Id);

    Task<Response<User>> InactivateByIdAsync(Guid Id);
}
