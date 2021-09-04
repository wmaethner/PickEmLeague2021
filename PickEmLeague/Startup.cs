using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PickEmLeagueDatabase;
using PickEmLeagueModels.Profiles;
using PickEmLeagueServices.Repositories.Implementations;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague
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
            //services.AddControllers();
            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddCors(options =>
            {
                options.AddPolicy("Policy",
                                    builder =>
                                    {
                                        builder.AllowAnyOrigin();
                                        builder.AllowAnyMethod();
                                        builder.AllowAnyHeader();
                                    });
            });

            services.AddDbContext<PickEmLeagueDbContext>(opts =>
            {
                opts.UseNpgsql(Configuration.GetConnectionString("PostgresDbConnection"),
                    b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(PickEmLeagueDbContext)).GetName().FullName));
            });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            AddRepositories(services);
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("Policy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            MigrateDb(app);
        }

        private void MigrateDb(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<PickEmLeagueDbContext>();
            db.Database.Migrate();
        }
    }
}
