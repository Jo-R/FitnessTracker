using FitnessTracker.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FitnessTracker.Data.Handlers.Users;
using FitnessTracker.Api.Converters;

namespace FitnessTracker.Api
{

    public class Startup
    {

        readonly string corsPolicy = "all";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
             .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));
            services.AddDbContext<FitnessTrackerContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // load the assembley where the mediatr handlers live so they get registered with container
            services.AddMediatR(typeof(CreateUserHandler).Assembly);
            // TODO all origins for now but might want to restrict
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy,  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(corsPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
