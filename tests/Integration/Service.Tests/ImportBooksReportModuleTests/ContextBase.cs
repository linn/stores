namespace Linn.Stores.Service.Tests.ImportBooksReportModuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules.Reports;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IEuCreditInvoicesReportFacadeService FacadeService { get; private set; }

        protected IEuCreditInvoicesReportService MockDomainService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.MockDomainService = Substitute.For<IEuCreditInvoicesReportService>();

            this.MockDomainService.GetReport(
                new DateTime(2022, 06, 01), 
                new DateTime(2022, 08, 31)).Returns(new ResultsModel
                {
                    ReportTitle = new NameModel("EU Credit Invoices")
                });
            this.FacadeService = new EuCreditInvoicesReportFacadeService(this.MockDomainService);
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.FacadeService);
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());

                        with.Module<EuCreditInvoicesReportModule>();

                        with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                        with.ResponseProcessor<IEnumerableCsvResponseProcessor>();

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
