namespace BackEnd_KanBan.Models.ColumnModels;

public class Column : BaseModels{
    public Guid BoardId { get; set; }
    public List<CardModels.Card>? Cards { get; set; }
    public bool IsActive { get; set; } = true;

    public Column(Guid boardId, string name)
    {
        if(name.Length > 150 || string.IsNullOrEmpty(name)) {
            throw new ArgumentException("o nome da coluna nao pode ser vazio e precisa ter menos que 150 caracteres");
        }

        Name = name;
        BoardId = boardId;
    }
}
