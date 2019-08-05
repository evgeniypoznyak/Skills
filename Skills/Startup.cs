using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Serilog;
using Skills.API.Middleware;
using Skills.Domain.Aggregate;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;
using Skills.Infrastructure.Adapter.MongoDb;

namespace Skills
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            BuildConfiguration();
            _logger = logger;
        }

        private void BuildConfiguration()
        {
            switch (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            {
                case "Production":
                    Configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                    break;
                default:
                    Configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.Development.json")
                        .Build();
                    break;
            }
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            SetupEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                .Destructure.ByTransforming<HttpRequest>(r => new {r.Body, r.Headers, r.Method})
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.MongoDBCapped(
                    Environment.GetEnvironmentVariable("MONGO_LOGGING"),
                    cappedMaxSizeMb: 50,
                    cappedMaxDocuments: 300
                )
                .CreateLogger();

            var mappingConfig = new MapperConfiguration(mc =>
            {
//                mc.AddProfile(new SkillModelToDtoProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddTransient<IMongoClient>(c =>
                    new MongoClient(Environment.GetEnvironmentVariable("MONGO_SKILLS_DB")));
            services.AddSingleton<IAdapter, MongoDbAdapter>();
            services.AddSingleton<IRepository<Skill>, SkillsRepository>();
        }

        private void SetupEnvironmentVariables()
        {
            if (Environment.GetEnvironmentVariable("IN_DOCKER") == "yes") return;
            Environment.SetEnvironmentVariable("MONGO_LOGGING", Configuration["MongoDBCapped:Host"]);
            Environment.SetEnvironmentVariable("MONGO_SKILLS_DB", Configuration["MongoDb:Host"]);
            Environment.SetEnvironmentVariable("MONGO_SKILLS_DB_NAME", Configuration["MongoDb:DatabaseName"]);
            Environment.SetEnvironmentVariable("MONGO_SKILLS_COLLECTION", Configuration["MongoDb:CollectionName"]);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMiddleware<ExceptionHandlingMiddleware>(_logger);
            app.UseMvc();
            app.Run(async context => await NotFoundMiddleware.Process(context));
        }
    }
}