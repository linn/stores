namespace Linn.Stores.Facade.Tests.PartTemplateFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected PartTemplateService Sut { get; private set; }

        protected IRepository<PartTemplate, string> PartTemplateRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PartTemplateRepository = Substitute.For<IRepository<PartTemplate, string>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new PartTemplateService(this.PartTemplateRepository, this.TransactionManager);
        }
    }
}
