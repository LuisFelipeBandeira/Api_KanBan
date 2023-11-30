﻿using BackEnd_KanBan.Models.ColumnModels;

namespace BackEnd_KanBan.Models.BoardModels;

public class Board : BaseModels{
    public List<Column> Columns { get; set; } = new List<Column>();
    public bool IsActive { get; set; } = true;

    public Board(List<Column>? columns, string name)
    {
        Id = Guid.NewGuid();

        if (name.Length > 150 || string.IsNullOrEmpty(name)) {
            throw new ArgumentException("o nome do usuario precisa ter menos de 150 caracteres e não pode ser vazio");
        }
        Name = name;

        if (columns != null) {
            Columns = columns;
        } else {
            var defaultColumns = new List<Column>{
                new Column(Id, "aguardando"),
                new Column(Id, "analisando"),
                new Column(Id, "finalizado"),
            };
            
            Columns = defaultColumns;
        }
    }
}
