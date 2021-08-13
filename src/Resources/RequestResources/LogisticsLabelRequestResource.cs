namespace Linn.Stores.Resources.RequestResources
{
    public class LogisticsLabelRequestResource
    {
        public string LabelType { get; set; }

        public int ConsignmentId { get; set; }

        public int FirstItem { get; set; }

        public int? LastItem { get; set; }

        public int UserNumber { get; set; }
    }
}
