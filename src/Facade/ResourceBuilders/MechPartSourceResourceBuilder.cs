using System.Linq;
using Linn.Common.Persistence;

namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    public class MechPartSourceResourceBuilder : IResourceBuilder<MechPartSourceWithPartInfo>
    {
        public MechPartSourceResource Build(MechPartSourceWithPartInfo model)
        {
            return new MechPartSourceResource
                        {
                            AssemblyType = model.AssemblyType,
                            DateEntered = model.DateEntered.ToString("o"),
                            DateSamplesRequired = model.DateSamplesRequired.ToString("o"),
                            EstimatedVolume = model.EstimatedVolume,
                            Id = model.Id,
                            LinnPartNumber = model.LinnPartNumber,
                            LinnPartDescription =model.LinnPart?.Description,
                            MechanicalOrElectrical = model.MechanicalOrElectrical,
                            Notes = model.Notes,
                            PartNumber = model.PartNumber,
                            PartType = model.PartType,
                            ProposedBy = model.ProposedBy?.Id,
                            RohsReplace = model.RohsReplace,
                            SampleQuantity = model.SampleQuantity,
                            SamplesRequired = model.SamplesRequired,
                            DataSheets = model.DataSheets.Select(s => new PartDataSheetResource
                                                                          {
                                                                              PartNumber = s.PartNumber,
                                                                              PdfFilePath = s.PdfFilePath,
                                                                              Sequence = s.Sequence
                                                                          })
                        };
        }

        public string GetLocation(MechPartSourceWithPartInfo model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<MechPartSourceWithPartInfo>.Build(MechPartSourceWithPartInfo source) => this.Build(source);
    }
}
