using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.UserModels;
using BackEnd_KanBan.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_KanBan.Sevices.UserServices;

public class UserServices : IUserServices {

    private readonly ApplicationDbContext _context;
    public UserServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Response<User>> DeleteByIdAsync([FromRoute] Guid Id) {
        throw new NotImplementedException();
    }

    public async Task<Response<List<User>>> GetAllAsync() {
        var response = new Response<List<User>>();

        try {
            response.Models = _context.Users.ToList();

            if (response.Models.Count == 0) {
                response.Message = "nenhum usuario foi encontrado";
            }

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<User>> GetByIdAsync([FromRoute] Guid Id) {
        var response = new Response<User>();

        try {
            response.Models = _context.Users.Where(u => u.Id == Id).FirstOrDefault();

            if (response.Models == null) {
                response.Message = "usuario nao encontrado";
            }

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<User>> NewUserAsync([FromBody] UserRequests user) {
        var response = new Response<User>();

        try {
            var newUser = new User(user.Name, user.Email, user.Password);
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            response.Models = newUser;
            response.Message = "usuario cadastrado com sucesso";

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }
        return response;
    }

    public async Task<Response<User>> UpdateByIdAsync([FromRoute] Guid Id) {
        throw new NotImplementedException();
    }
}
