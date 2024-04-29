using EmiCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace EmiCalculator.Context
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<EmiCalculatorData> EmiCalculatorData { get; set; }
    }
}
