using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using PeopleAndBooks.Business;
using PeopleAndBooks.Business.Implementations;
using PeopleAndBooks.Data;
using PeopleAndBooks.Repository.Generic;

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

            services.AddControllers();

            services.AddDbContext<SqlServerContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("SqlServerContext")));

            //Content Negociation
            //Service adicionado para que a aplicação aceite xml
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();

            // Versionamento da API - Precisa do nuget Microsoft.AspnetCore.Mvc.Versioning
            services.AddApiVersioning();

            
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
