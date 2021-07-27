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

        private readonly IRepository<Country, string> countryRepository;

        private readonly IRepository<ExportReturnDetail, ExportReturnDetailKey> exportReturnDetailRepository;

        private readonly IReportingHelper reportingHelper;

        private readonly int IprCpcNumberId = 13;

        public ImportBookReportService(
            IRepository<ImportBook, int> impbookRepository,
            IRepository<Country, string> countryRepository,
            IRepository<ExportReturnDetail, ExportReturnDetailKey> exportReturnDetailRepository,
            IReportingHelper reportingHelper)
        {
            this.impbookRepository = impbookRepository;
            this.countryRepository = countryRepository;
            this.exportReturnDetailRepository = exportReturnDetailRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetIPRReport(DateTime from, DateTime to, bool iprResults = true)
        {
            var euCountries = this.countryRepository.FilterBy(x => x.ECMember == "Y").Select(z => z.CountryCode)
                .ToList();
            var iprImpbooks = this.impbookRepository.FilterBy(
                x => x.OrderDetails.Any(
                         z => (z.CpcNumber.HasValue && z.CpcNumber.Value == this.IprCpcNumberId) == iprResults)
                     && x.CustomsEntryCodeDate.HasValue && from < x.CustomsEntryCodeDate.Value
                     && x.CustomsEntryCodeDate.Value < to);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateIprReportTitle(from, to, iprResults));

            this.AddReportColumns(reportLayout);

            var values = new List<CalculationValueModel>();

            foreach (var impbook in iprImpbooks)
            {
                var isInEU = euCountries.Contains(impbook.Supplier.CountryCode);

                foreach (var orderDetail in impbook.OrderDetails.Where(
                    x => (x.CpcNumber.HasValue && x.CpcNumber.Value == this.IprCpcNumberId) == iprResults))
                {
                    this.ExtractDetails(values, impbook, orderDetail, isInEU);
                }
            }

            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/logistics/import-books/{textValue}"));
            model.RowHeader = "Import Book Number/Ref";

            return model;
        }

        public ResultsModel GetIPRExport(DateTime from, DateTime to, bool iprResults = true)
        {
            var euCountries = this.countryRepository.FilterBy(x => x.ECMember == "Y").Select(z => z.CountryCode)
                .ToList();

            var iprImpbooks = this.impbookRepository.FilterBy(
                x => x.OrderDetails.Any(
                         z => (z.CpcNumber.HasValue && z.CpcNumber.Value == this.IprCpcNumberId) == iprResults)
                     && x.CustomsEntryCodeDate.HasValue && from < x.CustomsEntryCodeDate.Value
                     && x.CustomsEntryCodeDate.Value < to);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateIprReportTitle(from, to, iprResults));

            this.AddExportColumns(reportLayout);

            var values = new List<CalculationValueModel>();

            foreach (var impbook in iprImpbooks)
            {
                var isInEU = euCountries.Contains(impbook.Supplier.CountryCode);
                foreach (var orderDetail in impbook.OrderDetails.Where(
                    x => (x.CpcNumber.HasValue && x.CpcNumber.Value == this.IprCpcNumberId) == iprResults))
                {
                    this.ExtractExportDetails(values, impbook, orderDetail, isInEU);
                }
            }

            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/logistics/import-books/{textValue}"));
            model.RowHeader = "Import Book Number/Ref";

            return model;
        }

        public ResultsModel GetEUReport(DateTime from, DateTime to, bool euResults = true)
        {
            var euCountries = this.countryRepository.FilterBy(x => x.ECMember == "Y").Select(z => z.CountryCode)
                .ToList();

            var iprImpbooks = this.impbookRepository.FilterBy(
                x => (euCountries.Contains(x.Supplier.CountryCode) == euResults) && x.CustomsEntryCodeDate.HasValue
                     && from < x.CustomsEntryCodeDate.Value && x.CustomsEntryCodeDate.Value < to);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateEUReportTitle(from, to, euResults));

            this.AddReportColumns(reportLayout);

            var values = new List<CalculationValueModel>();

            foreach (var impbook in iprImpbooks)
            {
                foreach (var orderDetail in impbook.OrderDetails)
                {
                    this.ExtractDetails(values, impbook, orderDetail, euResults);
                }
            }

            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/logistics/import-books/{textValue}"));
            model.RowHeader = "Import Book Number/Ref";

            return model;
        }

        public ResultsModel GetEUExport(DateTime from, DateTime to, bool euResults = true)
        {
            var euCountries = this.countryRepository.FilterBy(x => x.ECMember == "Y").Select(z => z.CountryCode)
                .ToList();

            var iprImpbooks = this.impbookRepository.FilterBy(
                x => (euCountries.Contains(x.Supplier.CountryCode) == euResults) && x.CustomsEntryCodeDate.HasValue
                     && from < x.CustomsEntryCodeDate.Value && x.CustomsEntryCodeDate.Value < to);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateEUReportTitle(from, to, euResults));

            this.AddExportColumns(reportLayout);

            var values = new List<CalculationValueModel>();

            foreach (var impbook in iprImpbooks)
            {
                var isInEU = euCountries.Contains(impbook.Supplier.CountryCode);
                foreach (var orderDetail in impbook.OrderDetails)
                {
                    this.ExtractExportDetails(values, impbook, orderDetail, isInEU);
                }
            }

            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/logistics/import-books/{textValue}"));
            model.RowHeader = "Import Book Number/Ref";

            return model;
        }

        private void AddReportColumns(SimpleGridLayout reportLayout)
        {
            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("RsnNo", "Unique RSN No", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("InvNo", "Job Ref or Invoice Number", GridDisplayType.TextValue),
                        new AxisDetailsModel("SupplierCountry", "Country Importing From", GridDisplayType.TextValue),
                        new AxisDetailsModel("Carrier", "Import Agent (Carrier)", GridDisplayType.TextValue),
                        new AxisDetailsModel(
                            "ShippingRef",
                            "Import AWB/Transport Bill No (Shipping Ref)",
                            GridDisplayType.TextValue),
                        new AxisDetailsModel("CustomsEntryCode", "Import Entry Code", GridDisplayType.TextValue),
                        new AxisDetailsModel(
                            "CustomsEntryCodeDate",
                            "Date of Entry (customs)",
                            GridDisplayType.TextValue),
                        new AxisDetailsModel("TariffCode", "Commodity (tariff) code", GridDisplayType.TextValue),
                        new AxisDetailsModel("OrderDescription", "Goods Description", GridDisplayType.TextValue),
                        new AxisDetailsModel("Qty", "Quantity", GridDisplayType.TextValue),
                        new AxisDetailsModel("OriginalCurrency", "Original Currency", GridDisplayType.TextValue),
                        new AxisDetailsModel("ForeignValue", "Value (in original currency)", GridDisplayType.TextValue),
                    });
        }

        private void AddExportColumns(SimpleGridLayout reportLayout)
        {
            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("RsnNo", "Unique RSN No", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("InvNo", "Job Ref or Invoice Number", GridDisplayType.TextValue),
                        new AxisDetailsModel("CustomerName", "Customer Name", GridDisplayType.TextValue),
                        new AxisDetailsModel("SupplierCountry", "Country Importing From", GridDisplayType.TextValue),
                        new AxisDetailsModel("Carrier", "Import Agent (Carrier)", GridDisplayType.TextValue),
                        new AxisDetailsModel(
                            "ShippingRef",
                            "Import AWB/Transport Bill No (Shipping Ref)",
                            GridDisplayType.TextValue),
                        new AxisDetailsModel("CustomsEntryCode", "Import Entry Code", GridDisplayType.TextValue),
                        new AxisDetailsModel(
                            "CustomsEntryCodeDate",
                            "Date of Entry (customs)",
                            GridDisplayType.TextValue),
                        new AxisDetailsModel("EconomicCode", "Economic Code", GridDisplayType.TextValue),
                        new AxisDetailsModel("TariffCode", "Commodity (tariff) code", GridDisplayType.TextValue),
                        new AxisDetailsModel("OrderDescription", "Goods Description", GridDisplayType.TextValue),
                        new AxisDetailsModel("DutyRate", "Rate Of Duty", GridDisplayType.TextValue),
                        new AxisDetailsModel("Qty", "Quantity", GridDisplayType.TextValue),
                        new AxisDetailsModel("OriginalCurrency", "Original Currency", GridDisplayType.TextValue),
                        new AxisDetailsModel("ForeignValue", "Value (in original currency)", GridDisplayType.TextValue),
                        new AxisDetailsModel("ExchangeRate", "R/E", GridDisplayType.TextValue),
                        new AxisDetailsModel("Currency", GridDisplayType.TextValue),
                        new AxisDetailsModel("GBPValue", "GBP/Customs Value", GridDisplayType.TextValue),
                        new AxisDetailsModel("Quarter", "Quarter or Month for BOD", GridDisplayType.TextValue),
                    });
        }

        private void ExtractDetails(
            ICollection<CalculationValueModel> values,
            ImportBook impbook,
            ImportBookOrderDetail orderDetail,
            bool isInEU)
        {
            var exportReturnDetail = (ExportReturnDetail)null;

            if (isInEU && orderDetail.RsnNumber.HasValue)
            {
                exportReturnDetail = this.exportReturnDetailRepository.FindBy(
                    x => x.RsnNumber == orderDetail.RsnNumber.Value);
            }

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
                        ColumnId = "InvNo",
                        TextDisplay =
                            exportReturnDetail != null
                                ? exportReturnDetail.InterCompanyInvoice.DocumentNumber.ToString()
                                : string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "SupplierCountry",
                        TextDisplay = impbook.Supplier.CountryCode,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "Carrier",
                        TextDisplay = impbook.Carrier.Name,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "ShippingRef",
                        TextDisplay = impbook.TransportBillNumber,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "CustomsEntryCode",
                        TextDisplay = $"{impbook.CustomsEntryCodePrefix} - {impbook.CustomsEntryCode}",
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "CustomsEntryCodeDate",
                        TextDisplay = impbook.CustomsEntryCodeDate?.ToString("dd-MMM-yyyy"),
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
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "OrderDescription",
                        TextDisplay = orderDetail.OrderDescription,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "Qty",
                        TextDisplay = orderDetail.Qty.ToString(),
                        RowTitle = impbook.Id.ToString()
                    });

            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "OriginalCurrency",
                        TextDisplay = impbook.Currency,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "ForeignValue",
                        TextDisplay =
                            exportReturnDetail != null
                                ? exportReturnDetail.CustomsValue?.ToString()
                                : string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });
        }

        private void ExtractExportDetails(
            ICollection<CalculationValueModel> values,
            ImportBook impbook,
            ImportBookOrderDetail orderDetail,
            bool isInEU)
        {
            var exportReturnDetail = (ExportReturnDetail)null;

            if (isInEU && orderDetail.RsnNumber.HasValue)
            {
                exportReturnDetail = this.exportReturnDetailRepository.FindBy(
                    x => x.RsnNumber == orderDetail.RsnNumber.Value);
            }

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
                        ColumnId = "InvNo",
                        TextDisplay =
                            exportReturnDetail != null
                                ? exportReturnDetail.InterCompanyInvoice.DocumentNumber.ToString()
                                : string.Empty,
                    RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "CustomerName",
                        TextDisplay = isInEU ? "Linn Belgium" : string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });

            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "SupplierCountry",
                        TextDisplay = isInEU ? "Belgium" : impbook.Supplier.CountryCode,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "Carrier",
                        TextDisplay = impbook.Carrier.Name,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "ShippingRef",
                        TextDisplay = impbook.TransportBillNumber,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "CustomsEntryCode",
                        TextDisplay = $"{impbook.CustomsEntryCodePrefix} - {impbook.CustomsEntryCode}",
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "CustomsEntryCodeDate",
                        TextDisplay = impbook.CustomsEntryCodeDate?.ToString("dd-MMM-yyyy"),
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "EconomicCode",
                        TextDisplay = string.Empty,
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
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "OrderDescription",
                        TextDisplay = orderDetail.OrderDescription,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "DutyRate",
                        TextDisplay = string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "Qty",
                        TextDisplay = orderDetail.Qty.ToString(),
                        RowTitle = impbook.Id.ToString()
                    });

            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "OriginalCurrency",
                        TextDisplay = impbook.Currency,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "ForeignValue",
                        TextDisplay =
                            exportReturnDetail != null
                                ? exportReturnDetail.CustomsValue?.ToString()
                                : string.Empty,
                    RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "ExchangeRate",
                        TextDisplay = string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "Currency",
                        TextDisplay = string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });

            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "GBPValue",
                        TextDisplay = string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });
            values.Add(
                new CalculationValueModel
                    {
                        RowId = $"{impbook.Id.ToString()}/{orderDetail.LineNumber}",
                        ColumnId = "Quarter",
                        TextDisplay = string.Empty,
                        RowTitle = impbook.Id.ToString()
                    });
        }

        private string GenerateIprReportTitle(DateTime fromDate, DateTime toDate, bool ipr)
        {
            var nonText = ipr ? string.Empty : "Non-";

            return $"{nonText}IPR Impbooks between {fromDate:dd-MMM-yyyy} and {toDate:dd-MMM-yyyy}";
        }

        private string GenerateEUReportTitle(DateTime fromDate, DateTime toDate, bool eu)
        {
            var nonText = eu ? string.Empty : "Non-";
            return $"{nonText}EU Impbooks between {fromDate:dd-MMM-yyyy} and {toDate:dd-MMM-yyyy}";
        }
    }
}
