namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportReturnDetailsResourceBuilder : IResourceBuilder<IEnumerable<ExportReturnDetail>>
    {
        private readonly ExportReturnDetailResourceBuilder exportReturnDetailResourceBuilder =
            new ExportReturnDetailResourceBuilder();

        public IEnumerable<ExportReturnDetailResource> Build(IEnumerable<ExportReturnDetail> exportReturnDetails)
        {
            return exportReturnDetails.Select(e => this.exportReturnDetailResourceBuilder.Build(e));
        }

        public string GetLocation(IEnumerable<ExportReturnDetail> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<ExportReturnDetail>>.Build(
            IEnumerable<ExportReturnDetail> exportReturnDetails) =>
            this.Build(exportReturnDetails);
    }
}