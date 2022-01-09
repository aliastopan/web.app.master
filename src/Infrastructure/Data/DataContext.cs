using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        { }

        public DbSet<User>? Users { get; set; }
    }
}