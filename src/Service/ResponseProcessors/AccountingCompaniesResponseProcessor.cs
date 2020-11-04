namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Domain.LinnApps;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class AccountingCompaniesResponseProcessor : JsonResponseProcessor<IEnumerable<AccountingCompany>>
    {
        public AccountingCompaniesResponseProcessor(IResourceBuilder<IEnumerable<AccountingCompany>> resourceBuilder)
            : base(resourceBuilder, "accounting-companies", 1)
        {
        }
    }
}
