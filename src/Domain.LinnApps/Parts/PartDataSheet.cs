namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class PartDataSheet
    {
        public string PartNumber { get; set; }

        public Part Part { get; set; }

        public string PdfFilePath { get; set; }

        public int Sequence { get; set; }
    }
}
