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
using MisMarcadores.Log;
using MisMarcadores.Web.Api.Models;
using MisMarcadores.Repository;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Web.Api.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // add asp net core mvc
            services.AddMvc(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            services.AddDbContext<MisMarcadoresContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MisMarcadoresDB")));
            services.AddScoped<DbContext, MisMarcadoresContext>();

            //SERVICES
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<ISesionesService, SesionesService>();
            services.AddScoped<IDeportesService, DeportesService>();
            services.AddScoped<IParticipantesService, ParticipantesService>();
            services.AddScoped<IEncuentrosService, EncuentrosService>();
            services.AddScoped<ILogService, LogService>();

            //DATA ACCESS
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<ISesionesRepository, SesionesRepository>();
            services.AddScoped<IDeportesRepository, DeportesRepository>();
            services.AddScoped<IParticipantesRepository, ParticipantesRepository>();
            services.AddScoped<IEncuentrosRepository, EncuentrosRepository>();
            services.AddScoped<IFavoritosRepository, FavoritosRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //FILTERS
            services.AddScoped<BaseFilter>();
            services.AddScoped<AutenticacionFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();

            app.UseCors("CorsPolicy");

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "Default",
                    template: "api/{controller}/{id?}"
                );
            });
        }
    }
}
