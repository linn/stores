﻿namespace Linn.Stores.Service.Tests.WhatWillDecrementReportModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules.Reports;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IWhatWillDecrementReportFacadeService WhatWillDecrementReportFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WhatWillDecrementReportFacadeService = Substitute.For<IWhatWillDecrementReportFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.WhatWillDecrementReportFacadeService);

                        with.Dependency<IResourceBuilder<ResultsModel>>(new ResultsModelResourceBuilder());

                        with.Module<WhatWillDecrementReportModule>();

                        with.ResponseProcessor<ResultsModelJsonResponseProcessor>();

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
