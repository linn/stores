namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;

    public class MakeExportReturnResultResourceBuilder : IResourceBuilder<MakeExportReturnResult>
    {
        public MakeExportReturnResource Build(MakeExportReturnResult result)
        {
            return new MakeExportReturnResource { ExportReturnId = result.ExportReturnId };
        }

        public string GetLocation(MakeExportReturnResult result)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<MakeExportReturnResult>.Build(MakeExportReturnResult result) => this.Build(result);
    }
}