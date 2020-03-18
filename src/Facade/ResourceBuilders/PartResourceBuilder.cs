namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Resources;

    public class PartResourceBuilder : IResourceBuilder<Part>
    {
        public PartResource Build(Part part)
        {
            return new PartResource
                       {
                           Id = part.Id,
                           PartNumber = part.PartNumber,
                           Description = part.Description
                       };
        }

        public string GetLocation(Part part)
        {
            return $"/parts/{part.Id}";
        }

        object IResourceBuilder<Part>.Build(Part part) => this.Build(part);

        private IEnumerable<LinkResource> BuildLinks(Part part)
        {
            throw new NotImplementedException();
        }
    }
}