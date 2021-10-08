namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class LoanResourceBuilder : IResourceBuilder<Loan>
    {
        public LoanResource Build(Loan loanHeader)
        {
            return new LoanResource {LoanNumber = loanHeader.LoanNumber};
        }

        public string GetLocation(Loan loanHeader)
        {
            return $"/logistics/loan-headers/{loanHeader.LoanNumber}";
        }

        object IResourceBuilder<Loan>.Build(Loan loanHeader)
        {
            return this.Build(loanHeader);
        }
    }
}
