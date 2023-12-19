using BackEnd_KanBan.Models.ColumnModels;

namespace BackEnd_KanBan.Models.CardModels;

public class Card : BaseModels{
    public Guid ColumnId { get; set; }
    public string? CardOwner { get; set; }
    public bool IsFinished { get; set; } = false;
    public Card(Guid columnId, string cardName)
    {
        Id = Guid.NewGuid();
        ColumnId = columnId;
        Name = cardName;
    }

    public Card()
    {
        
    }
}
