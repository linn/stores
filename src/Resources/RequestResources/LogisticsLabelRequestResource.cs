namespace Linn.Stores.Resources.RequestResources
{
    public class LogisticsLabelRequestResource
    {
        public string LabelType { get; set; }

        public int? ConsignmentId { get; set; }

        public int? FirstItem { get; set; }

        public int? LastItem { get; set; }

        public int? AddressId { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string Line5 { get; set; }

        public int UserNumber { get; set; }

        public int NumberOfCopies { get; set; } = 1;
    }
}
