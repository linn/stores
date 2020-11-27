namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;

    public static class DefaultHeaders
    {
        public static IDictionary<string, string[]> JsonHeaders()
        {
            return new Dictionary<string, string[]> { { "Accept", new[] { "application/json" } }, { "Content-Type", new[] { "application/json" } } };
        }

        public static IDictionary<string, string[]> JsonGetHeaders()
        {
            return new Dictionary<string, string[]> { { "Accept", new[] { "application/json" } } };
        }
    }
}
