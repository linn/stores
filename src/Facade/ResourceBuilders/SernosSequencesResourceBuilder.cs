namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SernosSequencesResourceBuilder : IResourceBuilder<IEnumerable<SernosSequence>>
    {
        private readonly SernosSequenceResourceBuilder sernosSequenceResourceBuilder = new SernosSequenceResourceBuilder();

        public IEnumerable<SernosSequenceResource> Build(IEnumerable<SernosSequence> sernosSequences)
        {
            return sernosSequences
                .Select(a => this.sernosSequenceResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<SernosSequence>>.Build(IEnumerable<SernosSequence> sernosSequences) => this.Build(sernosSequences);

        public string GetLocation(IEnumerable<SernosSequence> sernosSequences)
        {
            throw new NotImplementedException();
        }
    }
}
