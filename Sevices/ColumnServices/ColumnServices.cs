using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.CardModels;
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

    public async Task<Response<List<Column>>> GetColumnsByBoardAsync(Guid boardId) {
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
            var card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);
            if (card == null) {
                response.Message = "card nao existente";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            var column = await _context.Columns.SingleOrDefaultAsync(c => c.Cards.Contains(card));
            if (column == null) {
                response.Message = "coluna nao encontrada";
                response.Body = null;
                return response;
            }

            response.Body = column;

        } catch (Exception ex) {
            response.Sucess = false;
            response.Body = null;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<Column>> GetColumnByIdAsync(Guid columnId) {
        var response = new Response<Column>();

        try {
            var column = await _context.Columns.SingleOrDefaultAsync(c => c.Id == columnId);

            if (column == null) {
                response.Message = "coluna nao encontrada";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            response.Body = column;

        } catch (Exception ex) {
            response.Sucess = false;
            response.Body = null;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<Column>> InactivateColumnByIdAsync(Guid columnId) {
        var response = new Response<Column>();

        try {
            var column = await _context.Columns.SingleOrDefaultAsync(c => c.Id == columnId);

            if ( column == null) {
                response.Message = "coluna nao encontrada";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            column.IsActive = false;
            column.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            response.Body = column;
            response.Message = "coluna inativada com sucesso";

        }catch (Exception ex) {
            response.Sucess = false;
            response.Body = null;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<List<Column>>> NewColumnsAsync(List<ColumnRequests> columns, Guid boardId) {
        var response = new Response<List<Column>>();

        try {
            var board = await _context.Boards.SingleOrDefaultAsync(b => b.Id == boardId);

            if (board == null) {
                response.Message = "board nao encontrado";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            if (columns == null || columns.Count == 0) {
                response.Message = "nenhuma coluna foi informada";
                response.Sucess = false;
                response.Body = null;
                return response;
            }

            if (columns.Count == 1) {
                var newColumn = new Column(boardId, columns.FirstOrDefault().Name);
                board.Columns.Add(newColumn);

                await _context.SaveChangesAsync();
                response.Body.Add(newColumn);
                response.Message = "coluna adicionada com sucesso ao board informado";

                return response;
            }

            var listNewColumns = new List<Column>();

            foreach (var column in columns)
            {
                var newColumn = new Column(boardId, column.Name);

                board.Columns.Add(newColumn);
                listNewColumns.Add(newColumn);
            }

            await _context.SaveChangesAsync();

            response.Message = "colunas adicionadas com sucesso ao board";
            response.Body = listNewColumns;

        }
        catch(Exception ex) {
            response.Sucess = false;
            response.Body = null;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<Column>> UpdateColumnByIdAsync(ColumnRequests column, Guid ColumnId) {
        var response = new Response<Column>();

        try {
            var columnDb = await _context.Columns.SingleOrDefaultAsync(c => c.Id == ColumnId);

            if (columnDb == null) {
                response.Sucess = false;
                response.Body = null;
                response.Message = "coluna informada nao existe no banco de dados";
                return response;
            }

            columnDb.Name = column.Name;
            columnDb.IsActive = column.IsActive;
            columnDb.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            response.Message = "coluna atualizada com sucesso";
            response.Body = columnDb;

        }catch(Exception ex) {
            response.Sucess = false;
            response.Body = null;
            response.Message = ex.Message;
        }

        return response;
    }

}
