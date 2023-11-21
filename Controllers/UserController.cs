using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd_KanBan.Models;
using BackEnd_KanBan.Sevices.UserServices;
using BackEnd_KanBan.Models.UserModels;
using System.Collections.Generic;

namespace BackEnd_KanBan.Controllers;
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase {

    private readonly IUserServices _iUserServices;
    public UserController(IUserServices iUserServices) {
        _iUserServices = iUserServices;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<User>>>> GetAllAsync() {
        var response = await _iUserServices.GetAllAsync();

        if (response.Sucess) {
            return Ok(response);
        }

        return NotFound(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<User>>> GetByIdAsync([FromRoute] Guid id) {
        var response = await _iUserServices.GetByIdAsync(id);

        if (response.Sucess) {
            return Ok(response);
        }
        
        return NotFound(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<User>>> NewUserAsync([FromBody] UserRequests user) {
        var response = await _iUserServices.NewUserAsync(user);

        if (response.Sucess) {
            return Ok(response);
        }

        return BadRequest(response);
    }


}
