using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.BoardModels;
using BackEnd_KanBan.Models.ColumnModels;
using BackEnd_KanBan.Repository;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_KanBan.Sevices.BoardServices;

public class BoardServices : IBoardServices {

    private readonly ApplicationDbContext _context;
    public BoardServices(ApplicationDbContext Context) {
        _context = Context;
    }

    public async Task<Response<Board>> DeleteBoardByIdAsync(Guid Id) {
        var response = new Response<Board>();

        try {
            response.Body = await _context.Boards.Include(b => b.Columns).SingleOrDefaultAsync(b => b.Id == Id);

            if (response.Body == null) {
                response.Message = "board informado nao foi encontrado";
                response.Sucess = false;
                return response;
            }

            _context.Remove(response.Body);
            await _context.SaveChangesAsync();

        } catch (Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<List<Board>>> GetAllBoardsAsync() {
        var response = new Response<List<Board>>();

        try {
            response.Body = await _context.Boards.Include(b => b.Columns).ToListAsync();

            if (response.Body.Count == 0) {
                response.Body = null;
                response.Message = "nenhum board foi encontrado";
            }

        } catch (Exception ex) {
            response.Body = null;
            response.Sucess = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<Board>> GetBoardByIdAsync(Guid id) {
        var response = new Response<Board>();

        try {
            response.Body = await _context.Boards.Include(b => b.Columns).SingleOrDefaultAsync(b => b.Id == id);

            if (response.Body == null) {
                response.Sucess = false;
                response.Message = "board nao encontrado";
            }

        } catch (Exception ex) {
            response.Body = null;
            response.Sucess = false;
            response.Message = ex.Message;
        }

        return response;
    }
  
    public async Task<Response<Board>> InactivateBoardByIdAsync(Guid Id) {
        var response = new Response<Board>();

        try {
            var board = await _context.Boards.Include(b => b.Columns).SingleOrDefaultAsync(b => b.Id == Id);

            if (board == null) {
                response.Body = null;
                response.Sucess = false;
                response.Message = "board nao encontrado";
            }

            board.IsActive = false;
            board.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            response.Message = "board inativado com sucesso";
            response.Body = board;

        } catch (Exception ex) {
            response.Body = null;
            response.Sucess = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<Board>> NewBoardsAsync(BoardRequests board) {
        var response = new Response<Board>();

        try {

            if (board.Columns != null) {
                List<Column> columns = new List<Column>();

                var newBoard = new Board(null, board.Name);

                foreach (var column in board.Columns) {
                    var newColumn = new Column(newBoard.Id, column.Name);
                    columns.Add(newColumn);
                }

                newBoard.Columns = columns;

                await _context.Boards.AddAsync(newBoard);
                await _context.SaveChangesAsync();

                response.Body = newBoard;
                response.Message = "board cadastrado com sucesso com colunas informadas";
                return response;
            }

            var newBoardWithDefaulColumns = new Board(null, board.Name);
            await _context.Boards.AddAsync(newBoardWithDefaulColumns);
            await _context.SaveChangesAsync();

            response.Body = newBoardWithDefaulColumns;
            response.Message = "board cadastrado com sucesso com colunas padroes";

        } catch (Exception ex) {
            response.Body = null;
            response.Sucess = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<Board>> UpdateBoardByIdAsync(BoardRequests board, Guid Id) {
        var response = new Response<Board>();

        try {
            var boardDb = await _context.Boards.Include(b => b.Columns).SingleOrDefaultAsync(b => b.Id == Id);

            if (boardDb == null) {
                response.Body = null;
                response.Sucess = false;
                response.Message = "board nao encontrado";
                return response;
            }
            
            if(board.Columns != null) {
                List<Column> columns = new List<Column>();

                foreach (var column in board.Columns) {
                    var newColumn = new Column(Id, column.Name);
                    columns.Add(newColumn);
                }

                boardDb.Columns = columns;
            }

            boardDb.Name = board.Name;
            boardDb.IsActive = board.IsActive;
            boardDb.UpdatedAt = DateTime.Now;


            await _context.SaveChangesAsync();

            response.Body = boardDb;
            response.Message = "board atualizado com sucesso";

        }catch (Exception ex) {
            response.Body = null;
            response.Sucess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
