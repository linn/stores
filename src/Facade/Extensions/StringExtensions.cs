namespace Linn.Stores.Facade.Extensions
{
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class StringExtensions
    {
        public static int ParseId(this string uri)
        {
            return string.IsNullOrEmpty(uri) ? 0 : int.Parse(uri.Split('/').Last());
        }

        public static bool ContainsIgnoringCase(this string str, string substr)
        {
            return str.ToUpper().Contains(substr.ToUpper());
        }
    }
}
