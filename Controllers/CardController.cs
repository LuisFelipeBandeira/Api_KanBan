using BackEnd_KanBan.Api.Models.CardModels;
using BackEnd_KanBan.Api.Sevices.CardServices;
using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.CardModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace BackEnd_KanBan.Api.Controllers;
[Route("api/cards")]
[ApiController]
public class CardController : ControllerBase {

    private readonly ICardServices _cardServices;
    public CardController(ICardServices cardServices)
    {
        _cardServices = cardServices;
    }

    [HttpGet("/byOwner/{owner}")]
    [Authorize]
    public async Task<ActionResult<Response<List<Card>>>> GetCardsByOwner([FromRoute] string owner) {
        var response =  await _cardServices.GetCardsByOwner(owner);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("/byColumn/{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<List<Card>>>> GetCardsByColumn([FromRoute] Guid columnId) {
        var response = await _cardServices.GetCardsByColumn(columnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("/byCardId/{cardId}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> GetCardById([FromRoute] Guid cardId) {
        var response = await _cardServices.GetCardById(cardId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("/finish/{cardId}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> FinishCard([FromRoute] Guid cardId) {
        var response = await _cardServices.FinishCard(cardId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("/SetCardOwner/{cardId}/{cardOwner}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> SetCardOwner([FromRoute] Guid cardId, [FromRoute] string cardOwner) {
        var response = await _cardServices.SetCardOwner(cardId, cardOwner);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("/ChangeColumn/{cardId}/{destinationColumnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> ChangeColumn([FromRoute] Guid cardId, [FromRoute] Guid destinationColumnId) {
        var response = await _cardServices.ChangeColumn(cardId, destinationColumnId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("/{cardId}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> UpdateCard([FromRoute] Guid cardId, [FromBody] CardRequests card) {
        var response = await _cardServices.UpdateCard(cardId, card);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("/{columnId}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> NewCard([FromRoute] Guid columnId, [FromBody] CardRequests card) {
        var response = await _cardServices.NewCard(columnId, card);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("/{cardId}")]
    [Authorize]
    public async Task<ActionResult<Response<Card>>> DeleteCard([FromRoute] Guid cardId) {
        var response = await _cardServices.DeleteCard(cardId);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }


}
