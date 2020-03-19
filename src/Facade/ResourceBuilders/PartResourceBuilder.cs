namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Resources;

    public class PartResourceBuilder : IResourceBuilder<Part>
    {
        public PartResource Build(Part part)
        {
            return new PartResource
                       {
                           Id = part.Id,
                           PartNumber = part.PartNumber,
                           Description = part.Description,
                           ProductAnalysisCode = part.ProductAnalysisCode?.ProductCode,
                           ProductAnalysisCodeDescription = part.ProductAnalysisCode?.Description,
                           SafetyCertificateExpirationDate = part.SafetyCertificateExpirationDate?.ToString("o"),
                           SafetyCriticalPart = part.SafetyCriticalPart == "Y",
                           ImdsIdNumber = part.ImdsIdNumber,
                           ParetoCode = part.ParetoClass?.ParetoCode,
                           ParetoDescription = part.ParetoClass?.Description,
                           ImdsWeight = part.ImdsWeight,
                           DecrementRule = part.DecrementRule,
                           SparesRequirement = part.SparesRequirement,
                           BomType = part.BomType,
                           AccountingCompany = part.AccountingCompany,
                           OptionSet = part.OptionSet,
                           MaterialPrice = part.MaterialPrice,
                           SingleSourcePart = this.ToNullableBool(part.SingleSourcePart),
                           StockControlled = this.ToNullableBool(part.StockControlled),
                           LinnProduced = this.ToNullableBool(part.LinnProduced),
                           PartCategory = part.PartCategory,
                           IgnoreWorkstationStock = this.ToNullableBool(part.IgnoreWorkstationStock),
                           EmcCriticalPart = this.ToNullableBool(part.EmcCriticalPart),
                           Currency = part.Currency,
                           LabourPrice = part.LabourPrice,
                           CostingPrice = part.CostingPrice,
                           OrderHold = this.ToNullableBool(part.OrderHold),
                           PlannedSurplus = this.ToNullableBool(part.PlannedSurplus),
                           PsuPart = this.ToNullableBool(part.PsuPart),
                           CurrencyUnitPrice = part.CurrencyUnitPrice,
                           CccCriticalPart = this.ToNullableBool(part.CccCriticalPart),
                           DrawingReference = part.DrawingReference,
                           SafetyDataDirectory = part.SafetyDataDirectory,
                           BomId = part.BomId,
                           BaseUnitPrice = part.BaseUnitPrice,
                           UnitOfMeasure = part.UnitOfMeasure,
                           PerformanceCriticalPart = this.ToNullableBool(part.PerformanceCriticalPart),
                           MechanicalOrElectronic = part.MechanicalOrElectronic,
                           RootProduct = part.RootProduct,
                           PreferredSupplier = part.PreferredSupplier?.Id,
                           PreferredSupplierName = part.PreferredSupplier?.Description,
                           Links = this.BuildLinks(part).ToArray()
                       };
        }

        public string GetLocation(Part part)
        {
            return $"/parts/{part.Id}";
        }

        object IResourceBuilder<Part>.Build(Part part) => this.Build(part);

        private IEnumerable<LinkResource> BuildLinks(Part part)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(part) };
        }

        private bool? ToNullableBool(string yesOrNoString)
        {
            if (yesOrNoString == null)
            {
                return null;
            }

            return yesOrNoString == "Y";
        }
    }
}