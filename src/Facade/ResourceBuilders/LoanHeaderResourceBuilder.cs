namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class LoanHeaderResourceBuilder : IResourceBuilder<LoanHeader>
    {
        public LoanHeaderResource Build(LoanHeader loanHeader)
        {
            return new LoanHeaderResource {LoanNumber = loanHeader.LoanNumber};
        }

        public string GetLocation(LoanHeader loanHeader)
        {
            return $"/logistics/loan-headers/{loanHeader.LoanNumber}";
        }

        object IResourceBuilder<LoanHeader>.Build(LoanHeader loanHeader)
        {
            return this.Build(loanHeader);
        }
    }
}
