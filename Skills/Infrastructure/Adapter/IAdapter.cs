using System.Collections.Generic;

namespace Skills.Infrastructure.Adapter
{
    public interface IAdapter
    {
        List<object> FindAll();
    }
}