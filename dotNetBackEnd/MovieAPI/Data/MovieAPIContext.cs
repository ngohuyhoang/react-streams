using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Models
{
    public class MovieAPIContext : DbContext
    {
        public MovieAPIContext (DbContextOptions<MovieAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Stream> Stream { get; set; }

    }
}
