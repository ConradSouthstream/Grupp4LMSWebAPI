using Grupp4Lms.Core.IRepositories;
using Grupp4Lms.Data.Data;
using Grupp4Lms.Data.MapperProfiles;
using Grupp4Lms.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Grupp4Lms.Api
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
            services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddXmlDataContractSerializerFormatters();

            //services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Grupp4Lms.Api", Version = "v1" });

                // Grupp4Lms.Api.xml
                // Grupp4Lms.Core.xml
                // Grupp4Lms.Data.xml

                // Get the xml comments file
                var xmlCommentsFileApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPathApi = Path.Combine(AppContext.BaseDirectory, xmlCommentsFileApi);
                var xmlCommentFullPathCore = Path.Combine(AppContext.BaseDirectory, "Grupp4Lms.Core.xml");
                var xmlCommentFullPathData = Path.Combine(AppContext.BaseDirectory, "Grupp4Lms.Data.xml");

                c.IncludeXmlComments(xmlCommentFullPathApi);
                c.IncludeXmlComments(xmlCommentFullPathCore);
                c.IncludeXmlComments(xmlCommentFullPathData);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grupp4Lms.Api v1"));
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
