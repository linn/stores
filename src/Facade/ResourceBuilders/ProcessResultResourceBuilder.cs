namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Models;

    public class ProcessResultResourceBuilder : IResourceBuilder<ProcessResult>
    {
        public ProcessResultResource Build(ProcessResult process)
        {
            return new ProcessResultResource
                       {
                           Message = process.Message,
                           Success = process.Success,
                       };
        }

        object IResourceBuilder<ProcessResult>.Build(ProcessResult process) => this.Build(process);

        public string GetLocation(ProcessResult model)
        {
            throw new System.NotImplementedException();
        }
    }
}
