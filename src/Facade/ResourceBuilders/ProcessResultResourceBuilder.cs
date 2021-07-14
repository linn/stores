namespace Linn.Stores.Facade.ResourceBuilders
{
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
