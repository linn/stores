namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartResourceBuilder : IResourceBuilder<Part>
    {
        private readonly PartDataSheetResourceBuilder dataSheetResourceBuilder = new PartDataSheetResourceBuilder();

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
                           SafetyCriticalPart = part.SafetyCriticalPart,
                           ImdsIdNumber = part.ImdsIdNumber,
                           ParetoCode = part.ParetoClass?.ParetoCode,
                           ParetoDescription = part.ParetoClass?.Description,
                           ImdsWeight = part.ImdsWeight,
                           DecrementRuleName = part.DecrementRule?.Rule,
                           DecrementRuleDescription = part.DecrementRule?.Description,
                           SparesRequirement = part.SparesRequirement,
                           BomType = part.BomType,
                           BomVerifyFreqWeeks= part.BomVerifyFreqWeeks,
                           AccountingCompany = part.AccountingCompany?.Name,
                           AccountingCompanyDescription = part.AccountingCompany?.Description,
                           OptionSet = part.OptionSet,
                           MaterialPrice = part.MaterialPrice,
                           SingleSourcePart = part.SingleSourcePart,
                           StockControlled = part.StockControlled,
                           LinnProduced = part.LinnProduced,
                           IgnoreWorkstationStock = part.IgnoreWorkstationStock,
                           EmcCriticalPart = part.EmcCriticalPart,
                           Currency = part.Currency,
                           LabourPrice = part.LabourPrice,
                           CostingPrice = part.CostingPrice,
                           OrderHold = part.OrderHold,
                           PlannedSurplus = part.PlannedSurplus,
                           PsuPart = part.PsuPart,
                           CurrencyUnitPrice = part.CurrencyUnitPrice,
                           CccCriticalPart = part.CccCriticalPart,
                           DrawingReference = part.DrawingReference,
                           SafetyDataDirectory = part.SafetyDataDirectory,
                           BomId = part.BomId,
                           BaseUnitPrice = part.BaseUnitPrice,
                           OurUnitOfMeasure = part.OurUnitOfMeasure,
                           PerformanceCriticalPart = part.PerformanceCriticalPart,
                           RootProduct = part.RootProduct,
                           PreferredSupplier = part.PreferredSupplier?.Id,
                           PreferredSupplierName = part.PreferredSupplier?.Name,
                           SecondStageDescription = part.SecondStageDescription,
                           RailMethod = part.RailMethod,
                           PlannerStory = part.PlannerStory,
                           DatePhasedOut = part.DatePhasedOut?.ToString("o"),
                           DateLive = part.DateLive?.ToString("o"),
                           StockNotes = part.StockNotes,
                           PurchasingPhaseOutType = part.PurchasingPhaseOutType,
                           QcInformation = part.QcInformation,
                           DateDesignObsolete = part.DateDesignObsolete?.ToString("o"),
                           ScrapOrConvert = part.ScrapOrConvert,
                           SafetyWeeks = part.SafetyWeeks,
                           RawOrFinished = part.RawOrFinished,
                           CreatedBy = part.CreatedBy?.Id,
                           CreatedByName = part.CreatedBy?.FullName,
                           PhasedOutBy = part.PhasedOutBy?.Id,
                           PhasedOutByName = part.PhasedOutBy?.FullName,
                           MadeLiveBy = part.MadeLiveBy?.Id,
                           MadeLiveByName = part.MadeLiveBy?.FullName,
                           SecondStageBoard = part.SecondStageBoard,
                           ReasonPhasedOut = part.ReasonPhasedOut,
                           DateCreated = part.DateCreated?.ToString("o"),
                           QcOnReceipt = part.QcOnReceipt,
                           TqmsCategoryOverride = part.TqmsCategoryOverride,
                           OurInspectionWeeks = part.OurInspectionWeeks,
                           MaxStockRail = part.MaxStockRail,
                           MinStockRail = part.MinStockRail,
                           NominalAccount = part.NominalAccount?.NominalAccountId,
                           Nominal = part.NominalAccount?.Nominal?.NominalCode,
                           NominalDescription = part.NominalAccount?.Nominal?.Description,
                           Department = part.NominalAccount?.Department?.DepartmentCode,
                           DepartmentDescription = part.NominalAccount?.Department?.Description,
                           SernosSequenceName = part.SernosSequence?.Sequence,
                           SernosSequenceDescription = part.SernosSequence?.Description,
                           AssemblyTechnologyName = part.AssemblyTechnology?.Name,
                           AssemblyTechnologyDescription = part.AssemblyTechnology?.Description,
                           NonForecastRequirement = part.NonForecastRequirement,
                           OneOffRequirement = part.OneOffRequirement,
                           DataSheets = part.DataSheets?.Select(s => this.dataSheetResourceBuilder.Build(s)).OrderBy(s => s.Sequence),
                           ParamData = this.BuildParamDataResource(part.ParamData),
                           Manufacturers = part.MechPartSource?.MechPartManufacturerAlts?.Select(
                               m => new MechPartManufacturerAltResource
                                        {
                                            PartNumber = m.PartNumber,
                                            ManufacturerCode = m.ManufacturerCode,
                                            ManufacturerDescription = m.Manufacturer?.Description,
                                            Preference = m.Preference
                                        }).OrderBy(m => m.Preference),
                           Links = this.BuildLinks(part).ToArray(),
                           SalesArticleNumber = part.SalesArticle?.ArticleNumber,
                           SourceId = part.MechPartSource?.Id,
                           LibraryName = part.LibraryName,
                           LibraryRef = part.LibraryRef,
                           FootprintRef1 = part.FootprintRef1,
                           FootprintRef2 = part.FootprintRef2,
                           FootprintRef3 = part.FootprintRef3,
                           AltiumType = part.AltiumType,
                           DatasheetPath = part.DataSheetPdfPath,
                           TheirPartNumber = part.ManufacturersPartNumber,
                           TemperatureCoefficient = part.TemperatureCoefficient,
                           Device = part.Device,
                           AltiumValueRkm = part.AltiumValueRkm,
                           Dielectric = part.Dielectric,
                           Construction = part.Construction,
                           CapNegativeTolerance = part.CapNegativeTolerance,
                           CapPositiveTolerance = part.CapPositiveTolerance,
                           CapVoltageRating = part.CapVoltageRating,
                           Frequency = part.Frequency,
                           FrequencyLabel = part.FrequencyLabel,
                           SimKind = part.SimKind,
                           SimSubKind = part.SimSubKind,
                           SimModelName = part.SimModelName,
                           AltiumValue = part.AltiumValue,
                           ResistorTolerance = part.ResistorTolerance
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
            if (part.MechPartSource != null)
            {
                yield return new LinkResource
                {
                    Rel = "mechanical-sourcing-sheet",
                    Href = $"/parts/sources/{part.MechPartSource.Id}"
                };
            }

            yield return new LinkResource 
                             {
                                 Rel = "stock-locators",
                                 Href = $"/inventory/stock-locators?partId={part.Id}"
                             };

            if (part.BomId.HasValue)
            {
                yield return new LinkResource
                                 {
                                     Rel = "bom-tree",
                                     Href =
                                         $"/purchasing/boms/tree/options?bomName={part.PartNumber}"
                                 };
            }

            if (part.PreferredSupplierId.HasValue && part.PreferredSupplierId != 4415)
            {
                yield return new LinkResource
                                 {
                                     Rel = "part-supplier",
                                     Href =
                                         $"/purchasing/part-suppliers/record?partId={part.Id}&supplierId={part.PreferredSupplierId}"
                                 };
            }
            else
            {
                yield return new LinkResource
                                 {
                                     Rel = "part-supplier",
                                     Href = $"/purchasing/part-suppliers/"
                                 };
            }
        }

        private PartParamDataResource BuildParamDataResource(PartParamData entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new PartParamDataResource
                       {
                           AttributeSet = entity.AttributeSet,
                           Capacitance = entity.Capacitance,
                           Construction = entity.Construction,
                           Current = entity.Current,
                           Diameter = entity.Diameter,
                           Dielectric = entity.Dielectric,
                           Height = entity.Height,
                           IcFunction = entity.IcFunction,
                           IcType = entity.IcType,
                           Length = entity.Length,
                           NegativeTolerance = entity.NegativeTolerance,
                           Package = entity.Package,
                           TransistorType = entity.TransistorType,
                           Voltage = entity.Voltage,
                           Pitch = entity.Pitch,
                           Width = entity.Width,
                           Polarity = entity.Polarity,
                           Resistance = entity.Resistance,
                           Power = entity.Power,
                           TemperatureCoefficient = entity.TemperatureCoefficient,
                           PartNumber = entity.PartNumber,
                           PositiveTolerance = entity.PositiveTolerance
                       };
        }
    }
}
