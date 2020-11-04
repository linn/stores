﻿namespace Linn.Stores.Resources.Parts
{
    public class PartTemplateResource
    {
        public string PartRoot { get; set; }

        public string Description { get; set; }

        public string HasDataSheet { get; set; }

        public string HasNumberSequence { get; set; }

        public int? NextNumber { get; set; }

        public string AccountingCompany { get; set; }

        public string ProductCode { get; set; }

        public string StockControlled { get; set; }

        public string LinnProduced { get; set; }

        public string BomType { get; set; }

        public string AssemblyTechnology { get; set; }

        public string AllowPartCreation { get; set; }

        public string ParetoCode { get; set; }
    }
}
