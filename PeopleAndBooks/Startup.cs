using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeopleAndBooks.Data;
using PeopleAndBooks.Services;
using PeopleAndBooks.Services.Implementations;

namespace PeopleAndBooks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<SqlServerContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("SqlServerContext")));

            // Versionamento da API - Precisa do nuget Microsoft.AspnetCore.Mvc.Versioning
            services.AddApiVersioning();

            services.AddScoped<IPersonService, PersonServiceImplementation>();

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
