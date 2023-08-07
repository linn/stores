namespace Linn.Stores.Service.Tests.QcPartsReportModuleSpecs
{
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
        protected IQcPartsReportFacadeService FacadeService { get; private set; }

        protected IQcPartsReportService MockDomainService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.MockDomainService = Substitute.For<IQcPartsReportService>();
            this.FacadeService = new QcPartsReportFacadeService(this.MockDomainService);

            this.MockDomainService.GetReport().Returns(new ResultsModel { ReportTitle = new NameModel("Title") });
            
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.FacadeService);
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());

                        with.Module<QcPartsReportModule>();

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
