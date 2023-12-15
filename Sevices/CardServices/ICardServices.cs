using BackEnd_KanBan.Api.Models.CardModels;
using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.CardModels;

namespace BackEnd_KanBan.Api.Sevices.CardServices;

public interface ICardServices {
    public Task<Response<Card>> GetCardById(Guid cardid);
    public Task<Response<List<Card>>> GetCardsByOwner(string cardOwner);
    public Task<Response<List<Card>>> GetCardsByColumn(Guid columnId);
    public Task<Response<Card>> FinishCard(Guid cardId);
    public Task<Response<Card>> SetCardOwner(Guid cardId, string cardOwner);
    public Task<Response<Card>> ChangeColumn(Guid cardId, Guid destinationColumnId);
    public Task<Response<Card>> UpdateCard(Guid cardId, CardRequests card);
    public Task<Response<Card>> NewCard(Guid columnId, CardRequests card);
    public Task<Response<Card>> DeleteCard(Guid cardId);
}
