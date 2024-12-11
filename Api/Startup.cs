using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.Handlers.AddStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.DeleteStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioHeaderItems;
using PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioItemTypes;
using PeoplesPartnership.ApiRefactor.Handlers.GetStudioItem;
using PeoplesPartnership.ApiRefactor.Handlers.UpdateStudioItem;

namespace PeoplesPartnership.ApiRefactor
{
    public class Startup
    {
        public const string CONNECTION_STRING_NAME = "StudioConnection";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StudioContext>((provider, options) =>
            {
                options.UseSqlServer(connectionString: Configuration.GetConnectionString(CONNECTION_STRING_NAME));
            });
            
            services.AddCors(options =>
             {
                 options.AddPolicy("AllowMyOrigin",
                 builder => builder.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 );
             });

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));

            //Register Handlers
            services.AddScoped<IDeleteStudioItemHandler, DeleteStudioItemHandler>();
            services.AddScoped<IAddStudioItemHandler, AddStudioItemHandler>();
            services.AddScoped<IGetAllStudioHeaderItemsHandler, GetAllStudioHeaderItemsHandler>();
            services.AddScoped<IGetAllStudioItemTypesHandler, GetAllStudioItemTypesHandler>();
            services.AddScoped<IGetStudioItemByIdHandler, GetStudioItemByIdHandler>();
            services.AddScoped<IUpdateStudioItemHandler, UpdateStudioItemHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowMyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers().RequireCors("AllowMyOrigin");
            });

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
