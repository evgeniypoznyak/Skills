using System;
using System.IO;
using AccountDataExtractService.Middleware;
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
using Skills.Middleware;
using Skills.Repository;
using Skills.Adapter;
using Skills.Adapter.MongoDb;
using Skills.Aggregate;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetupEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                .Destructure.ByTransforming<HttpRequest>(
                    r => new {Body = r.Body, Headers = r.Headers, Method = r.Method})
                .MinimumLevel.Debug()
                .WriteTo.Console()
//                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MongoDBCapped(
                    Environment.GetEnvironmentVariable("MONGO_LOGGING"),
                    cappedMaxSizeMb: 50,
                    cappedMaxDocuments: 300,
                    collectionName: "log"
                )
                .CreateLogger();

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new SkillProfile()); });
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
            if (Environment.GetEnvironmentVariable("MONGO_LOGGING") == null)
            {
                Environment.SetEnvironmentVariable("MONGO_LOGGING", Configuration["MongoDBCapped:Host"]);
            }

            if (Environment.GetEnvironmentVariable("MONGO_SKILLS_DB") == null)
            {
                Environment.SetEnvironmentVariable("MONGO_SKILLS_DB", Configuration["MongoDb:Host"]);
            }

            if (Environment.GetEnvironmentVariable("MONGO_SKILLS_DB_NAME") == null)
            {
                Environment.SetEnvironmentVariable("MONGO_SKILLS_DB_NAME", Configuration["MongoDb:DatabaseName"]);
            }

            if (Environment.GetEnvironmentVariable("MONGO_SKILLS_COLLECTION") == null)
            {
                Environment.SetEnvironmentVariable("MONGO_SKILLS_COLLECTION", Configuration["MongoDb:CollectionName"]);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>(_logger);
            app.UseMvc();
            app.Run(async context => await NotFoundMiddleware.Process(context));
        }
    }
}