namespace Linn.Stores.Service.Tests.TqmsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Tqms;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected ITqmsReportsFacadeService TqmsReportsFacadeService { get; private set; }

        protected ISingleRecordFacadeService<TqmsMaster, TqmsMasterResource> TqmsMasterFacadeService { get; private set; }

        protected IFacadeService<TqmsJobRef, string, TqmsJobRefResource, TqmsJobRefResource> TqmsJobRefFacadeService { get; private set; }

        protected IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource> TqmsCategoryFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.TqmsReportsFacadeService = Substitute.For<ITqmsReportsFacadeService>();
            this.TqmsMasterFacadeService = Substitute.For<ISingleRecordFacadeService<TqmsMaster, TqmsMasterResource>>();
            this.TqmsJobRefFacadeService = Substitute.For<IFacadeService<TqmsJobRef, string, TqmsJobRefResource, TqmsJobRefResource>>();
            this.TqmsCategoryFacadeService = Substitute.For<IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.TqmsReportsFacadeService);
                        with.Dependency(this.TqmsMasterFacadeService);
                        with.Dependency(this.TqmsJobRefFacadeService);
                        with.Dependency(this.TqmsCategoryFacadeService);
                        with.Dependency<IResourceBuilder<IEnumerable<ResultsModel>>>(new ResultsModelsResourceBuilder());
                        with.Dependency<IResourceBuilder<TqmsMaster>>(new TqmsMasterResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<TqmsCategory>>>(new TqmsCategoriesResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<TqmsJobRef>>>(new TqmsJobRefsResourceBuilder());
                        with.Module<TqmsModule>();
                        with.ResponseProcessor<ResultsModelsJsonResponseProcessor>();
                        with.ResponseProcessor<TqmsMasterResponseProcessor>();
                        with.ResponseProcessor<TqmsJobRefsResponseProcessor>();
                        with.ResponseProcessor<TqmsCategoriesResponseProcessor>();
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
