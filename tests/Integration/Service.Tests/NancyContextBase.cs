namespace Linn.Stores.Service.Tests
{
    using Nancy.Testing;

    public abstract class NancyContextBase
    {
        protected Browser Browser { get; set; }

        protected BrowserResponse Response { get; set; }
    }
}