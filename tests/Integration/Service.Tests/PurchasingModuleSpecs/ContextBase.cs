namespace Linn.Stores.Service.Tests.PurchasingModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Facade.ResourceBuilders.Purchasing;
    using Linn.Stores.Resources.Purchasing;
    using Linn.Stores.Service.Modules.Purchasing;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected 
            IFacadeFilterService<PlCreditDebitNote, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource> 
            DebitNoteService
        {
            get;
            private set;
        }

        protected IRepository<PlCreditDebitNote, int> DebitNoteRepository 
        { 
            get; 
            private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.DebitNoteService = Substitute
                .For<IFacadeFilterService<PlCreditDebitNote, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource>>();
            this.DebitNoteRepository = Substitute.For<IRepository<PlCreditDebitNote, int>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.DebitNoteService);
                        with.Dependency<IResourceBuilder<PlCreditDebitNote>>(new PlCreditDebitNoteResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PlCreditDebitNote>>>(new PlCreditDebitNotesResourceBuilder());
                        with.Dependency(this.DebitNoteRepository);
                        with.Module<PurchasingModule>();

                        with.ResponseProcessor<PlCreditDebitNoteResponseProcessor>();
                        with.ResponseProcessor<PlCreditDebitNotesResponseProcessor>();
                        with.RequestStartup(
                            (container, pipelines, context) =>
                                {
                                    var claims = new List<Claim>
                                                     {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim("employee", "employees/123"),
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
