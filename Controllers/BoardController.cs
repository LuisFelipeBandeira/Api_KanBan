using BackEnd_KanBan.Sevices.BoardServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_KanBan.Controllers;
[Route("api/boards")]
[ApiController]
public class BoardController : ControllerBase {
    private readonly IBoardServices _boardServices;

    public BoardController(IBoardServices boardServices)
    {
        _boardServices = boardServices;
    }

    
}
