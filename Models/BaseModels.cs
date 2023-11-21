using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd_KanBan.Models;

public abstract class BaseModels {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
