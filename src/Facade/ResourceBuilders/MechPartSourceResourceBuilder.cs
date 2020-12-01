namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Resources;

    public class MechPartSourceResourceBuilder : IResourceBuilder<MechPartSource>
    {
        private readonly PartResourceBuilder partResourceBuilder = new PartResourceBuilder();

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
                            Links = this.BuildLinks(model).ToArray()
            };
        }

        public string GetLocation(MechPartSource model)
        {
            return $"/inventory/parts/sources/{model.Id}";
        }

        object IResourceBuilder<MechPartSource>.Build(MechPartSource source) => this.Build(source);

        private IEnumerable<LinkResource> BuildLinks(MechPartSource source)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(source) };
        }
    }
}
