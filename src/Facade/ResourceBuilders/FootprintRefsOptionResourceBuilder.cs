namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class FootprintRefsOptionResourceBuilder : IResourceBuilder<IEnumerable<FootprintRefOption>>
    {
        public IEnumerable<FootprintRefOptionResource> Build(IEnumerable<FootprintRefOption> libraryRefs)
        {
            return libraryRefs
                .Select(a => new FootprintRefOptionResource
                {
                                     LibraryName = a.LibraryName,
                                     Ref1 = a.Ref1,
                                     Ref2 = a.Ref2,
                                     Ref3 = a.Ref3
                });
        }

        object IResourceBuilder<IEnumerable<FootprintRefOption>>.Build(IEnumerable<FootprintRefOption> libraryRefs) 
            => this.Build(libraryRefs);

        public string GetLocation(IEnumerable<FootprintRefOption> libraryRefs)
        {
            throw new NotImplementedException();
        }
    }
}
