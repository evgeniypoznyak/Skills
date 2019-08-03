using System.Collections.Generic;
using Skills.Infrastructure.Adapter.MongoDb;

namespace Skills.Infrastructure.Adapter
{
    public interface IAdapter
    {
        List<object> FindAll();
    }
}