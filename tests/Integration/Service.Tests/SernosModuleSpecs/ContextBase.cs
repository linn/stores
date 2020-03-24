namespace Linn.Stores.Service.Tests.SernosModuleSpecs
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
        protected ISernosSequencesService SernosSequencesService { get; private set; }

        protected IQueryRepository<SernosSequence> SernosSequenceRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.SernosSequencesService = Substitute
                .For<ISernosSequencesService>();

            this.SernosSequenceRepository = Substitute
                .For<IQueryRepository<SernosSequence>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.SernosSequencesService);
                    with.Dependency(this.SernosSequenceRepository);
                    with.Dependency<IResourceBuilder<SernosSequence>>(new SernosSequenceResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<SernosSequence>>>(new SernosSequencesResourceBuilder());
                    with.Module<SernosModule>();
                    with.ResponseProcessor<SernosSequencesResponseProcessor>();
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