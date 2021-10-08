namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class LoanResourceBuilder : IResourceBuilder<Loan>
    {
        public LoanResource Build(Loan loan)
        {
            return new LoanResource { LoanNumber = loan.LoanNumber };
        }

        public string GetLocation(Loan loan)
        {
            return $"/logistics/loans/{loan.LoanNumber}";
        }

        object IResourceBuilder<Loan>.Build(Loan loan)
        {
            return this.Build(loan);
        }
    }
}
