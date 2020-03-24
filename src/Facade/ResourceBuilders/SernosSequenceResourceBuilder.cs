namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SernosSequenceResourceBuilder : IResourceBuilder<SernosSequence>
    {
        public SernosSequenceResource Build(SernosSequence sernosSequence)
        {
            return new SernosSequenceResource
                       {
                           SequenceName = sernosSequence.Sequence,
                           Description = sernosSequence.Description,
                       };
        }

        public string GetLocation(SernosSequence sernosSequence)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<SernosSequence>.Build(SernosSequence sernosSequence) => this.Build(sernosSequence);

        private IEnumerable<LinkResource> BuildLinks(SernosSequence sernosSequence)
        {
            throw new NotImplementedException();
        }
    }
}