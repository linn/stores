﻿namespace Linn.Stores.Facade.Extensions
{
    using Linn.Stores.Domain;
    using Linn.Stores.Domain.LinnApps.Wcs;

    public static class WarehouseLocationExtensions
    {
        public static ScsPallet ToScsPallet(this WarehouseLocation location)
        {
            return new ScsPallet
                       {
                           PalletNumber = location.PalletId.Value, 
                           Area = location.ScsAreaCode(),
                           Column = location.ScsColumnIndex(),
                           Level = location.ScsLevelIndex(),
                           Side = location.ScsSideIndex(),
                           Height = location.Pallet.ScsHeight(),
                           HeatValue = location.Pallet.ScsHeat(),
                           RotationAverage = location.Pallet.RotationAverage
                       };
        }
    }
}