namespace BackEnd_KanBan.Models.BoardModels;

public class Board : BaseModels{
    public List<ColumnModels.Column> Columns { get; set; }
    public bool IsActive { get; set; } = true;

}
