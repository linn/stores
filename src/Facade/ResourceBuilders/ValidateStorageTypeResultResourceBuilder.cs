namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    public class ValidateStorageTypeResultResourceBuilder : IResourceBuilder<ValidateStorageTypeResult>
    {
        public ValidateStorageTypeResultResource Build(ValidateStorageTypeResult result)
        {
            return new ValidateStorageTypeResultResource
                       {
                          Message = result.Message,
                          LocationCode = result.LocationCode,
                          LocationId = result.LocationId
                       };
        }

        public string GetLocation(ValidateStorageTypeResult result)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ValidateStorageTypeResult>.Build(ValidateStorageTypeResult result) 
            => this.Build(result);
    }
}
