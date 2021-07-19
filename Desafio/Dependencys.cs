using Desafio.API.Controllers;
using Desafio.API.Service;
using Desafio.Repository;
using Desafio.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.API
{
    /// <summary>
    /// Injeção de dependências
    /// </summary>
    public class Dependencys
    {
        /// <summary>
        /// Injeção de dependências
        /// </summary>
        public Dependencys(IServiceCollection services)
        {

            //scoped - apenas uma dependencia por requisiçao
            //transient - a cada requisição é criado uma nova dependencia
            //singleton - apenas uma dependencia para toda a aplicação
            
            //Injeção de dependencias de conexão
            services.AddScoped<DataContextMySql, DataContextMySql>();

            #region Injeção de dependencias dos Repositorios
            //Repositorio generico
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Repositorios com regras especificas
            services.AddScoped<GenreRepository, GenreRepository>();
            services.AddScoped<RentRepository, RentRepository>();
            services.AddScoped<MovieRepository, MovieRepository>();
            services.AddScoped<UserRepository, UserRepository>();

            #endregion

            #region Injeção de dependencias dos Serviços
            //Serviços de identificação e autenticação
            services.AddScoped<AuthService, AuthService>();
            services.AddScoped<UserService, UserService>();
            
            //Serviçõs da camada de negócio
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieService, MovieService>();

            #endregion
        }
    }
}
