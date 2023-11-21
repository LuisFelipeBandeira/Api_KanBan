using Microsoft.AspNetCore.Components.Web;

namespace BackEnd_KanBan.Models;

public class Response<T> where T : class{
    public T? Models { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Sucess { get; set; } = true;
 
}
