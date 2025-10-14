namespace Linn.Stores.Domain.LinnApps
{
    public class PrinterMapping
    {
        public int Id { get; set; }

        public string PrinterName { get; set; }

        public int UserNumber { get; set; }

        public string PrinterGroup { get; set; }

        public string PrinterType { get; set; }

        public string DefaultForGroup { get; set; }

        public string PrinterUri { get; set; }
    }
}
