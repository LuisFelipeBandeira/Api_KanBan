using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd_KanBan.Models;
using BackEnd_KanBan.Sevices.UserServices;
using BackEnd_KanBan.Models.UserModels;
using System.Collections.Generic;
using Microsoft.Identity.Client;
using BackEnd_KanBan.Api.Models.UserModels;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd_KanBan.Controllers;
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase {

    private readonly IUserServices _iUserServices;
    public UserController(IUserServices iUserServices) {
        _iUserServices = iUserServices;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Response<List<User>>>> GetAllAsync() {
        var response = await _iUserServices.GetAllAsync();

        if (response.Sucess) {
            return Ok(response);
        }

        return NotFound(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Response<User>>> GetByIdAsync([FromRoute] Guid id) {
        var response = await _iUserServices.GetByIdAsync(id);

        if (response.Sucess) {
            return Ok(response);
        }

        return NotFound(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Response<User>>> NewUserAsync([FromBody] UserRequests user) {
        var response = await _iUserServices.NewUserAsync(user);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Response<User>>> DeleteUserByIdAsync([FromRoute] Guid id) {
        var response = await _iUserServices.DeleteByIdAsync(id);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Response<User>>> UpdateByIdAsync([FromRoute] Guid id, [FromBody] UserRequests user) {
        var response = await _iUserServices.UpdateByIdAsync(user, id);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("api/users/inactivate/{id}")]
    [Authorize]
    public async Task<ActionResult<Response<User>>> InactivateByIdAsync([FromRoute] Guid id) {
        var response = await _iUserServices.InactivateByIdAsync(id);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }


    [HttpPost("login")]
    public async Task<ActionResult<Response<User>>> UserLoginAsync([FromBody] UserLogin userLogin) {
        var response = await _iUserServices.UserLoginAsync(userLogin);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
