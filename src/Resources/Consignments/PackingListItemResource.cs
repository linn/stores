﻿namespace Linn.Stores.Resources.Consignments
{
    public class PackingListItemResource
    {
        public int ItemNumber { get; set; }

        public int? ContainerNumber { get; set; }

        public string Description { get; set; }

        public string Weight { get; set; }

        public string DisplayDimensions { get; set; }
    }
}
