namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class CartonTypeResourceBuilder : IResourceBuilder<CartonType>
    {
        public CartonTypeResource Build(CartonType cartonType)
        {
            return new CartonTypeResource
            {
                CartonTypeName = cartonType.CartonTypeName,
                Description = cartonType.Description,
                Height = cartonType.Height,
                Width = cartonType.Width,
                Depth = cartonType.Depth
            };
        }

        public string GetLocation(CartonType cartonType)
        {
            return $"/logistics/carton-types/{cartonType.CartonTypeName}";
        }

        object IResourceBuilder<CartonType>.Build(CartonType cartonType) => this.Build(cartonType);
    }
}
