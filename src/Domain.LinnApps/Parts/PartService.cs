namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class PartService : IPartService
    {
        private readonly IAuthorisationService authService;

        private readonly IQueryRepository<Supplier> supplierRepository;

        private readonly IRepository<QcControl, int> qcControlRepository;

        public PartService(
            IAuthorisationService authService,
            IRepository<QcControl, int> qcControlRepository,
            IQueryRepository<Supplier> supplierRepository)
        {
            this.authService = authService;
            this.supplierRepository = supplierRepository;
            this.qcControlRepository = qcControlRepository;
        }

        public void UpdatePart(Part from, Part to, List<string> privileges)
        {
            if (from.DatePhasedOut == null && to.DatePhasedOut != null)
            {
                if (!this.authService.HasPermissionFor(AuthorisedAction.PartAdmin, privileges))
                {
                    throw new UpdatePartException("You are not authorised to phase out parts.");
                }

                if (to.ReasonPhasedOut == null)
                {
                    throw new UpdatePartException("Must Provide a Reason When phasing out a part.");
                }

                from.ScrapOrConvert = to.ScrapOrConvert ?? "CONVERT";
            }
            
            this.Validate(to);

            from.PhasedOutBy = to.PhasedOutBy;
            from.DatePhasedOut = to.DatePhasedOut;
            from.ReasonPhasedOut = to.ReasonPhasedOut;
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
            from.DateDesignObsolete = to.DateDesignObsolete;
            from.PurchasingPhaseOutType = to.PurchasingPhaseOutType;
        }

        public Part CreatePart(Part partToCreate, List<string> privileges)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.PartAdmin, privileges))
            {
                throw new CreatePartException("You are not authorised to create parts.");
            }

            if (partToCreate.StockControlled == "Y" && partToCreate.RailMethod == null)
            {
                partToCreate.RailMethod = "POLICY";
            }

            if (partToCreate.LinnProduced == "Y" && partToCreate.PreferredSupplier == null)
            {
                partToCreate.PreferredSupplier = this.supplierRepository.FindBy(s => s.Id == 4415);
            }

            partToCreate.OrderHold = "N";

           this.Validate(partToCreate);

            return partToCreate;
        }

        private void Validate(Part to)
        {
            if (to.ScrapOrConvert != null && to.DatePhasedOut == null)
            {
                throw new UpdatePartException("A part must be obsolete to be convertible or to be scrapped.");
            }

            if (to.RailMethod == "SMM"
                && to.StockControlled == "Y"
                && to.MinStockRail == 0
                && to.MaxStockRail == 0)
            {
                throw new UpdatePartException("Rail method SMM with 0 min/max rails is not a valid stocking policy.");
            }

            if (to.TqmsCategoryOverride != null && to.StockNotes == null)
            {
                throw new UpdatePartException("You must enter a reason and/or reference or project code when setting an override");
            }
        }

        public void AddQcControl(string partNumber, int? createdBy, string qcInfo)
        {
            this.qcControlRepository.Add(new QcControl
                                             {
                                                 Id = null,
                                                 PartNumber = partNumber,
                                                 TransactionDate = DateTime.Today,
                                                 ChangedBy = createdBy,
                                                 NumberOfBookIns = 0,
                                                 OnOrOffQc = "ON",
                                                 Reason = qcInfo
                                             });
        }
    }
}