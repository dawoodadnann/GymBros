using GymBroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBroApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<GymBro> Bros { get; set; }
}