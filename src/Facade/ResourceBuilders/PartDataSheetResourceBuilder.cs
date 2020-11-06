namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartDataSheetResourceBuilder : IResourceBuilder<PartDataSheet>
    {
        public PartDataSheetResource Build(PartDataSheet model)
        {
            return new PartDataSheetResource
            {
                PdfFilePath = model.PdfFilePath,
                Sequence = model.Sequence
            };
        }

        object IResourceBuilder<PartDataSheet>.Build(PartDataSheet s) => this.Build(s);

        public string GetLocation(PartDataSheet model)
        {
            throw new System.NotImplementedException();
        }
    }
}
