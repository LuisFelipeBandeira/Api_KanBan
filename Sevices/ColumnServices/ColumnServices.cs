using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.ColumnModels;

namespace BackEnd_KanBan.Sevices.ColumnServices;

public class ColumnServices : IColumnServices {
    public Task<Response<Column>> DeleteColumnAsync(Guid columnId) {
        throw new NotImplementedException();
    }

    public Task<Response<Column>> GetColumnByBoardAsync(Guid boardId) {
        throw new NotImplementedException();
    }

    public Task<Response<Column>> GetColumnByCardAsync(Guid cardId) {
        throw new NotImplementedException();
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
