namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.ImportBooks;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IImportBookFacadeService ImportBooksFacadeService { get; private set; }

        protected IImportBookExchangeRateService ImportBookExchangeRateService { get; private set; }

        protected IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource> ImportBookTransactionCodeFacadeService { get; private set; }

        protected IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource> ImportBookTransportCodeFacadeService { get; private set; }

        protected IFacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource, ImportBookCpcNumberResource> ImportBookCpcNumberFacadeService { get; private set; }

        protected IFacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource, ImportBookDeliveryTermResource> ImportBookDeliveryTermFacadeService { get; private set; }

        protected IFacadeService<Port, string, PortResource, PortResource> PortFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ImportBooksFacadeService =
                Substitute.For<IImportBookFacadeService>();
            this.ImportBookExchangeRateService = Substitute.For<IImportBookExchangeRateService>();
            this.ImportBookTransactionCodeFacadeService = Substitute
                .For<IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource>>();
            this.ImportBookTransportCodeFacadeService = Substitute
                .For<IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource>>();
            this.ImportBookCpcNumberFacadeService = Substitute
                .For<IFacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource, ImportBookCpcNumberResource>>();
            this.ImportBookDeliveryTermFacadeService = Substitute
                .For<IFacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource, ImportBookDeliveryTermResource>>();
            this.PortFacadeService = Substitute.For<IFacadeService<Port, string, PortResource, PortResource>>();

            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency(this.ImportBooksFacadeService);
                with.Dependency(this.ImportBookExchangeRateService);
                with.Dependency(this.ImportBookTransactionCodeFacadeService);
                with.Dependency(this.ImportBookTransportCodeFacadeService);
                with.Dependency(this.ImportBookCpcNumberFacadeService);
                with.Dependency(this.ImportBookDeliveryTermFacadeService);
                with.Dependency(this.PortFacadeService);

                with.Dependency<IResourceBuilder<ImportBook>>(new ImportBookResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBook>>>(new ImportBooksResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookExchangeRate>>(new ImportBookExchangeRateResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookExchangeRate>>>(new ImportBookExchangeRatesResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookTransportCode>>(new ImportBookTransportCodeResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookTransactionCode>>(new ImportBookTransactionCodeResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookTransportCode>>>(new ImportBookTransportCodesResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookTransactionCode>>>(new ImportBookTransactionCodesResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookCpcNumber>>(new ImportBookCpcNumberResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookCpcNumber>>>(new ImportBookCpcNumbersResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<Port>>>(new PortsResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookDeliveryTerm>>(new ImportBookDeliveryTermResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookDeliveryTerm>>>(new ImportBookDeliveryTermsResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookOrderDetail>>>(new ImportBookOrderDetailsResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookOrderDetail>>(new ImportBookOrderDetailResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookPostEntry>>>(new ImportBookPostEntriesResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookPostEntry>>(new ImportBookPostEntryResourceBuilder());
                with.Dependency<IResourceBuilder<IEnumerable<ImportBookInvoiceDetail>>>(new ImportBookInvoiceDetailsResourceBuilder());
                with.Dependency<IResourceBuilder<ImportBookInvoiceDetail>>(new ImportBookInvoiceDetailResourceBuilder());
                with.Dependency<IResourceBuilder<ProcessResult>>(new ProcessResultResourceBuilder());

                with.Module<ImportBooksModule>();

                with.ResponseProcessor<ImportBookResponseProcessor>();
                with.ResponseProcessor<ImportBooksResponseProcessor>();

                with.ResponseProcessor<ImportBookExchangeRateResponseProcessor>();
                with.ResponseProcessor<ImportBookExchangeRatesResponseProcessor>();

                with.ResponseProcessor<ImportBookTransactionCodeResponseProcessor>();
                with.ResponseProcessor<ImportBookTransactionCodesResponseProcessor>();

                with.ResponseProcessor<ImportBookTransportCodeResponseProcessor>();
                with.ResponseProcessor<ImportBookTransportCodesResponseProcessor>();

                with.ResponseProcessor<ImportBookCpcNumbersResponseProcessor>();
                with.ResponseProcessor<PortsResponseProcessor>();
                with.ResponseProcessor<ImportBookDeliveryTermsResponseProcessor>();

                with.ResponseProcessor<ProcessResultResponseProcessor>();

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
