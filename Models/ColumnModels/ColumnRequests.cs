namespace BackEnd_KanBan.Models.ColumnModels;

using BackEnd_KanBan.Api.Models.CardModels;

public class ColumnRequests {
    public string Name { get; set; }
    public Guid? BoardId { get; set; }
    public List<CardRequests>? Cards { get; set; }

}
