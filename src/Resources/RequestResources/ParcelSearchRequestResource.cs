namespace Linn.Stores.Resources.RequestResources
{
    public class ParcelSearchRequestResource
    {
        public string ParcelNumberSearchTerm { get; set; }

        public string SupplierIdSearchTerm { get; set; }

        public string CarrierIdSearchTerm { get; set; }

        public string DateCreatedSearchTerm { get; set; }

        public string SupplierInvNoSearchTerm { get; set; }

        public string ConsignmentNoSearchTerm { get; set; }

        public string CommentsSearchTerm { get; set; }

        public bool? NoImportBooksAttachedOnly { get; set; }
    }
}
