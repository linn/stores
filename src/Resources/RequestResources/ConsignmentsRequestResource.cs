namespace Linn.Stores.Resources.RequestResources
{
    public class ConsignmentsRequestResource : FromToDateResource
    {
        public string searchTerm { get; set; }

        public int? ConsignmentId { get; set; }

        public int? HubId { get; set; }
    }
}
