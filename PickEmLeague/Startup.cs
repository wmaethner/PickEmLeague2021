﻿using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PickEmLeague.Registrations;
using PickEmLeagueDatabase;
using PickEmLeagueModels.Profiles;

using Amazon.Runtime.Credentials;
using Amazon.Runtime;
using System.IO;
using System;

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

            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            services.RegisterDatabase(Configuration);
            services.RegisterServices();
            services.RegisterRepositories();


            //var creds = File.ReadAllLines(Path.Combine(
            //        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            //        "AwsCreds.txt"));

            //services.AddDefaultAWSOptions(new Amazon.Extensions.NETCore.Setup.AWSOptions()
            //{
            //    Credentials = new BasicAWSCredentials(creds[0].Split("=")[1], creds[1].Split("=")[1])
            //});

            //Console.WriteLine(creds[0]);
            //Console.WriteLine(creds[1]);
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
