using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template.Configuration;
using Template.Configuration.Options;
using Template.Extensions;
using Template.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Template
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
            services.AddApplicationServices();

            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptionsMonitor<DatabaseOptions>>();
                string connectionString = dbOptions.CurrentValue.BuildConnectionString();
                options.UseNpgsql(connectionString);
            });

            services.AddOptions<HttpOptions>()
                .Bind(Configuration.GetSection("Http"));
            services.AddOptions<DatabaseOptions>()
                .Bind(Configuration.GetSection("Database"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
