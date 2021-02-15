namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookOrderDetailResourceBuilder : IResourceBuilder<ImpBookOrderDetail>
    {
        public ImportBookOrderDetailResource Build(ImpBookOrderDetail model)
        {
            return new ImportBookOrderDetailResource
                       {
                           ImportBookId = model.ImpBookId,
                           LineNumber = model.LineNumber,
                           OrderNumber = model.OrderNumber,
                           RsnNumber = model.RsnNumber,
                           OrderDescription = model.OrderDescription,
                           Qty = model.Qty,
                           DutyValue = model.DutyValue,
                           FreightValue = model.FreightValue,
                           VatValue = model.VatValue,
                           OrderValue = model.OrderValue,
                           Weight = model.Weight,
                           LoanNumber = model.LoanNumber,
                           LineType = model.LineType,
                           CpcNumber = model.CpcNumber,
                           TariffCode = model.TariffCode,
                           InsNumber = model.InsNumber,
                           VatRate = model.VatRate
                       };
        }

        object IResourceBuilder<ImpBookOrderDetail>.Build(ImpBookOrderDetail model) => this.Build(model);

        public string GetLocation(ImpBookOrderDetail model)
        {
            throw new System.NotImplementedException();
        }
    }
}