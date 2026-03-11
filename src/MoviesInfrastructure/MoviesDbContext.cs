using MySql.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using MoviesDomain.Entities;

namespace MoviesInfrastructure;

public class MoviesDbContext: DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {

    }    
    public DbSet<Movie> Movies {get;set;}
}
