using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PeopleAndBooks.Business;
using PeopleAndBooks.Business.Implementations;
using PeopleAndBooks.Data;
using PeopleAndBooks.Repository;
using PeopleAndBooks.Repository.Generic;
using PeopleAndBooks.Repository.User;
using PeopleAndBooks.Repository.User.Implementation;
using PeopleAndBooks.System.Configuration.Token;
using PeopleAndBooks.System.Configuration.Token.Services;
using System;
using System.Text;

namespace PeopleAndBooks
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            #region JWT TOKEN CONFIGURE
            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                        Configuration.GetSection("TokenConfiguration")
                ).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            //Add as configura��es de autentica��o...

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfigurations.Issuer,
                        ValidAudience = tokenConfigurations.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            #endregion

            #region CORS
            /*
                CORS - Cross-Origin Resource Sharing (Compartilhamento de recursos com origens diferentes) 
                � um mecanismo que usa cabe�alhos adicionais HTTP para informar a um navegador que permita 
                que um aplicativo Web seja executado em uma origem (dom�nio) com permiss�o para acessar 
                recursos selecionados de um servidor em uma origem distinta.

             */

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            #endregion

            services.AddControllers();

            services.AddDbContext<SqlServerContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("SqlServerContext")));

            #region CONTENT NEGOTIATION

            // Esta implementa��o faz com que sua API possa prover conte�do tanto em JSON quanto em XML

            /*
            Service adicionado para que a aplica��o aceite xml
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();
            */
            #endregion

            #region VERSIONAMENTO DA API (por controller)
            // Versionamento da API - Precisa do nuget Microsoft.AspnetCore.Mvc.Versioning
            // Tamb�m h� trechos de c�digos nas controllers
            services.AddApiVersioning();
            #endregion

            #region SWAGGER

            // Implanta��o do Swagger
            // Precisa do pacote SwashBuckle

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API People and books",
                        Version = "v1",
                        Description = "API simples desenvolvida para aprendizado e portif�lio",
                        Contact = new OpenApiContact
                        {
                            Name = "Helbert Moreira",
                            Url = new Uri("https://www.linkedin.com/in/helbert-moreira-96554b155/")
                        }
                    });
            });
            #endregion

            #region HATEOAS
            #endregion

            #region JWT TOKEN
            services.AddTransient<TokenService>();
            #endregion

            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();            
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<ILogin, LoginBusinessImplementationcs>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region CORS
            /*
                Para esta configura��o devemos nos atenta a seguintes regras:

                - Em "Configure", esta implementa��o do CORS deve ficar depois de:
                    * app.UseHttpsRedirection()
                    * app.UseRouting()
             */
            app.UseCors();
            #endregion

            #region SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                    "API People end books - v1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}
