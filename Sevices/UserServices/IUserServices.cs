using Microsoft.AspNetCore.Mvc;

namespace BackEnd_KanBan.Sevices.UserServices;

public interface IUserServices {
    Task<Models.Response<List<Models.UserModels.User>>> GetAllAsync();

    Task<Models.Response<Models.UserModels.User>> GetByIdAsync(Guid Id);

    Task<Models.Response<Models.UserModels.User>> DeleteUserByIdAsync(Guid Id);

    Task<Models.Response<Models.UserModels.User>> NewUserAsync(Models.UserModels.UserRequests user);

    Task<Models.Response<Models.UserModels.User>> UpdateByIdAsync(Guid Id);
}
