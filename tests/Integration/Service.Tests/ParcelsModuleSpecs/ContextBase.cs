namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeWithSearchReturnTen<Parcel, int, ParcelResource, ParcelResource> ParcelsService { get; private set; }

        protected IQueryRepository<Parcel> ParcelRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.ParcelsService = Substitute
                .For<IFacadeWithSearchReturnTen<Parcel, int, ParcelResource, ParcelResource>>();

            this.ParcelRepository = Substitute
                .For<IQueryRepository<Parcel>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ParcelsService);
                    with.Dependency(this.ParcelRepository);
                    with.Dependency<IResourceBuilder<Parcel>>(new ParcelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Parcel>>>(new ParcelsResourceBuilder());
                    with.Module<ParcelsModule>();
                    with.ResponseProcessor<ParcelResponseProcessor>();
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