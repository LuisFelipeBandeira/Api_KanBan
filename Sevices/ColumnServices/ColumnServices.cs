using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.ColumnModels;
using BackEnd_KanBan.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_KanBan.Sevices.ColumnServices;

public class ColumnServices : IColumnServices {

    private readonly ApplicationDbContext _context;
    public ColumnServices(ApplicationDbContext context) {
        _context = context;
    }
    public async Task<Response<Column>> DeleteColumnAsync(Guid columnId) {
        var response = new Response<Column>();

        try {
            var column = await _context.Columns.SingleOrDefaultAsync(c => c.Id == columnId);

            if (column == null) {
                response.Message = "coluna nao encontrada pelo columnId informado";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            response.Body = column;
            response.Message = "coluna deletada com sucesso";
            _context.Remove(column);
            await _context.SaveChangesAsync();

        } catch (Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<List<Column>>> GetColumnAndCardsByBoardAsync(Guid boardId) {
        var response = new Response<List<Column>>();

        try {
            var columns = await _context.Columns.Include(c => c.Cards)
                .Where(c => c.BoardId == boardId)
                .ToListAsync();

            if (columns.Count < 1) {
                response.Message = "colunas nao encontradas pelo boardId informado";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            response.Body = columns;

        } catch (Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Column>> GetColumnAndCardsByIdAsync(Guid columnId) {
        var response = new Response<Column>();

        try {
            var column = await _context.Columns.Include(c => c.Cards)
                .SingleOrDefaultAsync(c => c.Id == columnId);

            if (column == null) {
                response.Message = "coluna nao encontrada pelo columnId informado";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            response.Body = column;

        } catch (Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<List<Column>>> GetColumnByBoardAsync(Guid boardId) {
        var response = new Response<List<Column>>();

        try {
            var columns = await _context.Columns.Where(c => c.BoardId == boardId).ToListAsync();

            if (columns.Count < 1) {
                response.Message = "colunas nao encontradas pelo boardId informado";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            response.Body = columns;

        } catch (Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            response.Body = null;
        }

        return response;
    }

    public async Task<Response<Column>> GetColumnByCardAsync(Guid cardId) {
        var response = new Response<Column>();

        try {

        } catch (Exception ex) {
            response.Sucess = false;
            response.Body = null;
            response.Message = ex.Message;
        }

        return response;
    }

    public Task<Response<Column>> GetColumnByIdAsync(Guid columnId) {
        throw new NotImplementedException();
    }

    public Task<Response<Column>> InactivateColumnByIdAsync(Guid columnId) {
        throw new NotImplementedException();
    }

    public Task<Response<Column>> NewColumnAsync(ColumnRequests column, Guid boardId) {
        throw new NotImplementedException();
    }

    public Task<Response<Column>> UpdateColumnByIdAsync(ColumnRequests column, Guid Id) {
        throw new NotImplementedException();
    }
}
