namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.Responses;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/inventory"));
            this.Get("/inventory", _ => this.GetApp());
            this.Get("/inventory/(.*)/create", _ => this.GetApp());

            this.Get("/inventory/signin-oidc-client", _ => this.GetApp());
            this.Get("/inventory/signin-oidc-silent", _ => this.SilentRenew());

            this.Get(@"^(.*)$", _ => this.GetApp());
        }

        public object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object SilentRenew()
        {
            return this.Negotiate.WithView("silent-renew");
        }
    }
}
