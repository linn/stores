namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;

    public class ProcessResultResourceBuilder : IResourceBuilder<ProcessResult>
    {
        public ProcessResultResource Build(ProcessResult process)
        {
            return new ProcessResultResource
            {
               Message = process.Message,
               Success = process.Success
            };
        }

        public string GetLocation(ProcessResult process)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ProcessResult>.Build(ProcessResult process) => this.Build(process);
    }
}
