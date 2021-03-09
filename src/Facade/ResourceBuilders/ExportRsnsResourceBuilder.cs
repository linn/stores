namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportRsnsResourceBuilder : IResourceBuilder<IEnumerable<ExportRsn>>
    {
        private readonly ExportRsnResourceBuilder exportRsnResourceBuilder = new ExportRsnResourceBuilder();

        public IEnumerable<ExportRsnResource> Build(IEnumerable<ExportRsn> rsns)
        {
            return rsns.Select(r => this.exportRsnResourceBuilder.Build(r));
        }

        public string GetLocation(IEnumerable<ExportRsn> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<ExportRsn>>.Build(IEnumerable<ExportRsn> rsns) => this.Build(rsns);
    }
}