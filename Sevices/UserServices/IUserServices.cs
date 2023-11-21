using Microsoft.AspNetCore.Mvc;

namespace BackEnd_KanBan.Sevices.UserServices;

public interface IUserServices {
    Task<Models.Response<List<Models.UserModels.User>>> GetAllAsync();

    Task<Models.Response<Models.UserModels.User>> GetByIdAsync([FromRoute] Guid Id);

    Task<Models.Response<Models.UserModels.User>> DeleteByIdAsync([FromRoute] Guid Id);

    Task<Models.Response<Models.UserModels.User>> NewUserAsync([FromBody] Models.UserModels.UserRequests user);

    Task<Models.Response<Models.UserModels.User>> UpdateByIdAsync([FromRoute] Guid Id);
}
