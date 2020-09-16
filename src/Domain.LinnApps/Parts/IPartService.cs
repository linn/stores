﻿namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public interface IPartService
    {
        void UpdatePart(Part from, Part to, List<string> privileges);

        Part CreatePart(Part partToCreate, List<string> privileges);
    }
}
