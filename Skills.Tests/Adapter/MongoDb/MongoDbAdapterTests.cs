using System;
using MongoDB.Driver;
using Newtonsoft.Json;
using Skills.Infrastructure.Adapter.MongoDb;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Tests.Adapter.MongoDb
{
    public class MongoDbAdapterTests
    {
        private readonly ITestOutputHelper _output;

        public MongoDbAdapterTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact 
            (Skip = "Skipped because it's making real call")
        ]
        public void TestConnection_WhenRealCall_ShouldWork()
        {
            try
            {
                Environment.SetEnvironmentVariable("MONGODB_HOST", "mongodb://localhost:8001/");
                Environment.SetEnvironmentVariable("MONGODB_DATABASE", "SkillsDb");
                Environment.SetEnvironmentVariable("MONGODB_COLLECTION", "skills");
                
                var host = Environment.GetEnvironmentVariable("MONGODB_HOST");
                var databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE");
                var collectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION");
                
                var client = new MongoClient(host);
                var database = client.GetDatabase(databaseName);
                var collection = database.GetCollection<SkillModel>(collectionName);

                var result = collection.Find( s => true)
                    .ToListAsync()
                    .Result;
                
                _output.WriteLine(JsonConvert.SerializeObject(result));
                
            }
            catch (Exception e)
            {
                _output.WriteLine("Error: " + e.Message);
            }
         
        }
        
    }
}