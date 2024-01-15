using BackEnd_KanBan.Models.UserModels;

namespace BackEnd_KanBan.Api.Sevices.AuthServices;

public interface ITokenServices {
    public Task<string> GetTokenAsync(User user);
}
