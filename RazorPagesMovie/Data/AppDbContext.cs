using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RazorPagesMovie.Models;


namespace RazorPagesMovie.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Reservation>Reservations{get; set;}
        public DbSet<Room> Rooms{get; set;}

    }

 
       
    
}

