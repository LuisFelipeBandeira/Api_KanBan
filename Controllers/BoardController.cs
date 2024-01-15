using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.BoardModels;
using BackEnd_KanBan.Sevices.BoardServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BackEnd_KanBan.Controllers;
[Route("api/boards")]
[ApiController]
public class BoardController : ControllerBase {
    private readonly IBoardServices _boardServices;

    public BoardController(IBoardServices boardServices) {
        _boardServices = boardServices;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Response<List<Board>>>> GetAllBoardsAsync() {
        var response = await _boardServices.GetAllBoardsAsync();

        if (!response.Sucess) {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Response<Board>>> GetBoardByIdAsync([FromRoute] Guid id) {
        var response = await _boardServices.GetBoardByIdAsync(id);

        if (!response.Sucess) {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Response<Board>>> NewBoardsAsync([FromBody] BoardRequests board) {
        var response = await _boardServices.NewBoardsAsync(board);

        if (!response.Sucess) {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Response<Board>>> DeleteBoardByIdAsync([FromRoute] Guid id) {
        var response = await _boardServices.DeleteBoardByIdAsync(id);

        if (!response.Sucess) {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Response<Board>>> UpdateBoardByIdAsync([FromRoute] Guid id, [FromBody] BoardRequests board) {
        var response = await _boardServices.UpdateBoardByIdAsync(board, id);

        if (!response.Sucess) {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("api/boards/inactivate/{id}")]
    [Authorize]
    public async Task<ActionResult<Response<Board>>> InactivateBoardByIdAsync([FromRoute] Guid id) {
        var response = await _boardServices.InactivateBoardByIdAsync(id);
        
        if (!response.Sucess) {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
