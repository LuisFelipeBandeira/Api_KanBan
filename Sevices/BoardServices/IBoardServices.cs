using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.BoardModels;
using BackEnd_KanBan.Models.UserModels;
using BoardRequests = BackEnd_KanBan.Models.BoardModels.BoardRequests;

namespace BackEnd_KanBan.Sevices.BoardServices;

public interface IBoardServices {
    Task<Response<List<Board>>> GetAllBoardsAsync();
    Task<Response<Board>> GetBoardByIdAsync(Guid id);
    Task<Response<Board>> NewBoardsAsync(BoardRequests board);
    Task<Response<Board>> DeleteBoardByIdAsync(Guid Id);
    Task<Response<Board>> UpdateBoardByIdAsync(BoardRequests board, Guid Id);
    Task<Response<Board>> InactivateBoardByIdAsync(Guid Id);
}
