using Desafio.API.Controllers;
using Desafio.API.Service;
using Desafio.Repository;
using Desafio.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.API
{
    public class Dependencys
    {
        public Dependencys(IServiceCollection services)
        {

            //scoped - apenas uma dependencia por requisiçao
            //transient - a cada requisição é criado uma nova dependencia
            //singleton - apenas uma dependencia para toda a aplicação
            
            //Injeção de dependencias de conexão
            services.AddScoped<DataContextMySql, DataContextMySql>();

            #region Injeção de dependencias dos Repositorios
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<GenreRepository, GenreRepository>();
            services.AddScoped<RentRepository, RentRepository>();
            services.AddScoped<MovieRepository, MovieRepository>();
            services.AddScoped<UserRepository, UserRepository>();

            #endregion

            #region Injeção de dependencias dos Serviços
            services.AddScoped<AuthService, AuthService>();
            
            //Serviçõs da camada de negócio
            services.AddScoped<UserService, UserService>();
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieService, MovieService>();

            #endregion
        }
    }
}
