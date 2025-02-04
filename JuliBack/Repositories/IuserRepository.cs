using JuliBack.Models;
using JuliBack.Contexto;
using Microsoft.EntityFrameworkCore;


namespace JuliBack.Repositories
{

    public interface IuserRepository
    {
        Task AddUsersAsync(Users users);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        Task SaveChangesAsync();

    }


    public class UserRepository : IuserRepository 
    {
        private readonly AppDbContext _Context;

        public UserRepository(AppDbContext appDbContext) 
        { 
            _Context = appDbContext; 
        }
    
        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync(); 
        }

        public async Task AddUsersAsync(Users users)
        {
             await _Context.Users.AddAsync(users);
        }


        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
