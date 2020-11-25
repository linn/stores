namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class PartParamData
    {
        public string PartNumber { get; set; }

        public Part Part { get; set; }

        public string AttributeSet { get; set; }

        public decimal? Capacitance { get; set; }

        public decimal? NegativeTolerance { get; set; }

        public decimal? PositiveTolerance { get; set; }

        public string Dielectric { get; set; }

        public string Package { get; set; }

        public decimal? Pitch { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        public decimal? Diameter { get; set; }

        public string TransistorType { get; set; }

        public string Polarity { get; set; }

        public decimal? Current { get; set; }

        public decimal? Resistance { get; set; }

        public string Construction { get; set; }

        public decimal? Power { get; set; }

        public int? TemperatureCoefficient { get; set; }

        public string IcType { get; set; }

        public string IcFunction { get; set; }

        public decimal? Voltage { get; set; }
    }
}
