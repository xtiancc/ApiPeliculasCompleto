using ApiPeliculasCompleto.Data;
using ApiPeliculasCompleto.Helper;
using ApiPeliculasCompleto.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto
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
            String cadena = this.Configuration.GetConnectionString("cadenalocal");
            services.AddTransient<RepositoryPeliculas>();
            services.AddDbContext<PeliculasContext>( options => options.UseSqlServer(cadena));

            HelperOAuthToken helper = new HelperOAuthToken(this.Configuration);
            services.AddTransient<HelperOAuthToken>(x => helper);
            services.AddAuthentication(helper.GetAuthOptions()).AddJwtBearer(helper.GetJwtOptions());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPeliculasCompleto", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPeliculasCompleto v1");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // IMPORTANTE EL ORDEN
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
