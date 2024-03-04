namespace Linn.Stores.Service.Tests.StoresBudgetModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeFilterService<StoresBudgetPosting, StoresBudgetPostingKey, StoresBudgetPostingResource, StoresBudgetPostingResource, StoresBudgetPostingResource> StoresBudgetPostingsFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StoresBudgetPostingsFacadeService = Substitute.For<IFacadeFilterService<StoresBudgetPosting, StoresBudgetPostingKey, StoresBudgetPostingResource, StoresBudgetPostingResource, StoresBudgetPostingResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.StoresBudgetPostingsFacadeService);
                    with.Dependency<IResourceBuilder<IEnumerable<StoresBudgetPosting>>>(new StoresBudgetPostingsResourceBuilder());
                    with.Module<StoresBudgetModule>();
                    with.ResponseProcessor<StoresBudgetPostingsResponseProcessor>();
                    with.RequestStartup(
                        (container, pipelines, context) =>
                        {
                            var claims = new List<Claim>
                                                 {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user")
                                                 };
                            var user = new ClaimsIdentity(claims, "jwt");

                            context.CurrentUser = new ClaimsPrincipal(user);
                        });
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}
