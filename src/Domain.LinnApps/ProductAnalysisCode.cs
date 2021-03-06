﻿namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Parts;

    public class ProductAnalysisCode
    {
        public string ProductCode { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> Parts { get; set; }
    }
}
