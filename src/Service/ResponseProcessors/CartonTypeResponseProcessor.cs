namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class CartonTypeResponseProcessor : JsonResponseProcessor<CartonType>
    {
        public CartonTypeResponseProcessor(IResourceBuilder<CartonType> resourceBuilder)
            : base(resourceBuilder, "carton-type", 1)
        {
        }
    }
}
