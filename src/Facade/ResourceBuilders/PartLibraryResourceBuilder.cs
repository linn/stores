namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartLibraryResourceBuilder : IResourceBuilder<PartLibrary>
    {
        public PartLibraryResource Build(PartLibrary p)
        {
            return new PartLibraryResource
                       {
                           LibraryName = p.LibraryName
                       };
        }

        public string GetLocation(PartLibrary p)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartLibrary>.Build(PartLibrary p) => this.Build(p);
    }
}
