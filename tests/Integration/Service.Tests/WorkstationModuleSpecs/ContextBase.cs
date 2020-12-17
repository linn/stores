﻿namespace Linn.Stores.Service.Tests.WorkstationModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IWorkstationFacadeService WorkstationFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WorkstationFacadeService = Substitute.For<IWorkstationFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.WorkstationFacadeService);
                    with.Dependency<IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>>(new WorkstationTopUpStatusResourceBuilder());
                    with.Module<WorkstationModule>();
                    with.ResponseProcessor<WorkstationTopUpStatusResponseProcessor>();
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
