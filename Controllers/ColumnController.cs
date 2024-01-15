using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.ColumnModels;
using BackEnd_KanBan.Sevices.ColumnServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_KanBan.Api.Controllers;
[Route("api/columns")]
[ApiController]
public class ColumnController : ControllerBase {

    private readonly IColumnServices _columnServices;

    public ColumnController(IColumnServices columnServices)
    {
        _columnServices = columnServices;
    }

    [HttpGet("bycolumnid/{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Column>>> GetColumnByIdAsync([FromRoute] Guid columnId) {
        var response = await _columnServices.GetColumnByIdAsync(columnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("bycolumnId/withcards/{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Column>>> GetColumnAndCardsByIdAsync([FromRoute] Guid columnId) {
        var response = await _columnServices.GetColumnAndCardsByIdAsync(columnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("byboardid/{boardId}")]
    [Authorize]
    public async Task<ActionResult<Response<List<Column>>>> GetColumnByBoardAsync([FromRoute] Guid boardId) {
        var response = await _columnServices.GetColumnsByBoardAsync(boardId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("byboardId/withcards/{boardId}")]
    [Authorize]
    public async Task<ActionResult<Response<List<Column>>>> GetColumnAndCardsByBoardAsync([FromRoute] Guid boardId) {
        var response = await _columnServices.GetColumnAndCardsByBoardAsync(boardId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }


    [HttpGet("bycardid/{cardid}")]
    [Authorize]
    public async Task<ActionResult<Response<Column>>> GetColumnByCardAsync([FromRoute] Guid cardid) {
        var response = await _columnServices.GetColumnByCardAsync(cardid);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);

    }

    [HttpPut("{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Column>>> UpdateColumnByIdAsync([FromBody] ColumnRequests column, [FromRoute] Guid columnId) {
        var response = await _columnServices.UpdateColumnByIdAsync(column, columnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);

    }


    [HttpPut("inactivate/{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Column>>> InactivateColumnByIdAsync([FromRoute] Guid columnId) {
        var response = await _columnServices.InactivateColumnByIdAsync(columnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);

    }


    [HttpPost("{boardId}")]
    [Authorize]
    public async Task<ActionResult<Response<List<Column>>>> NewColumnsAsync([FromBody] List<ColumnRequests> column, [FromRoute] Guid boardId) {
        var response = await _columnServices.NewColumnsAsync(column, boardId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);

    }

    [HttpDelete("{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Column>>> DeleteColumnAsync([FromRoute] Guid columnId) {
        var response = await _columnServices.DeleteColumnAsync(columnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);

    }


}
