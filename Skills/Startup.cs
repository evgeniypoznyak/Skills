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
using Skills.Domain.Dto;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;
using Skills.Infrastructure.Adapter.MongoDb;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddHealthChecks();
            services.AddMvc()
                .AddJsonOptions(options => options.UseCamelCasing(true))
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddTransient<IMongoClient>(c =>
                    new MongoClient(Environment.GetEnvironmentVariable("MONGO_SKILLS_DB")));
            services.AddSingleton<IAdapter<SkillDto>, MongoDbAdapter>();
            services.AddSingleton<IRepository<SkillDto>, SkillsRepository>();
            //Swagger Configuration Setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Skills API MicroService",
                    Version = "v1",
                    Description =
                        "This is the Skills API to pull skills from data service and to be used by NodeJS API Gateway.",
                    Contact = new Contact {Email = "evgeniy.poznyak@gmail.com"}
                });
                c.SwaggerDoc("authorize", new Info
                {
                    Title = "Skills-API-MicroService",
                    Version = "v1",
                    Description = "This is the evgeniy poznyak skill API.",
                    Contact = new Contact {Email = "evgeniy.poznyak@gmail.com"},
                });
                c.SwaggerDoc("health", new Info
                {
                    Title = "Skills-API-MicroService",
                    Version = "v1",
                    Description = "This is the Skills Health API.",
                    Contact = new Contact {Email = "evgeniy.poznyak@gmail.com"},
                });
//                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "SkillApi.xml"));
            });
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
            app.UseHealthChecks("/health");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skill Microservice API V1");
            });
            app.Run(async context => await NotFoundMiddleware.Process(context));
        }
    }
}