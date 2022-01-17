using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeopleAndBooks.Business;
using PeopleAndBooks.Business.Implementations;
using PeopleAndBooks.Data;
using PeopleAndBooks.Repository;
using PeopleAndBooks.Repository.Implementations;

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
            

            // Versionamento da API - Precisa do nuget Microsoft.AspnetCore.Mvc.Versioning
            services.AddApiVersioning();

            services.AddScoped<IPersonRepositoy, PersonRepositoryImplementation>();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

            services.AddScoped<IBookRepository, BookRepositoryImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

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
