namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Layouts;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookReportService : IImportBookReportService
    {
        private readonly IRepository<ImportBook, int> impbookRepository;

        private readonly IReportingHelper reportingHelper;

        private readonly int IprCpcNumberId = 13;

        public ImportBookReportService(IRepository<ImportBook, int> impbookRepository, IReportingHelper reportingHelper)
        {
            this.impbookRepository = impbookRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetIPRReport(DateTime from, DateTime to)
        {
            var iprImpbooks = this.impbookRepository.FilterBy(
                x =>

                    // x.IprCpcNumber.HasValue && x.IprCpcNumber.Value == 13 &&
                    // todo check if I can just use that ^, not sure it'll be populated reliably 
                    x.OrderDetails.Any(z => z.CpcNumber.HasValue && z.CpcNumber.Value == this.IprCpcNumberId)
                    && x.CustomsEntryCodeDate.HasValue && from < x.CustomsEntryCodeDate.Value
                    && x.CustomsEntryCodeDate.Value < to);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateReportTitle(from, to));

            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("RsnNo", "Unique RSN No", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("Currency", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel(
                            "CustomsEntryCodeDate",
                            "Date of Entry (customs)",
                            GridDisplayType.TextValue),
                        new AxisDetailsModel("TariffCode", "Commodity (tariff) code", GridDisplayType.TextValue)
                            {
                                AllowWrap = false
                            },
                    });

            var values = new List<CalculationValueModel>();

            foreach (var impbook in iprImpbooks)
            {
                foreach (var orderDetail in impbook.OrderDetails.Where(
                    x => x.CpcNumber.HasValue && x.CpcNumber.Value == this.IprCpcNumberId))
                {
                    this.ExtractDetails(values, impbook, orderDetail);
                }
            }

            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/logistics/import-books"));
            model.RowHeader = "Import Book Number/Ref";

            return model;
        }

        private void ExtractDetails(
            ICollection<CalculationValueModel> values,
            ImportBook impbook,
            ImportBookOrderDetail orderDetail)
        {
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "RsnNo",
                        TextDisplay = orderDetail.RsnNumber?.ToString(),
                        RowTitle = impbook.Id.ToString()
                });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                    ColumnId = "Currency",
                        TextDisplay = impbook.Currency,
                        RowTitle = impbook.Id.ToString()
                });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                    ColumnId = "CustomsEntryCodeDate",
                        TextDisplay = impbook.CustomsEntryCodeDate?.ToString("o"),
                        RowTitle = impbook.Id.ToString()
                });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "TariffCode",
                        TextDisplay = orderDetail.TariffCode,
                        RowTitle = impbook.Id.ToString()
                });
        }

        private string GenerateReportTitle(DateTime fromDate, DateTime toDate)
        {
            return $"IPR Impbooks between {fromDate:dd-MMM-yyyy} and {toDate:dd-MMM-yyyy}";
        }
    }
}
