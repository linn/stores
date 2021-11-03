namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    public class ValidateRsnResultResourceBuilder : IResourceBuilder<ValidateRsnResult>
    {
        public ValidateRsnResultResource Build(ValidateRsnResult result)
        {
            return new ValidateRsnResultResource
                       {
                           Message = result.Message,
                           Quantity = result.Quantity,
                           ArticleNumber = result.ArticleNumber,
                           Description = result.Description,
                           State = result.State,
                           SerialNumber = result.SerialNumber,
                           Success = result.Success
                       };
        }

        public string GetLocation(ValidateRsnResult result)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ValidateRsnResult>.Build(ValidateRsnResult result)
            => this.Build(result);
    }
}
