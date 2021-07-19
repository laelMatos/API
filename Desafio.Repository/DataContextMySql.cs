using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository
{
    public class DataContextMySql : DbContext
    {
        public DataContextMySql(DbContextOptions<DataContextMySql> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentMovie> RentMovies { get; set; }
        public DbSet<GenreMovie> GenreMovies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relacionamento de chave composta entre filmes e locação
            modelBuilder.Entity<RentMovie>()
                .HasKey(ck => new { ck.RentId, ck.MovieId });

            //Relacionamento de chave composta entre gênero e filmes
            modelBuilder.Entity<GenreMovie>()
                .HasKey(ck => new { ck.GenreId, ck.MovieId });
        }
    }
}