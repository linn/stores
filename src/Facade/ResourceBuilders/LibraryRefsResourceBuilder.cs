namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class LibraryRefsResourceBuilder : IResourceBuilder<IEnumerable<LibraryRef>>
    {
        public IEnumerable<LibraryRefResource> Build(IEnumerable<LibraryRef> libraryRefs)
        {
            return libraryRefs
                .Select(a => new LibraryRefResource
                                 {
                                     LibraryName = a.LibraryName,
                                     LibraryRef = a.Ref
                                 });
        }

        object IResourceBuilder<IEnumerable<LibraryRef>>.Build(IEnumerable<LibraryRef> libraryRefs) => this.Build(libraryRefs);

        public string GetLocation(IEnumerable<LibraryRef> libraryRefs)
        {
            throw new NotImplementedException();
        }
    }
}
