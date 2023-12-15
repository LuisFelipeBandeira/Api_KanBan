using BackEnd_KanBan.Api.Models.CardModels;
using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.CardModels;
using BackEnd_KanBan.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_KanBan.Api.Sevices.CardServices;

public class CardServices : ICardServices {

    private readonly ApplicationDbContext _context;
    public CardServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Response<Card>> ChangeColumn(Guid cardId, Guid destinationColumnId) {
        var response = new Response<Card>();

        try {
            var destinationColumn = await _context.Columns.SingleOrDefaultAsync(c => c.Id == destinationColumnId);

            if (destinationColumn == null) {
                response.Sucess = false;
                response.Message = "coluna destino nao foi encontrada no banco de dados";
                response.Body = null;
                return response;
            }

            var card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);

            if (card == null) {
                response.Sucess = false;
                response.Message = "card informado nao foi encontrado no banco de dados";
                response.Body = null;
                return response;
            }

            card.ColumnId = destinationColumn.Id;
            await _context.SaveChangesAsync();
            response.Message = $"Card migrado com sucesso para a coluna: {destinationColumn.Name}.";
            response.Body = card;



        } catch (Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }

    public Task<Response<Card>> DeleteCard(Guid cardId) {
        throw new NotImplementedException();
    }

    public Task<Response<Card>> FinishCard(Guid cardId) {
        throw new NotImplementedException();
    }

    public Task<Response<Card>> GetCardById(Guid cardid) {
        throw new NotImplementedException();
    }

    public Task<Response<List<Card>>> GetCardsByColumn(Guid columnId) {
        throw new NotImplementedException();
    }

    public Task<Response<List<Card>>> GetCardsByOwner(string cardOwner) {
        throw new NotImplementedException();
    }

    public Task<Response<Card>> NewCard(Guid columnId, CardRequests card) {
        throw new NotImplementedException();
    }

    public Task<Response<Card>> SetCardOwner(Guid cardId, string cardOwner) {
        throw new NotImplementedException();
    }

    public Task<Response<Card>> UpdateCard(Guid cardId, CardRequests card) {
        throw new NotImplementedException();
    }
}
