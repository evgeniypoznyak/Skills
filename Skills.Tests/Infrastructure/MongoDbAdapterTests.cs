using System;
using MongoDB.Driver;
using Newtonsoft.Json;
using Skills.Domain.Dto;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Tests.Infrastructure
{
    public class MongoDbAdapterTests
    {
        private ITestOutputHelper _output;

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
                var host = "mongodb://localhost:8001/";
                var databaseName = "Skills";
                var collectionName = "skills";

                var client = new MongoClient(host);
                var database = client.GetDatabase(databaseName);
                var collection = database.GetCollection<SkillDto>(collectionName);

                
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