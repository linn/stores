namespace Linn.Stores.Service.Tests.StoragePlaceAuditModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IStoragePlaceAuditReportFacadeService StoragePlaceAuditReportFacadeService { get; private set; }

        protected IAuditLocationService AuditLocationService { get; private set; }

        protected IStoragePlaceService StoragePlaceService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StoragePlaceAuditReportFacadeService = Substitute.For<IStoragePlaceAuditReportFacadeService>();
            this.AuditLocationService = Substitute.For<IAuditLocationService>();
            this.StoragePlaceService = Substitute.For<IStoragePlaceService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.StoragePlaceAuditReportFacadeService);
                        with.Dependency(this.AuditLocationService);
                        with.Dependency(this.StoragePlaceService);
                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<AuditLocation>>>(
                            new AuditLocationsResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<StoragePlace>>>(
                            new StoragePlacesResourceBuilder());
                        with.Dependency<IResourceBuilder<Error>>(new ErrorResourceBuilder());
                        with.ResponseProcessor<AuditLocationsResponseProcessor>();
                        with.ResponseProcessor<StoragePlacesResponseProcessor>();
                        with.ResponseProcessor<ResultsModelJsonResponseProcessor>();
                        with.ResponseProcessor<ErrorResponseProcessor>();
                        with.Module<StoragePlaceModule>();
                        with.RequestStartup(
                            (container, pipelines, context) =>
                                {
                                    var claims = new List<Claim>
                                                     {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user"),
                                                         new Claim("employee", "/e/33607")
                                                     };
                                    var user = new ClaimsIdentity(claims, "jwt");

                                    context.CurrentUser = new ClaimsPrincipal(user);
                                });
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
    }
