using BackEnd_KanBan.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_KanBan.Sevices.UserServices;

public interface IUserServices {
    Task<Models.Response<List<User>>> GetAllAsync();

    Task<Models.Response<User>> GetByIdAsync(Guid Id);

    Task<Models.Response<User>> DeleteUserByIdAsync(Guid Id);

    Task<Models.Response<User>> NewUserAsync(UserRequests user);

    Task<Models.Response<User>> UpdateByIdAsync(UserRequests user, Guid Id);

    Task<Models.Response<User>> InactivateByIdAsync(Guid Id);
}
