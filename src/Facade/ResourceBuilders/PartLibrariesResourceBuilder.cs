namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartLibrariesResourceBuilder : IResourceBuilder<IEnumerable<PartLibrary>>
    {
        private readonly PartLibraryResourceBuilder hubResourceBuilder = new PartLibraryResourceBuilder();

        public IEnumerable<PartLibraryResource> Build(IEnumerable<PartLibrary> partLibraries)
        {
            return partLibraries
                .Select(a => this.hubResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartLibrary>>.Build(IEnumerable<PartLibrary> partLibraries) => this.Build(partLibraries);

        public string GetLocation(IEnumerable<PartLibrary> partLibraries)
        {
            throw new NotImplementedException();
        }
    }
}
