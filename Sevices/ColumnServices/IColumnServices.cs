using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.ColumnModels;

namespace BackEnd_KanBan.Sevices.ColumnServices;

public interface IColumnServices {
    Task<Response<Column>> GetColumnByIdAsync(Guid columnId);
    Task<Response<Column>> GetColumnAndCardsByIdAsync(Guid columnId);
    Task<Response<List<Column>>> GetColumnsByBoardAsync(Guid boardId);
    Task<Response<List<Column>>> GetColumnAndCardsByBoardAsync(Guid boardId);
    Task<Response<Column>> GetColumnByCardAsync(Guid cardId);
    Task<Response<Column>> UpdateColumnByIdAsync(ColumnRequests column, Guid ColumnId);
    Task<Response<Column>> InactivateColumnByIdAsync(Guid columnId);
    Task<Response<List<Column>>> NewColumnsAsync(List<ColumnRequests> column, Guid boardId);
    Task<Response<Column>> DeleteColumnAsync(Guid columnId);
}
