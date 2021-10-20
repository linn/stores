namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Exceptions;

    using ExternalServices;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class PartService : IPartService
    {
        private readonly IAuthorisationService authService;

        private readonly IQueryRepository<Supplier> supplierRepository;

        private readonly IRepository<QcControl, int> qcControlRepository;

        private readonly IRepository<Part, int> partRepository;

        private readonly IRepository<PartTemplate, string> templateRepository;

        private readonly IPartPack partPack;

        private readonly IRepository<MechPartSource, int> sourceRepository;

        private readonly IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository;

        private readonly IDeptStockPartsService deptStockPartsService;

        public PartService(
            IAuthorisationService authService,
            IRepository<QcControl, int> qcControlRepository,
            IQueryRepository<Supplier> supplierRepository,
            IRepository<Part, int> partRepository,
            IRepository<PartTemplate, string> templateRepository,
            IRepository<MechPartSource, int> sourceRepository,
            IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository,
            IPartPack partPack,
            IDeptStockPartsService deptStockPartsService)
        {
            this.authService = authService;
            this.supplierRepository = supplierRepository;
            this.qcControlRepository = qcControlRepository;
            this.partRepository = partRepository;
            this.partPack = partPack;
            this.sourceRepository = sourceRepository;
            this.templateRepository = templateRepository;
            this.dataSheetRepository = dataSheetRepository;
            this.deptStockPartsService = deptStockPartsService;
        }

        public void UpdatePart(Part from, Part to, List<string> privileges, IEnumerable<MechPartManufacturerAlt> manufacturers)
        {
            to.PartNumber = to.PartNumber?.ToUpper();
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

            if (from.SalesArticle != null
                && !from.ProductAnalysisCode.ProductCode.Equals(to.ProductAnalysisCode.ProductCode))
            {
                throw new UpdatePartException("Cannot change product analysis code if part has a sales article.");
            }

            if (from.DateLive != to.DateLive)
            {
                if (!this.authService.HasPermissionFor(AuthorisedAction.PartAdmin, privileges))
                {
                    throw new UpdatePartException("You are not authorised to complete this action.");
                }

                if (from.DateLive == null && !this.partPack.PartLiveTest(from.PartNumber, out var message))
                {
                    throw new UpdatePartException(message);
                }

                from.DateLive = to.DateLive;
                from.MadeLiveBy = to.MadeLiveBy;
            }

            if (manufacturers != null)
            {
                var source = this.sourceRepository.FindById(from.MechPartSource.Id);
                source.MechPartManufacturerAlts = manufacturers.Select(
                    m =>
                        {
                            var manufacturer =
                                from.MechPartSource.MechPartManufacturerAlts.First(
                                    i => i.ManufacturerCode == m.ManufacturerCode);
                            manufacturer.PartNumber = m.PartNumber;
                            return manufacturer;
                        }).ToList();
            }

            Validate(to);

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

        public Part CreatePart(Part partToCreate, List<string> privileges, bool fromTemplate)
        {
            partToCreate.PartNumber = partToCreate.PartNumber?.ToUpper().Trim();

            if (!this.authService.HasPermissionFor(AuthorisedAction.PartAdmin, privileges))
            {
                throw new CreatePartException("You are not authorised to create parts.");
            }

            if (string.IsNullOrEmpty(partToCreate.StockControlled))
            {
                throw new CreatePartException("Must specify whether part is stock controlled.");
            }

            var partRoot = this.partPack.PartRoot(partToCreate.PartNumber);

            var template = this.templateRepository.FindById(partRoot);

            if (partRoot != null && template != null)
            {
                if (template.AllowPartCreation == "N")
                {
                    throw new CreatePartException("The system no longer allows creation of " + partRoot + " parts.");
                }

                var newestPartOfThisType = this.partRepository.FilterBy(p => p.PartNumber.StartsWith(partRoot) && p.DateCreated.HasValue)
                    .OrderByDescending(p => p.DateCreated).ToList().FirstOrDefault()
                    ?.PartNumber;

                var realNextNumber = FindRealNextNumber(newestPartOfThisType, template);

                if (this.partRepository.FindBy(p => p.PartNumber == partToCreate.PartNumber) != null)
                {
                    throw new CreatePartException("A Part with that Part Number already exists. Why not try " + realNextNumber);
                }

                this.templateRepository.FindById(partRoot).NextNumber = realNextNumber + 1;
            }
            else if (this.partRepository.FindBy(p => p.PartNumber == partToCreate.PartNumber) != null)
            {
                throw new CreatePartException("A Part with that Part Number already exists.");
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

            Validate(partToCreate);

            return partToCreate;
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

        public Part CreateFromSource(int sourceId, int createdBy, IEnumerable<PartDataSheet> dataSheets)
        {
            var source = this.sourceRepository.FindById(sourceId);
            source.PartNumber = 
                this.partPack.CreatePartFromSourceSheet(sourceId, createdBy, out var message);
            
            if (message != $"Created part {source.PartNumber}")
            {
                throw new CreatePartException(message);
            }

            var part = this.partRepository.FindBy(p => p.PartNumber == source.PartNumber);

            var seq = 1;
            foreach (var partDataSheet in dataSheets)
            {
                this.dataSheetRepository.Add(new PartDataSheet
                                                 {
                                                     PartNumber = source.PartNumber,
                                                     Sequence = seq,
                                                     Part = part,
                                                     PdfFilePath = partDataSheet.PdfFilePath
                                                 });
                seq++;
            }

            return part; 
        }

        public IEnumerable<Part> GetDeptStockPalletParts()
        {
            return this.deptStockPartsService.GetDeptStockPalletParts();
        }

        private static void Validate(Part to)
        {
            if (!string.IsNullOrEmpty(to.ScrapOrConvert)  && to.DatePhasedOut == null)
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

        private static int? FindRealNextNumber(string newestPartOfThisType, PartTemplate template)
        {
            var highestNumber = newestPartOfThisType?.Split(" ").Last().Split("/")[0];
            if (int.TryParse(highestNumber, out var res))
            {
                return res + 1;
            }

            if (template.HasNumberSequence != "Y")
            {
                throw new CreatePartException("Template has no number sequence");
            }

            return template.NextNumber;
        }
    }
}
