namespace BackEnd_KanBan.Models.UserModels;

public class User : BaseModels{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        if(name.Length > 150 || string.IsNullOrEmpty(name)) {
            throw new ArgumentException("o nome do usuario precisa ter menos de 150 caracteres e não pode ser vazio");
        }

        if (email.Length > 150 || string.IsNullOrEmpty(email)) {
            throw new ArgumentException("o email do usuario precisa ter menos de 150 caracteres e não pode ser vazio");
        }

        if (password.Length > 150 || string.IsNullOrEmpty(password)) {
            throw new ArgumentException("a senha do usuario precisa ter menos de 150 caracteres e não pode ser vazia");
        }

        Name = name;
        Email = email;
        Password = password;
    }
}
