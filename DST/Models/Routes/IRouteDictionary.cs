using System.Collections.Generic;

namespace DST.Models.Routes
{
    public interface IRouteDictionary<TRoute>
        where TRoute : IRouteDictionary<TRoute>, new()
    {
        TRoute Clone();
        IDictionary<string, string> ToDictionary();
        void Validate();
    }
}
