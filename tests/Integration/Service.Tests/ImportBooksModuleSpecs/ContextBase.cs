namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource> ImportBooksFacadeService
        {
            get;
            private set;
        }

        protected IImportBookExchangeRateService ImportBookExchangeRateService;

        protected IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource> ImportBookTransactionCodeFacadeService;

        protected IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource> ImportBookTransportCodeFacadeService;

        [SetUp]
        public void EstablishContext()
        {
            this.ImportBooksFacadeService =
                Substitute.For<IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource>>();
            this.ImportBookExchangeRateService = Substitute.For<IImportBookExchangeRateService>();
            this.ImportBookTransactionCodeFacadeService = Substitute.For<IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource>>();
            this.ImportBookTransportCodeFacadeService = Substitute.For<IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.ImportBooksFacadeService);
                        with.Dependency(this.ImportBookExchangeRateService);
                        with.Dependency(this.ImportBookTransactionCodeFacadeService);
                        with.Dependency(this.ImportBookTransportCodeFacadeService);
                        with.Dependency<IResourceBuilder<ImportBook>>(new ImportBookResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<ImportBook>>>(new ImportBooksResourceBuilder());
                        with.Dependency<IResourceBuilder<ImportBookExchangeRate>>(
                            new ImportBookExchangeRateResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<ImportBookExchangeRate>>>(
                            new ImportBookExchangeRatesResourceBuilder());

                        with.Dependency<IResourceBuilder<ImportBookTransportCode>>(
                            new ImportBookTransportCodeResourceBuilder());
                        with.Dependency<IResourceBuilder<ImportBookTransactionCode>>(
                            new ImportBookTransactionCodeResourceBuilder());


                        with.Dependency<IResourceBuilder<IEnumerable<ImportBookTransportCode>>>(
                            new ImportBookTransportCodesResourceBuilder());

                        with.Dependency<IResourceBuilder<IEnumerable<ImportBookTransactionCode>>>(
                            new ImportBookTransactionCodesResourceBuilder());

                        with.Module<ImportBooksModule>();

                        with.ResponseProcessor<ImportBookResponseProcessor>();
                        with.ResponseProcessor<ImportBooksResponseProcessor>();

                        with.ResponseProcessor<ImportBookExchangeRateResponseProcessor>();
                        with.ResponseProcessor<ImportBookExchangeRatesResponseProcessor>();

                        with.ResponseProcessor<ImportBookTransactionCodeResponseProcessor>();
                        with.ResponseProcessor<ImportBookTransactionCodesResponseProcessor>();

                        with.ResponseProcessor<ImportBookTransportCodeResponseProcessor>();
                        with.ResponseProcessor<ImportBookTransportCodesResponseProcessor>();

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
