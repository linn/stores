namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class PartService : IPartService
    {
        public void UpdatePart(Part from, Part to)
        {
            from.Description = to.Description;
            from.AccountingCompany = to.AccountingCompany;
            from.CccCriticalPart = to.CccCriticalPart;
            from.EmcCriticalPart = to.EmcCriticalPart;
            from.SafetyCriticalPart = to.SafetyCriticalPart;
            from.PlannedSurplus = to.PlannedSurplus;
            from.OurUnitOfMeasure = to.OurUnitOfMeasure;
            from.SingleSourcePart = to.SingleSourcePart;
            from.StockControlled = to.StockControlled;
            from.ParetoClass = to.ParetoClass;
            from.PerformanceCriticalPart = to.PerformanceCriticalPart;
            from.ProductAnalysisCode = to.ProductAnalysisCode;
            from.PsuPart = to.PsuPart;
            from.RootProduct = to.RootProduct;
            from.SafetyCertificateExpirationDate = to.SafetyCertificateExpirationDate;
            from.SafetyDataDirectory = to.SafetyDataDirectory;
            from.NominalAccount = to.NominalAccount;
            from.DecrementRule = to.DecrementRule;
            from.AssemblyTechnology = to.AssemblyTechnology;
            from.OptionSet = to.OptionSet;
            from.DrawingReference = to.DrawingReference;
            from.BomType = to.BomType;
            from.BomId = to.BomId;
            from.SernosSequence = to.SernosSequence;
            from.IgnoreWorkstationStock = to.IgnoreWorkstationStock;
            from.MechanicalOrElectronic = to.MechanicalOrElectronic;
            from.ImdsIdNumber = to.ImdsIdNumber;
            from.ImdsWeight = to.ImdsWeight;
            from.PartCategory = to.PartCategory;
            from.OrderHold = to.OrderHold;
            from.MaterialPrice = to.MaterialPrice;
            from.SparesRequirement = to.SparesRequirement;
            from.CurrencyUnitPrice = to.CurrencyUnitPrice;
            from.NonForecastRequirement = to.NonForecastRequirement;
            from.BaseUnitPrice = to.BaseUnitPrice;
            from.OneOffRequirement = to.OneOffRequirement;
            from.LabourPrice = to.LabourPrice;
            from.LinnProduced = to.LinnProduced;
            from.PreferredSupplier = to.PreferredSupplier;
            from.QcOnReceipt = to.QcOnReceipt;
            from.QcInformation = to.QcInformation;
            from.RawOrFinished = to.RawOrFinished;
            from.OurInspectionWeeks = to.OurInspectionWeeks;
            from.SafetyWeeks = to.SafetyWeeks;
            from.RailMethod = to.RailMethod;
            from.MinStockRail = to.MinStockRail;
            from.MaxStockRail = to.MaxStockRail;
            from.SecondStageBoard = to.SecondStageBoard;
            from.SecondStageDescription = to.SecondStageDescription;
            from.TqmsCategoryOverride = to.TqmsCategoryOverride;
            from.StockNotes = to.StockNotes;
            from.ScrapOrConvert = to.ScrapOrConvert;
            from.PurchasingPhaseOutType = to.PurchasingPhaseOutType;
            from.DateDesignObsolete = to.DateDesignObsolete;
            from.PhasedOutBy = to.PhasedOutBy;
            from.DatePhasedOut = to.DatePhasedOut;
            from.ReasonPhasedOut = to.ReasonPhasedOut;
        }
    }
}