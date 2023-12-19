using BackEnd_KanBan.Api.Models.CardModels;
using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.CardModels;
using BackEnd_KanBan.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd_KanBan.Api.Sevices.CardServices;

public class CardServices : ICardServices {

    private readonly ApplicationDbContext _context;
    public CardServices(ApplicationDbContext context) {
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
            card.UpdatedAt = DateTime.Now;
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

    public async Task<Response<Card>> DeleteCard(Guid cardId) {
        var response = new Response<Card>();

        try {
            response.Body = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);

            if (response.Body == null) {
                response.Sucess = false;
                response.Message = "card informado nao existe no banco de dados";
                return response;
            }

            _context.Cards.Remove(response.Body);
            await _context.SaveChangesAsync();
            response.Message = "card deletado com sucesso";

        } catch (Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Card>> FinishCard(Guid cardId) {
        var response = new Response<Card>();

        try {
            var card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);

            if (card == null) {
                response.Sucess = false;
                response.Body = null;
                response.Message = "card informado nao existe no banco de dados";
                return response;
            }

            card.IsFinished = true;
            card.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            response.Body = card;
            response.Message = "card finalizado com sucesso";

        } catch (Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Card>> GetCardById(Guid cardId) {
        var response = new Response<Card>();

        try {
            response.Body = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);

            if (response.Body == null) {
                response.Sucess = false;
                response.Message = "card nao encontrado";
                return response;
            }

        } catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<List<Card>>> GetCardsByColumn(Guid columnId) {
        var response = new Response<List<Card>>();

        try {
            var column = await _context.Columns.SingleOrDefaultAsync(c =>  columnId == c.Id);

            if (column == null) {
                response.Sucess = false;
                response.Message = "coluna nao encontrada no banco de dados";
                response.Body = null;
                return response;
            }

            response.Body = await _context.Cards.Where(c => c.ColumnId == column.Id).ToListAsync();

            if (!response.Body.Any()) {
                response.Message = "a coluna informada nao possui nenhum card";
                response.Body = null;
                return response;
            }

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<List<Card>>> GetCardsByOwner(string cardOwner) {
        var response = new Response<List<Card>>();

        try {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == cardOwner);

            if (user == null) {
                response.Sucess = false;
                response.Message = "o usuario informado nao foi encontrado no banco de dados";
                response.Body = null;
                return response;
            }

            response.Body = await _context.Cards.Where(c => c.CardOwner == cardOwner).ToListAsync();

            if (!response.Body.Any()) {
                response.Message = "nao foi encontrado nenhum card que o usuario informado seja proprietario";
                response.Body = null;
                return response;
            }

        }catch( Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Card>> NewCard(Guid columnId, CardRequests card) {
        var response = new Response<Card>();

        try {
            var column = await _context.Columns.SingleOrDefaultAsync(c => c.Id == columnId);
            
            if (column == null) {
                response.Sucess = false;
                response.Body = null;
                response.Message = "coluna informada nao foi encontrada";
                return response;
            }

            var newCard = new Card(columnId, card.Name);


            await _context.Cards.AddAsync(newCard);
            await _context.SaveChangesAsync();
            response.Body = newCard;
            response.Message = $"card: {newCard.Name}, foi cadastrado com sucesso na coluna: {column.Name}.";

        }catch(Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Card>> SetCardOwner(Guid cardId, string cardOwner) {
        var response = new Response<Card>();

        try {
            var card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);
            if (card == null) {
                response.Sucess = false;
                response.Body = null;
                response.Message = "card informado nao foi encontrado";
                return response;
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == cardOwner);
            if (user == null) {
                response.Sucess = false;
                response.Body = null;
                response.Message = "usuario informado para ser o proprietario nao foi encontrado";
                return response;
            }

            card.CardOwner = user.Name;
            card.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            response.Body = card;
            response.Message = $"proprietario do card: {card.Name}, atualizado para o usuario: {user.Name}";

        } catch( Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Card>> UpdateCard(Guid cardId, CardRequests card) {
        var response = new Response<Card>();

        try {
            var cardDb = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);
            if (cardDb == null) {
                response.Sucess = false;
                response.Body = null;
                response.Message = "card informado nao foi encontrado";
                return response;
            }

            cardDb.Name = card.Name;
            cardDb.CardOwner = card.CardOwner;
            cardDb.ColumnId = card.ColumnId;
            cardDb.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            response.Body = cardDb;
            response.Message = "card atualizado com sucesso";

        } catch (Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
            response.Body = null;
        }

        return response;
    }
}
