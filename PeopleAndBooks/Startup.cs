using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PeopleAndBooks.Business;
using PeopleAndBooks.Business.Implementations;
using PeopleAndBooks.Data;
using PeopleAndBooks.Repository.Generic;
using System;

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
            #region CORS
            /*
                CORS - Cross-Origin Resource Sharing (Compartilhamento de recursos com origens diferentes) 
                é um mecanismo que usa cabeçalhos adicionais HTTP para informar a um navegador que permita 
                que um aplicativo Web seja executado em uma origem (domínio) com permissão para acessar 
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

            // Esta implementação faz com que sua API possa prover conteúdo tanto em JSON quanto em XML

            /*
            Service adicionado para que a aplicação aceite xml
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
            // Também há trechos de códigos nas controllers
            services.AddApiVersioning();
            #endregion

            #region SWAGGER

            // Implantação do Swagger
            // Precisa do pacote SwashBuckle

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API People and books",
                        Version = "v1",
                        Description = "API simples desenvolvida para aprendizado e portifólio",
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
            
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();            
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

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
                Para esta configuração devemos nos atenta a seguintes regras:

                - Em "Configure", esta implementação do CORS deve ficar depois de:
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
