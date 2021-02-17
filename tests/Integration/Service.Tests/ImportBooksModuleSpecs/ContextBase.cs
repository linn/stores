namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource> importBooksFacadeService
        {
            get; private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.importBooksFacadeService =
                Substitute.For<IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.importBooksFacadeService);
                        with.Dependency<IResourceBuilder<ImportBook>>(new ImportBookResourceBuilder());
                        with.Module<ImportBooksModule>();
                        with.ResponseProcessor<ImportBookResponseProcessor>();
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
