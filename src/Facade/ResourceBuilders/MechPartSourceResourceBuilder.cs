namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartSourceResourceBuilder : IResourceBuilder<MechPartSource>
    {
        private readonly PartResourceBuilder partResourceBuilder = new PartResourceBuilder();

        private readonly MechPartAltResourceBuilder altResourceBuilder = new MechPartAltResourceBuilder();

        private readonly MechPartManufacturerAltResourceBuilder manufacturerAltResourceBuilder = 
            new MechPartManufacturerAltResourceBuilder();

        private readonly MechPartUsageResourceBuilder usageResourceBuilder = new MechPartUsageResourceBuilder();

        private readonly MechPartPurchasingQuoteResourceBuilder purchasingQuotesResourceBuilder = 
            new MechPartPurchasingQuoteResourceBuilder();

        public MechPartSourceResource Build(MechPartSource model)
        {
            return new MechPartSourceResource
                        {
                            AssemblyType = model.AssemblyType,
                            DateEntered = model.DateEntered?.ToString("o"),
                            DateSamplesRequired = model.DateSamplesRequired?.ToString("o"),
                            EstimatedVolume = model.EstimatedVolume,
                            Id = model.Id,
                            LinnPartNumber = model.PartToBeReplaced?.PartNumber,
                            Description = model.PartDescription,
                            LinnPartDescription = model.PartToBeReplaced?.Description,
                            MechanicalOrElectrical = model.MechanicalOrElectrical,
                            Notes = model.Notes,
                            PartNumber = model.PartNumber,
                            PartType = model.PartType,
                            ProposedBy = model.ProposedBy?.Id,
                            ProposedByName = model.ProposedBy?.FullName,
                            RohsReplace = model.RohsReplace,
                            SampleQuantity = model.SampleQuantity,
                            SamplesRequired = model.SamplesRequired,
                            EmcCritical = model.EmcCritical,
                            PerformanceCritical = model.PerformanceCritical,
                            SafetyCritical = model.SafetyCritical,
                            SingleSource = model.SingleSource,
                            Part = model.Part == null ? null : this.partResourceBuilder.Build(model.Part),
                            SafetyDataDirectory = model.SafetyDataDirectory,
                            ProductionDate = model.ProductionDate?.ToString("o"),
                            MechPartManufacturerAlts = model.MechPartManufacturerAlts?.Select(x => 
                                this.manufacturerAltResourceBuilder.Build(x)).OrderBy(x => x.Preference),
                            MechPartAlts = model.MechPartAlts?.Select(x => 
                                this.altResourceBuilder.Build(x)),
                            PackingDate = model.PackingDate?.ToString("o"),
                            DrawingsPackageDate = model.DrawingsPackageDate?.ToString("o"),
                            ChecklistDate = model.ChecklistDate?.ToString("o"),
                            ChecklistAvailable = model.ChecklistAvailable,
                            TestEquipmentDate = model.TestEquipmentDate?.ToString("o"),
                            TestEquipmentAvailable = model.TestEquipmentAvailable,
                            ProcessEvaluationDate = model.ProcessEvaluationDate?.ToString("o"),
                            DrawingsPackageAvailable = model.DrawingsPackageAvailable,
                            ProcessEvaluationAvailable = model.ProcessEvaluationAvailable,
                            ApprovedReferenceStandards = model.ApprovedReferenceStandards,
                            ProductKnowledgeDate = model.ProductKnowledgeDate?.ToString("o"),
                            ProcessEvaluation = model.ProcessEvaluation,
                            ApprovedReferencesAvailable = model.ApprovedReferencesAvailable,
                            ApprovedReferencesDate = model.ApprovedReferencesDate?.ToString("o"),
                            ChecklistCreated = model.ChecklistCreated,
                            DrawingFile = model.DrawingFile,
                            DrawingsPackage = model.DrawingsPackage,
                            PackingAvailable = model.PackingAvailable,
                            PackingRequired = model.PackingRequired,
                            ProductKnowledge = model.ProductKnowledge,
                            ProductKnowledgeAvailable = model.ProductKnowledgeAvailable,
                            TestEquipment = model.TestEquipment,
                            CapacitorRippleCurrent = model.CapacitorRippleCurrent,
                            Capacitance = model.Capacitance,
                            CapacitorVoltageRating = model.CapacitorVoltageRating,
                            CapacitorPositiveTolerance = model.CapacitorPositiveTolerance,
                            CapacitorNegativeTolerance = model.CapacitorNegativeTolerance,
                            CapacitorDielectric = model.CapacitorDielectric,
                            PackageName = model.Package,
                            CapacitorPitch = model.CapacitorPitch,
                            CapacitorLength = model.CapacitorLength,
                            CapacitorWidth = model.CapacitorWidth,
                            CapacitorHeight = model.CapacitorHeight,
                            CapacitorDiameter = model.CapacitorDiameter,
                            Resistance = model.Resistance,
                            ResistorTolerance = model.ResistorTolerance,
                            Construction = model.Construction,
                            ResistorLength = model.ResistorLength,
                            ResistorHeight = model.ResistorHeight,
                            ResistorWidth = model.ResistorWidth,
                            ResistorPowerRating = model.ResistorPowerRating,
                            ResistorVoltageRating = model.ResistorVoltageRating,
                            TemperatureCoefficient = model.TemperatureCoefficient,
                            TransistorType = model.TransistorType,
                            TransistorDeviceName = model.TransistorDeviceName,
                            TransistorCurrent = model.TransistorCurrent,
                            TransistorVoltage = model.TransistorVoltage,
                            TransistorPolarity = model.TransistorPolarity,
                            IcType = model.IcType,
                            IcFunction = model.IcFunction,
                            LibraryRef = model.LibraryRef,
                            FootprintRef = model.FootprintRef,
                            RkmCode = model.RkmCode,
                            CapacitanceLetterAndNumeralCode = model.CapacitanceLetterAndNumeralCode,
                            PartCreatedBy = model.PartCreatedBy?.Id,
                            PartCreatedByName = model.PartCreatedBy?.FullName,
                            PartCreatedDate = model.PartCreatedDate?.ToString("o"),
                            VerifiedBy = model.VerifiedById,
                            VerifiedByName = model.VerifiedBy?.FullName,
                            VerifiedDate = model.VerifiedDate?.ToString("o"),
                            QualityVerifiedBy = model.QualityVerifiedById,
                            QualityVerifiedByName = model.QualityVerifiedBy?.FullName,
                            QualityVerifiedDate = model.QualityVerifiedDate?.ToString("o"),
                            McitVerifiedBy = model.McitVerifiedById,
                            McitVerifiedByName = model.McitVerifiedBy?.FullName,
                            McitVerifiedDate = model.McitVerifiedDate?.ToString("o"),
                            ApplyTCodeBy = model.ApplyTCodeId,
                            ApplyTCodeByName = model.ApplyTCodeBy?.FullName,
                            ApplyTCodeDate = model.ApplyTCodeDate?.ToString("o"),
                            RemoveTCodeBy = model.RemoveTCodeId,
                            RemoveTCodeByName = model.RemoveTCodeBy?.FullName,
                            RemoveTCodeDate = model.RemoveTCodeDate?.ToString("o"),
                            CancelledBy = model.CancelledById,
                            CancelledByName = model.CancelledBy?.FullName,
                            CancelledDate = model.DateCancelled?.ToString("o"),
                            PurchasingQuotes = model.PurchasingQuotes?.Select(
                                (q, i) =>
                                    {
                                        var resource = this.purchasingQuotesResourceBuilder.Build(q);
                                        resource.Id = i;
                                        return resource;
                                    })
                                .ToList(),
                            Usages = model.Usages?.Select(
                                    (u, i) =>
                                        {
                                            var resource = this.usageResourceBuilder.Build(u);
                                            resource.Id = i;
                                            return resource;
                                        })
                                .ToList(),
                            Links = this.BuildLinks(model).ToArray(),
                            LifeExpectancyPart = model.LifeExpectancyPart,
                            Configuration = model.Configuration,
                            FootprintRef2 = model.FootprintRef2,
                            FootprintRef3 = model.FootprintRef3
            };
        }

        public string GetLocation(MechPartSource model)
        {
            return $"/parts/sources/{model.Id}";
        }

        object IResourceBuilder<MechPartSource>.Build(MechPartSource source) => this.Build(source);

        private IEnumerable<LinkResource> BuildLinks(MechPartSource source)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(source) };
        }
    }
}
