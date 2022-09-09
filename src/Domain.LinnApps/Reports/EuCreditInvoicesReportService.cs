namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Layouts;
    using Linn.Common.Reporting.Models;

    public class EuCreditInvoicesReportService : IEuCreditInvoicesReportService
    {
        private readonly IQueryRepository<EuCreditInvoice> repository;

        private readonly IReportingHelper reportingHelper;

        public EuCreditInvoicesReportService(
            IQueryRepository<EuCreditInvoice> repository, IReportingHelper reportingHelper)
        {
            this.repository = repository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetReport(DateTime from, DateTime to)
        {
            var data = this.repository.FilterBy(x => x.InvoiceDate >= from && x.InvoiceDate <= to);
            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.Value,
                null,
                "QC controlled parts that have an MR requirement or sales forecast and that aren't obsolete.");
            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("AccountName", "Account", GridDisplayType.TextValue),
                        new AxisDetailsModel("Invoice", "Invoice", GridDisplayType.TextValue),
                        new AxisDetailsModel("LineNo", "Line", GridDisplayType.TextValue),
                        new AxisDetailsModel("GoodsTotal", "Goods", GridDisplayType.Value) { DecimalPlaces = 2 },
                        new AxisDetailsModel("VatTotal", "Vat", GridDisplayType.Value) { DecimalPlaces = 2 },
                        new AxisDetailsModel("DocumentTotal", "Total", GridDisplayType.Value) { DecimalPlaces = 2 },
                        new AxisDetailsModel("RsnNumber", "RSN", GridDisplayType.TextValue),
                        new AxisDetailsModel("CreditNoteNumber", "Credit Note", GridDisplayType.TextValue),
                        new AxisDetailsModel("CreditCode", "Credit Code", GridDisplayType.TextValue),
                        new AxisDetailsModel("CreditCodeDescription", "Description", GridDisplayType.TextValue)
                    });

            var values = new List<CalculationValueModel>();
            foreach (var datum in data)
            {
                var rowId = $"{datum.Invoice}/{datum.RsnNumber}";
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "AccountName",
                            TextDisplay = datum.AccountName
                    });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Invoice",
                            TextDisplay = datum.Invoice.ToString()
                    });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "LineNo",
                            TextDisplay = datum.LineNo.ToString()
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "GoodsTotal",
                            Value = datum.GoodsTotal
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "VatTotal",
                            Value = datum.VatTotal
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "DocumentTotal",
                            Value = datum.DocumentTotal
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "RsnNumber",
                            TextDisplay = datum.RsnNumber.ToString()
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "CreditNoteNumber",
                            TextDisplay = datum.CreditNoteNumber.ToString()
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "CreditCode",
                            TextDisplay = datum.CreditCode
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "CreditCodeDescription",
                            TextDisplay = datum.CreditCodeDescription
                        });
            }

            reportLayout.SetGridData(values);
            return reportLayout.GetResultsModel();
        }
    }
}
