namespace Linn.Stores.Resources.RequestResources
{
    public class MechPartSourceSearchResource
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public int? CreatedBy { get; set; }

        public string Description { get; set; }

        public string PartNumber { get; set; }

        public string ProjectDeptCode { get; set; }
    }
}
