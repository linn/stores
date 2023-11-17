namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class MechPartSourceService : IMechPartSourceService
    {
        private readonly IAuthorisationService authService;

        public MechPartSourceService(IAuthorisationService authService)
        {
            this.authService = authService;
        }

        public string GetRkmCode(string unit, decimal value)
        {
            var units = new Dictionary<string, decimal>
                            {
                                { "K", 1000m },
                                { "M", 1000000m },
                            };

            if (string.IsNullOrEmpty(unit))
            {
                return value.ToString("G");
            }

            if (value < 1)
            {
                return value.ToString("G") + unit;
            }

            var result = value / units[unit];
            return result % 1m == 0 ?
                       result.ToString("G") + unit
                       : result.ToString("G").Replace(".", unit);
        }

        public string GetCapacitanceLetterAndNumeralCode(string unit, decimal value)
        {
            var divisor = 1m;

            if (!string.IsNullOrEmpty(unit))
            {
                var units = new Dictionary<string, decimal>
                                {
                                    { "u", 0.000001m },
                                    { "n", 0.000000001m },
                                    { "p", 0.000000000001m },
                                };
                divisor = units[unit];
            }
           

            var result = (value / divisor).ToString("G29");

            if (result.Contains("."))
            {
                return result.Replace(".", unit) + "F";
            }

            return result + unit + "F";
        }

        public MechPartSource Create(MechPartSource candidate, IEnumerable<string> userPrivileges)
        {
            if (!this.authService
                    .HasPermissionFor(
                        AuthorisedAction.PartAdmin,
                        userPrivileges))
            {
                throw new CreatePartException("You are not authorised to create.");
            }

            if (candidate.SafetyCritical == "Y" && string.IsNullOrEmpty(candidate.SafetyDataDirectory))
            {
                throw new CreatePartException("You must enter a EMC/safety data directory for EMC or safety critical parts");
            }

            if (candidate.Usages?.FirstOrDefault() == null)
            {
                throw new CreatePartException("You must enter at least one Usage when creating a source sheet");
            }

            return candidate;
        }

        public void Update(MechPartSource updated, MechPartSource current, IEnumerable<string> userPrivileges)
        {
            if (!this.authService
                    .HasPermissionFor(
                        AuthorisedAction.PartAdmin,
                        userPrivileges))
            {
                throw new UpdatePartException("You are not authorised to update.");
            }

            if (updated.Part != null)
            {
                current.Part.DataSheets =
                    this.GetUpdatedDataSheets(current.Part.DataSheets, updated.Part.DataSheets);
            }

            current.AssemblyType = updated.AssemblyType;
            current.DateSamplesRequired = updated.DateSamplesRequired;
            current.EmcCritical = updated.EmcCritical;
            current.EstimatedVolume = updated.EstimatedVolume;
            current.LinnPartNumber = updated.LinnPartNumber;
            current.MechanicalOrElectrical = updated.MechanicalOrElectrical;
            current.Notes = updated.Notes;
            current.ProposedBy = updated.ProposedBy;
            current.PerformanceCritical = updated.PerformanceCritical;
            current.SafetyCritical = updated.SafetyCritical;
            current.SingleSource = updated.SingleSource;
            current.PartType = updated.PartType;
            current.RohsReplace = updated.RohsReplace;
            current.SampleQuantity = updated.SampleQuantity;
            current.SamplesRequired = updated.SamplesRequired;
            current.PartToBeReplaced = updated.PartToBeReplaced;
            current.ProductionDate = updated.ProductionDate;
            current.SafetyDataDirectory = updated.SafetyDataDirectory;
            current.MechPartManufacturerAlts = this.GetUpdatedManufacturers(current.MechPartManufacturerAlts, updated.MechPartManufacturerAlts);
            current.MechPartAlts = updated.MechPartAlts;
            current.ApprovedReferenceStandards = updated.ApprovedReferenceStandards;
            current.ApprovedReferencesAvailable = updated.ApprovedReferencesAvailable;
            current.ApprovedReferencesDate = updated.ApprovedReferencesDate;
            current.ChecklistAvailable = updated.ChecklistAvailable;
            current.ChecklistCreated = updated.ChecklistCreated;
            current.ChecklistDate = updated.ChecklistDate;
            current.DrawingFile = updated.DrawingFile;
            current.DrawingsPackage = updated.DrawingsPackage;
            current.DrawingsPackageAvailable = updated.DrawingsPackageAvailable;
            current.DrawingsPackageDate = updated.DrawingsPackageDate;
            current.PackingAvailable = updated.PackingAvailable;
            current.PackingDate = updated.PackingDate;
            current.PackingRequired = updated.PackingRequired;
            current.ProcessEvaluation = updated.ProcessEvaluation;
            current.ProcessEvaluationAvailable = updated.ProcessEvaluationAvailable;
            current.ProcessEvaluationDate = updated.ProcessEvaluationDate;
            current.ProductKnowledge = updated.ProductKnowledge;
            current.ProductKnowledgeAvailable = updated.ProductKnowledgeAvailable;
            current.ProductKnowledgeDate = updated.ProductKnowledgeDate;
            current.TestEquipment = updated.TestEquipment;
            current.TestEquipmentAvailable = updated.TestEquipmentAvailable;
            current.TestEquipmentDate = updated.TestEquipmentDate;
            current.CapacitorRippleCurrent = updated.CapacitorRippleCurrent;
            current.Capacitance = updated.Capacitance;
            current.CapacitorVoltageRating = updated.CapacitorVoltageRating;
            current.CapacitorPositiveTolerance = updated.CapacitorPositiveTolerance;
            current.CapacitorNegativeTolerance = updated.CapacitorNegativeTolerance;
            current.CapacitorDielectric = updated.CapacitorDielectric;
            current.Package = updated.Package;
            current.CapacitorPitch = updated.CapacitorPitch;
            current.CapacitorLength = updated.CapacitorLength;
            current.CapacitorWidth = updated.CapacitorWidth;
            current.CapacitorHeight = updated.CapacitorHeight;
            current.CapacitorDiameter = updated.CapacitorDiameter;
            current.CapacitanceLetterAndNumeralCode = updated.CapacitanceLetterAndNumeralCode;
            current.Resistance = updated.Resistance;
            current.ResistorTolerance = updated.ResistorTolerance;
            current.Construction = updated.Construction;
            current.ResistorLength = updated.ResistorLength;
            current.ResistorHeight = updated.ResistorHeight;
            current.ResistorWidth = updated.ResistorWidth;
            current.ResistorPowerRating = updated.ResistorPowerRating;
            current.ResistorVoltageRating = updated.ResistorVoltageRating;
            current.TemperatureCoefficient = updated.TemperatureCoefficient;
            current.TransistorType = updated.TransistorType;
            current.TransistorDeviceName = updated.TransistorDeviceName;
            current.TransistorCurrent = updated.TransistorCurrent;
            current.TransistorVoltage = updated.TransistorVoltage;
            current.TransistorPolarity = updated.TransistorPolarity;
            current.IcType = updated.IcType;
            current.IcFunction = updated.IcFunction;
            current.LibraryRef = updated.LibraryRef;
            current.FootprintRef = updated.FootprintRef;
            current.LibraryName = updated.LibraryName;
            current.FootprintRef2 = updated.FootprintRef2;
            current.FootprintRef3 = updated.FootprintRef3;
            current.RkmCode = updated.RkmCode;
            current.ApplyTCodeBy = updated.ApplyTCodeBy;
            current.ApplyTCodeDate = updated.ApplyTCodeDate;
            current.CancelledBy = updated.CancelledBy;
            current.DateCancelled = updated.DateCancelled;
            current.McitVerifiedBy = updated.McitVerifiedBy;
            current.McitVerifiedDate = updated.McitVerifiedDate;
            current.QualityVerifiedBy = updated.QualityVerifiedBy;
            current.QualityVerifiedDate = updated.QualityVerifiedDate;
            current.RemoveTCodeBy = updated.RemoveTCodeBy;
            current.RemoveTCodeDate = updated.RemoveTCodeDate;
            current.VerifiedBy = updated.VerifiedBy;
            current.VerifiedDate = updated.VerifiedDate;
            current.PurchasingQuotes = updated.PurchasingQuotes;
            current.Usages = updated.Usages;
            current.LifeExpectancyPart = updated.LifeExpectancyPart;
            current.Configuration = updated.Configuration;
        }

        private IEnumerable<PartDataSheet> GetUpdatedDataSheets(IEnumerable<PartDataSheet> from, IEnumerable<PartDataSheet> to)
        {
            var updated = to.ToList();
            var old = from.ToList();

            foreach (var partDataSheet in updated.Where(n => old.All(o => o.Sequence != n.Sequence)))
            {
                partDataSheet.Sequence = updated.Max(s => s.Sequence) + 1;
            }

            return updated;
        }

        private IEnumerable<MechPartManufacturerAlt> GetUpdatedManufacturers(IEnumerable<MechPartManufacturerAlt> from, IEnumerable<MechPartManufacturerAlt> to)
         {
            if (to == null)
            {
                return from;
            }

            var updated = to.ToList();
            var old = from.ToList();

            foreach (var alt in updated.Where(n => old.All(o => o.Sequence != n.Sequence)))
            {
                alt.Sequence = updated.Max(s => s.Sequence) + 1;
            }

            return updated;
        }
    }
}
