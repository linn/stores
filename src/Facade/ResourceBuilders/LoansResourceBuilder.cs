namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class LoansResourceBuilder : IResourceBuilder<IEnumerable<Loan>>
    {
        private readonly LoanResourceBuilder loanResourceBuilder = new LoanResourceBuilder();

        public IEnumerable<LoanResource> Build(IEnumerable<Loan> loanHeaders)
        {
            return loanHeaders.Select(a => this.loanResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<Loan> loanHeaders)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<Loan>>.Build(IEnumerable<Loan> loanHeaders)
        {
            return this.Build(loanHeaders);
        }
    }
}
