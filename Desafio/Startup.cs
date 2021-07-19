using Desafio.API;
using Desafio.API.App_start;
using Desafio.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Desafio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //MultLinguagem baseada na localização
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            
            //Libera o acesso via localhost
            services.AddCors();

            //Compressão das respostas em json
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            //usar cache, para todos as chamadas.
            //services.AddResponseCaching();

            services.AddControllers();

            var Key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddMvc(options =>
            {
                //Filtro de validações
                options.Filters.Add(typeof(ValidateModelStateFilter));
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            //
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Banco em memoria
            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            //Captura a connection string para se conectar com o banco
            services.AddDbContext<DataContextMySql>(opt => opt.UseSqlServer(
                Configuration.GetConnectionString("connectionString")
            ));

            //Injeção de dependencias
            var dependency = new Dependencys(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio - Lael Santos de Matos", Version = "1º versão Desafio"});

                //Adiciona autenticação para usar dento do Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT usando o esquema Bearer. " +
                    "\r\n\r\n Digite 'Bearer' [espaço] e, em seguida, seu token na entrada de texto abaixo." +
                    "\r\n\r\nExemplo: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //Atribuir token de autenticação a todas as requisições
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                
                app.UseSwaggerUI(c => {
                    // Desabilitar swagger schemas
                    c.DefaultModelsExpandDepth(-1);
                    //Rota para acessar dados via json
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Versão 1");
                    });
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            //Libera o acesso via localhost
            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
