namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class ErrorResourceBuilder : IResourceBuilder<Error>
    {
        public ErrorResource Build(Error error)
        {
            return new ErrorResource { Errors = new List<string> { error.Message } };
        }

        public string GetLocation(Error error)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Error>.Build(Error error) => this.Build(error);
    }
}
