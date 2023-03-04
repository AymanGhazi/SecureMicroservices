using Microsoft.EntityFrameworkCore;
using Movies.API.Models;
using System.Collections.Generic;

namespace Movies.API.Data
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
        }

    }
}