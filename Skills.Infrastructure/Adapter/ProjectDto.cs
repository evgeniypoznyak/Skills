using System.Collections.Generic;

namespace Skills.Infrastructure.Adapter
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string Repository { get; set; }
        public List<string> Images { get; set; }
        public List<string> Videos { get; set; }
        public string Description { get; set; }
    }
}


/*
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string Repository { get; set; }
        public  List<string> Images { get; set; }
        public  List<string> Videos { get; set; }
        public string Description { get; set; }
        */