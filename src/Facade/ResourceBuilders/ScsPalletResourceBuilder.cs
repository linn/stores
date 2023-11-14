namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain;
    using Linn.Stores.Resources.Scs;

    public class ScsPalletResourceBuilder : IResourceBuilder<ScsPallet>
    {
        public ScsPalletResource Build(ScsPallet pallet)
        {
            return new ScsPalletResource
                       {
                           PalletNumber = pallet.PalletNumber,
                           Allocated = pallet.Allocated,
                           Disabled = pallet.Disabled,
                           CurrentLocation = new LocationResource
                                                 {
                                                     Area = pallet.Area,
                                                     Column = pallet.Column,
                                                     Level = pallet.Level,
                                                     Side = pallet.Side
                                                 },
                           HeatValue = pallet.HeatValue,
                           Height = pallet.Height,
                           RotationAverage = pallet.RotationAverage ?? 0
                       };
        }

        public string GetLocation(ScsPallet model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ScsPallet>.Build(ScsPallet pallet) => this.Build(pallet);
    }
}
