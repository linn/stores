namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Exceptions;

    using ExternalServices;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;

    public class PartService : IPartService
    {
        private readonly IAuthorisationService authService;

        private readonly IQueryRepository<Supplier> supplierRepository;

        private readonly IRepository<QcControl, int> qcControlRepository;

        private readonly IPartRepository partRepository;

        private readonly IRepository<PartTemplate, string> templateRepository;

        private readonly IPartPack partPack;

        private readonly IRepository<MechPartSource, int> sourceRepository;

        private readonly IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository;

        private readonly IDeptStockPartsService deptStockPartsService;

        private readonly IEmailService emailService;

        private readonly IQueryRepository<PhoneListEntry> phoneList;

        public PartService(
            IAuthorisationService authService,
            IRepository<QcControl, int> qcControlRepository,
            IQueryRepository<Supplier> supplierRepository,
            IPartRepository partRepository,
            IRepository<PartTemplate, string> templateRepository,
            IRepository<MechPartSource, int> sourceRepository,
            IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository,
            IPartPack partPack,
            IDeptStockPartsService deptStockPartsService,
            IEmailService emailService,
            IQueryRepository<PhoneListEntry> phoneList)
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
            this.emailService = emailService;
            this.phoneList = phoneList;
        }

        public void UpdatePart(Part from, Part to, List<string> privileges)
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

            if (from.DatePhasedOut != null && to.DatePhasedOut == null)
            {
                if (!this.authService.HasPermissionFor(AuthorisedAction.PartAdmin, privileges))
                {
                    throw new UpdatePartException("You are not authorised to phase in parts.");
                }
            }

            if (from.SalesArticle != null
                && !from.ProductAnalysisCode.ProductCode.Equals(to.ProductAnalysisCode?.ProductCode))
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

                if (to.PreferredSupplier == null)
                {
                    throw new UpdatePartException("Cannot make live without a preferred supplier!");
                }

                from.DateLive = to.DateLive;
                from.MadeLiveBy = to.MadeLiveBy;
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
            from.ImdsIdNumber = to.ImdsIdNumber;
            from.ImdsWeight = to.ImdsWeight;
            from.OrderHold = to.OrderHold;
            from.SparesRequirement = to.SparesRequirement;
            from.NonForecastRequirement = to.NonForecastRequirement;
            from.OneOffRequirement = to.OneOffRequirement;
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
            from.PlannerStory = to.PlannerStory;
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


            if (partRoot != null && fromTemplate)
            {
                var template = this.templateRepository.FindById(partRoot);

                if (template.AllowPartCreation == "N")
                {
                    throw new CreatePartException("The system no longer allows creation of " + partRoot + " parts.");
                }

                var newestPartOfThisType = this.partRepository.SearchPartsWithWildcard($"{partRoot} %", null, null, true, 100)
                    .Where(p => p.DateCreated.HasValue && !p.PartNumber.Contains("/")).OrderByDescending(p => p.Id)
                    .FirstOrDefault()?.PartNumber;
                
                var realNextNumber = FindRealNextNumber(newestPartOfThisType, template);

                if (this.partRepository.FilterBy(p => p.PartNumber == partToCreate.PartNumber).ToList()
                        .FirstOrDefault() != null)
                {
                    throw new CreatePartException("A Part with that Part Number already exists. Why not try " + realNextNumber);
                }

                this.templateRepository.FindById(partRoot).NextNumber = realNextNumber + 1;
            }
            else if (this.partRepository.FilterBy(p => p.PartNumber == partToCreate.PartNumber).ToList()
                         .FirstOrDefault() != null)
            {
                throw new CreatePartException("A Part with that Part Number already exists.");
            }

            if (partToCreate.StockControlled == "Y" && partToCreate.RailMethod == null)
            {
                partToCreate.RailMethod = "POLICY";
            }

            partToCreate.OrderHold = "N";

            this.Validate(partToCreate);

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

            var to = this.phoneList.FindBy(x => x.UserNumber == 16008);
            var bcc1 = this.phoneList.FindBy(x => x.UserNumber == 33145);
            var bcc2 = this.phoneList.FindBy(x => x.UserNumber == 5000);

            var cc = new List<Dictionary<string, string>>
                          { 
                              new Dictionary<string, string>
                                {
                                    { "name", bcc1.User.Name },
                                    { "address", bcc1.EmailAddress }
                                },
                              new Dictionary<string, string>
                                  {
                                      { "name", bcc2.User.Name },
                                      { "address", bcc2.EmailAddress }
                                  }
                          };
            this.emailService.SendEmail(
                to.EmailAddress,
                to.User.Name,
                cc,
                null,
                "stores@linn.co.uk",
                "Parts Utility",
                $"New Source Sheet Created - {source.PartNumber}",
                $"Click here to view: https://app.linn.co.uk/parts/sources/{source.Id}",
                null,
                null);
            return part; 
        }

        public IEnumerable<Part> GetDeptStockPalletParts()
        {
            return this.deptStockPartsService.GetDeptStockPalletParts();
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

        private void Validate(Part to)
        {
            if (!string.IsNullOrEmpty(to.ScrapOrConvert) && to.PhasedOutBy == null)
            {
                to.ScrapOrConvert = null;
            }

            if (to.RailMethod == "SMM"
                && to.StockControlled == "Y"
                && to.MinStockRail == 0
                && to.MaxStockRail == 0)
            {
                throw new UpdatePartException("Rail method SMM with 0 min/max rails is not a valid stocking policy.");
            }

            if (string.IsNullOrEmpty(to.RawOrFinished))
            {
                throw new CreatePartException("Must specify raw or finished!");
            }

            if (to.TqmsCategoryOverride != null && to.StockNotes == null)
            {
                throw new UpdatePartException("You must enter a reason and/or reference or project code when setting an override");
            }

            if (to.LinnProduced != null && to.LinnProduced.Equals("Y"))
            {
                to.PreferredSupplier = this.supplierRepository.FindBy(s => s.Id == 4415);
            }
        }
    }
}
