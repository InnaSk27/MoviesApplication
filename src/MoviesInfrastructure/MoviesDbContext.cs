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
    public DbSet<User> Users {get;set;}
    public DbSet<Actor> Actors {get;set;}
    public DbSet<Studio> Studio {get;set;}
    public DbSet<MovieActor> MovieActors {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasIndex(m => m.Name)
            .IsUnique();

        modelBuilder.Entity<Studio>()
            .HasIndex(m => m.Name)
            .IsUnique();

        modelBuilder.Entity<Studio>()
            .HasMany(a => a.Movies)
            .WithOne(p => p.Studio)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MovieActor>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId});

        modelBuilder.Entity<MovieActor>()
            .HasOne(a => a.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(a => a.Actor)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.ActorId);

    }
}
