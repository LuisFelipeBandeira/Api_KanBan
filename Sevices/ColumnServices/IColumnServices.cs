using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.ColumnModels;

namespace BackEnd_KanBan.Sevices.ColumnServices;

public interface IColumnServices {
    Task<Response<Column>> GetColumnByIdAsync(Guid columnId);
    Task<Response<Column>> GetColumnByBoardAsync(Guid boardId);
    Task<Response<Column>> GetColumnByCardAsync(Guid cardId);
    Task<Response<Column>> UpdateColumnByIdAsync(ColumnRequests column, Guid Id);
    Task<Response<Column>> InactivateColumnByIdAsync(Guid columnId);
    Task<Response<Column>> NewColumnAsync(ColumnRequests column, Guid boardId);
    Task<Response<Column>> DeleteColumnAsync(Guid columnId);
}
