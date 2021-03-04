namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportRsnResourceBuilder : IResourceBuilder<ExportRsn>
    {
        public ExportRsnResource Build(ExportRsn exportRsn)
        {
            return new ExportRsnResource
                       {
                           RsnNumber = exportRsn.RsnNumber,
                           ReasonCodeAlleged = exportRsn.ReasonCodeAlleged,
                           DateEntered = exportRsn.DateEntered?.ToString("o"),
                           Quantity = exportRsn.Quantity,
                           ArticleNumber = exportRsn.ArticleNumber,
                           AccountId = exportRsn.AccountId,
                           OutletName = exportRsn.OutletName,
                           OutletNumber = exportRsn.OutletNumber,
                           Country = exportRsn.Country,
                           CountryName = exportRsn.CountryName,
                           AccountType = exportRsn.AccountType
                       };
        }

        public string GetLocation(ExportRsn model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<ExportRsn>.Build(ExportRsn exportRsn) => this.Build(exportRsn);
    }
}