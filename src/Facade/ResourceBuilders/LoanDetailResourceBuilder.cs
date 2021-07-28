namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class LoanDetailResourceBuilder : IResourceBuilder<LoanDetail>
    {
        public LoanDetailResource Build(LoanDetail model)
        {
            return new LoanDetailResource
                       {
                           ArticleNumber = model.ArticleNumber,
                           ItemNumber = model.ItemNumber,
                           Line = model.Line,
                           LoanNumber = model.LoanNumber,
                           QtyOnLoan = model.QtyOnLoan,
                           SerialNumber = model.SerialNumber,
                           SerialNumber2 = model.SerialNumber2
                       };
        }

        object IResourceBuilder<LoanDetail>.Build(LoanDetail detail) => this.Build(detail);

        public string GetLocation(LoanDetail model)
        {
            throw new System.NotImplementedException();
        }
    }
}
