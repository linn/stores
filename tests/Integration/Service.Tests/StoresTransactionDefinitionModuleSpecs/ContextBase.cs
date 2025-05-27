namespace Linn.Stores.Service.Tests.StoresTransactionDefinitionModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<StoresTransactionDefinition, string, StoresTransactionDefinitionResource, StoresTransactionDefinitionResource> StoresTransactionDefinitionFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StoresTransactionDefinitionFacadeService = Substitute.For<IFacadeService<StoresTransactionDefinition, string, StoresTransactionDefinitionResource, StoresTransactionDefinitionResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.StoresTransactionDefinitionFacadeService);
                        with.Dependency<IResourceBuilder<StoresTransactionDefinition>>(new StoresTransactionDefinitionResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<StoresTransactionDefinition>>>(new StoresTransactionDefinitionsResourceBuilder());
                        with.Module<StoresTransactionDefinitionModule>();
                        with.ResponseProcessor<StoresTransactionDefinitionsResponseProcessor>();
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
