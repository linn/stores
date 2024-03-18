namespace Linn.Stores.Resources.RequestResources
{
    public class LogisticsLabelRequestResource
    {
        public string LabelType { get; set; }

        public int? ConsignmentId { get; set; }

        public int? FirstItem { get; set; }

        public int? LastItem { get; set; }

        public int? AddressId { get; set; }

        public int UserNumber { get; set; }

        public int NumberOfCopies { get; set; } = 1;
    }
}
