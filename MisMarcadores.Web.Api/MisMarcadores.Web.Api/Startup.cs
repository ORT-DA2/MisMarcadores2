using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MisMarcadores.Logic;
using MisMarcadores.Web.Api.Models;
using MisMarcadores.Repository;
using MisMarcadores.Data.DataAccess;

namespace MisMarcadores.Web.Api
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
            services.AddMvc();
            //Ejemplo de EF en Memoria
            //services.AddDbContext<DomainContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("WACDatabase")));
            //Ejemplo de EF con SQLServer
            services.AddDbContext<MisMarcadoresContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MisMarcadoresDB")));
            services.AddScoped<DbContext, MisMarcadoresContext>();

            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();

            //SERVICES
            services.AddScoped<IUsuariosService, UsuariosService>();
            
            //DATA ACCESS
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
