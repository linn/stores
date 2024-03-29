﻿namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeFilterService<Parcel, int, ParcelResource, ParcelResource, ParcelSearchRequestResource> ParcelsFacadeService { get; private set; }

        protected IRepository<Parcel, int> ParcelRepository { get; private set; }

        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ParcelsFacadeService = Substitute.For<IFacadeFilterService<Parcel, int, ParcelResource, ParcelResource, ParcelSearchRequestResource>>();

            this.ParcelRepository = Substitute.For<IRepository<Parcel, int>>();

            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ParcelsFacadeService);
                    with.Dependency(this.ParcelRepository);
                    with.Dependency(this.AuthorisationService);
                    with.Dependency<IResourceBuilder<Parcel>>(new ParcelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Parcel>>>(new ParcelsResourceBuilder());
                    with.Module<ParcelsModule>();
                    with.ResponseProcessor<ParcelResponseProcessor>();
                    with.ResponseProcessor<ParcelsResponseProcessor>();
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
