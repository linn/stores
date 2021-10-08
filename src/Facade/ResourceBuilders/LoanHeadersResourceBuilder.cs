namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class LoanHeadersResourceBuilder : IResourceBuilder<IEnumerable<LoanHeader>>
    {
        private readonly LoanHeaderResourceBuilder loanHeaderResourceBuilder = new LoanHeaderResourceBuilder();

        public IEnumerable<LoanHeaderResource> Build(IEnumerable<LoanHeader> loanHeaders)
        {
            return loanHeaders.Select(a => this.loanHeaderResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<LoanHeader> loanHeaders)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<LoanHeader>>.Build(IEnumerable<LoanHeader> loanHeaders)
        {
            return this.Build(loanHeaders);
        }
    }
}
