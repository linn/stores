﻿namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class PartResourceBuilder : IResourceBuilder<Part>
    {
        public PartResource Build(Part part)
        {
            return new PartResource
                       {
                           Id = part.Id,
                           PartNumber = part.PartNumber,
                           Description = part.Description,
                           Links = this.BuildLinks(part).ToArray()
                       };
        }

        public string GetLocation(Part part)
        {
            return $"/parts/{part.Id}";
        }

        object IResourceBuilder<Part>.Build(Part part) => this.Build(part);

        private IEnumerable<LinkResource> BuildLinks(Part part)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(part) };
        }
    }
}