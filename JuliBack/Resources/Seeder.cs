using JuliBack.Models;
using JuliBack.Contexto;

namespace JuliBack.Resources;

public class Seeder
{
    private readonly AppDbContext _Context;

    public Seeder(AppDbContext Context) => _Context = Context;

    public async Task CreateUsers()
    {
        if (!_Context.Users.Any())
        {
            List<Users> Users =
            [
                new Users
                    {
                        Username = "Ivan",
                        Password = BCrypt.Net.BCrypt.HashPassword("Gazanga13")

                    },

                    new Users
                    {
                        Username = "juliandesigner",
                        Password = BCrypt.Net.BCrypt.HashPassword("gordocompu14")
                    }
            ];

            await _Context.Users.AddRangeAsync(Users);

            await _Context.SaveChangesAsync();

        }

    }
}

