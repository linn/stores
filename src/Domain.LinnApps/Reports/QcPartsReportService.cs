namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Layouts;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class QcPartsReportService : IQcPartsReportService
    {
        private readonly IQueryRepository<MrPart> repository;

        private readonly IReportingHelper reportingHelper;

        public QcPartsReportService(
            IReportingHelper reportingHelper, 
            IQueryRepository<MrPart> repository)
        {
            this.repository = repository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetReport()
        {
            var excludedSuppliers = new int[3] { 058894, 101837, 9716 };

            var data = this.repository.FindAll().Where(p =>
                p.Part.QcOnReceipt.Equals("Y") 
                && !p.Part.DatePhasedOut.HasValue 
                && (p.Part.BomType == "C" || p.Part.BomType == "A")
                && p.Part.LinnProduced.Equals("N") 
                && !excludedSuppliers.Contains(p.Part.PreferredSupplierId.Value)
                && (p.Part.ParetoClass.ParetoCode == "A"
                    || p.Part.ParetoClass.ParetoCode == "B"
                    || p.Part.ParetoClass.ParetoCode == "C")); 

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.Value,
                null,
                "QC controlled parts that have an MR requirement or sales forecast and that aren't obsolete.");                                       
            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("PartNumber", "Part", GridDisplayType.TextValue),
                        new AxisDetailsModel("Desc", "Desc", GridDisplayType.TextValue),
                        new AxisDetailsModel("Supplier", "Supplier", GridDisplayType.TextValue),
                        new AxisDetailsModel("Who", "Who", GridDisplayType.TextValue),
                        new AxisDetailsModel("Why", "Why", GridDisplayType.TextValue),
                        new AxisDetailsModel("When", "When", GridDisplayType.TextValue)
                    });

            var values = new List<CalculationValueModel>();
            foreach (var datum in data)
            {
                var currentRowId = datum.PartNumber;
                var qc = datum.Part.QcControls
                    .Where(x => x.OnOrOffQc.Equals("ON")).OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = currentRowId,
                            ColumnId = "PartNumber",
                            TextDisplay = datum.PartNumber
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = currentRowId,
                            ColumnId = "Desc",
                            TextDisplay = datum.Part.Description
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = currentRowId,
                            ColumnId = "Supplier",
                            TextDisplay = datum.Part.PreferredSupplier.Name
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = currentRowId,
                            ColumnId = "Who",
                            TextDisplay = qc?.Employee?.FullName
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = currentRowId,
                            ColumnId = "Why",
                            TextDisplay = qc?.Reason
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = currentRowId,
                            ColumnId = "When",
                            TextDisplay = qc?.TransactionDate.ToShortDateString()
                        });
            }

            reportLayout.SetGridData(values);
            return reportLayout.GetResultsModel();
        }
    }
}
