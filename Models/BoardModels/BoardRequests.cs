using BackEnd_KanBan.Models.ColumnModels;

namespace BackEnd_KanBan.Models.BoardModels;

public class BoardRequests {
    public string Name { get; set; }
    public List<Column>? Columns { get; set; }
    public bool IsActive { get; set; }
}
