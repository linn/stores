namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportReturnDetailResourceBuilder : IResourceBuilder<ExportReturnDetail>
    {
        public ExportReturnDetailResource Build(ExportReturnDetail exportReturnDetail)
        {
            return new ExportReturnDetailResource
                       {
                           ReturnId = exportReturnDetail.ReturnId,
                           RsnNumber = exportReturnDetail.RsnNumber,
                           ArticleNumber = exportReturnDetail.ArticleNumber,
                           LineNo = exportReturnDetail.LineNo,
                           Qty = exportReturnDetail.Qty,
                           Description = exportReturnDetail.Description,
                           CustomsValue = exportReturnDetail.CustomsValue,
                           BaseCustomsValue = exportReturnDetail.BaseCustomsValue,
                           TariffId = exportReturnDetail.TariffId,
                           ExpInvDocumentType = exportReturnDetail.ExpInvDocumentType,
                           ExpInvDocumentNumber = exportReturnDetail.ExpInvDocumentNumber,
                           ExpInvDate = exportReturnDetail.ExpInvDate?.ToString("o"),
                           NumCartons = exportReturnDetail.NumCartons,
                           Weight = exportReturnDetail.Weight,
                           Width = exportReturnDetail.Width,
                           Height = exportReturnDetail.Height,
                           Depth = exportReturnDetail.Depth
                       };
        }

        public string GetLocation(ExportReturnDetail model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<ExportReturnDetail>.Build(ExportReturnDetail exportReturnDetail) =>
            this.Build(exportReturnDetail);
    }
}