namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    public class MechPartSourceResourceBuilder : IResourceBuilder<MechPartSource>
    {
        private readonly PartResourceBuilder partResourceBuilder = new PartResourceBuilder();

        public MechPartSourceResource Build(MechPartSource model)
        {
            return new MechPartSourceResource
                        {
                            AssemblyType = model.AssemblyType,
                            DateEntered = model.DateEntered.ToString("o"),
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
                            Part = this.partResourceBuilder.Build(model.Part),
                            SafetyDataDirectory = model.SafetyDataDirectory,
                            ProductionDate = model.ProductionDate?.ToString("o")
                        };
        }

        public string GetLocation(MechPartSource model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<MechPartSource>.Build(MechPartSource source) => this.Build(source);
    }
}
