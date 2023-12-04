namespace BackEnd_KanBan.Api.Models.CardModels;

public class CardRequests {
    public string Name { get; set; }
    public Guid ColumnId { get; set; }
    public string? CardOwner { get; set; }
}
