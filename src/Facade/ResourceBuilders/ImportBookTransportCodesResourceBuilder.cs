namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookTransportCodesResourceBuilder : IResourceBuilder<IEnumerable<ImportBookTransportCode>>
    {
        private readonly ImportBookTransportCodeResourceBuilder importBookTransportCodeResourceBuilder =
            new ImportBookTransportCodeResourceBuilder();

        public IEnumerable<ImportBookTransportCodeResource> Build(IEnumerable<ImportBookTransportCode> transportCodes)
        {
            return transportCodes.OrderBy(b => b.TransportId)
                .Select(a => this.importBookTransportCodeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookTransportCode>>.
            Build(IEnumerable<ImportBookTransportCode> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookTransportCode> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
