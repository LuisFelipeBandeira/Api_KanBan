namespace BackEnd_KanBan.Models.ColumnModels;

public class Column : BaseModels{
    public Guid BoardId { get; set; }
    public List<CardModels.Card> Cards { get; set; }
    public bool IsActive { get; set; } = true;
}
