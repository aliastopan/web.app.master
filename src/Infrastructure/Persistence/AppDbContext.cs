using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Common;
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        { }

        public DbSet<User>? Users { get; set; }

        public async Task InsertUserAsync(User user)
        {
            user.Password = Cryptograph.GetHash(user.Password!);

            this.Users!.Add(user);
            await this.SaveChangesAsync();
        }

        public (User, bool) LookUpUser(string username, string password)
        {
            string hash = Cryptograph.GetHash(password);

            var result = this.Users!
                .FirstOrDefault(user =>
                    username == user.Username &&
                    hash == user.Password);

            return (result, result is not null)!;
        }
    }
}