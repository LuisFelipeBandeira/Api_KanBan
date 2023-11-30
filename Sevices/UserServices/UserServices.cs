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
    public async Task<Response<User>> DeleteByIdAsync(Guid Id) {
        var response = new Response<User>();

        try {
            var userToRemove = await _context.Users.SingleOrDefaultAsync(u => u.Id.Equals(Id));

            if (userToRemove == null) {
                response.Message = "usuario nao existe na base de dados";
                response.Sucess = false;
                return response;
            }

            _context.Users.Remove(userToRemove);
            await _context.SaveChangesAsync();
            response.Body = userToRemove;
            response.Message = "usuario deletado com sucesso";

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<List<User>>> GetAllAsync() {
        var response = new Response<List<User>>();

        try {
            response.Body = await _context.Users.ToListAsync();

            if (response.Body.Count == 0) {
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
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id.Equals(Id));

            if (user == null) {
                response.Message = "usuario nao encontrado";
                response.Sucess = false;
                return response;
            }

            response.Body = user;

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<User>> NewUserAsync(BoardRequests user) {
        var response = new Response<User>();

        try {
            var newUser = new User(user.Name, user.Email, user.Password);
            
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            response.Body = newUser;
            response.Message = "usuario cadastrado com sucesso";

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }
        return response;
    }

    public async Task<Response<User>> UpdateByIdAsync(BoardRequests user, Guid Id) {
        var response = new Response<User>();

        try {
            var userDb = await _context.Users.SingleOrDefaultAsync(u => u.Id.Equals(Id));

            if (userDb == null) {
                response.Message = "usuario nao encontrado";
                response.Sucess = false;
                return response;
            }

            userDb.Name = user.Name;
            userDb.Email = user.Email;
            userDb.Password = user.Password;
            await _context.SaveChangesAsync();
            response.Body = userDb;
            response.Message = "usuario atualizado com sucesso";

        }catch(Exception ex) {
            response.Message = ex.Message;
            response.Sucess = false;
        }

        return response;
    }

    public async Task<Response<User>> InactivateByIdAsync(Guid Id) {
        var response = new Response<User>();

        try {
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Id.Equals(Id));

            if (user == null) {
                response.Sucess = false;
                response.Message = "usuario nao encontrado";
                return response;
            }

            user.IsActive = false;
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            response.Message = "usuario inativado com sucesso";
            response.Body = user;

        }catch(Exception ex) {
            response.Sucess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
