using BackEnd_KanBan.Models;
using BackEnd_KanBan.Models.UserModels;
using BackEnd_KanBan.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_KanBan.Sevices.UserServices;

public class UserServices : IUserServices {

    private readonly ApplicationDbContext _context;
    public UserServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Response<User>> DeleteUserByIdAsync(Guid Id) {
        var response = new Response<User>();

        try {
            var userToRemove = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();

            if (userToRemove == null) {
                response.Message = "usuario nao existe na base de dados";
                response.Sucess = false;
                return response;
            }

            _context.Users.Remove(userToRemove);
            await _context.SaveChangesAsync();
            response.Models = userToRemove;
            response.Message = "usuario deletado com sucesso";
            return response;


        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
            return response;
        }
    }

    public async Task<Response<List<User>>> GetAllAsync() {
        var response = new Response<List<User>>();

        try {
            response.Models = await _context.Users.ToListAsync();

            if (response.Models.Count == 0) {
                response.Message = "nenhum usuario foi encontrado";
            }

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<User>> GetByIdAsync(Guid Id) {
        var response = new Response<User>();

        try {
            response.Models = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();

            if (response.Models == null) {
                response.Message = "usuario nao encontrado";
            }

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<User>> NewUserAsync(UserRequests user) {
        var response = new Response<User>();

        try {
            var newUser = new User(user.Name, user.Email, user.Password);
            
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            response.Models = newUser;
            response.Message = "usuario cadastrado com sucesso";

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }
        return response;
    }

    public async Task<Response<User>> UpdateByIdAsync(Guid Id) {
        throw new NotImplementedException();
    }
}
