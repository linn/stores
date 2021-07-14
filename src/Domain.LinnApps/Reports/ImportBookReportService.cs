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

        public ImportBookReportService(IRepository<ImportBook, int> impbookRepository, IReportingHelper reportingHelper)
        {
            this.impbookRepository = impbookRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetIPRReport(DateTime from, DateTime to)
        {
            var results = this.impbookRepository.FilterBy(
                x =>

                    // x.IprCpcNumber.HasValue && x.IprCpcNumber.Value == 13 &&
                    // todo check if I can just use that ^
                    x.OrderDetails.Any(z => z.CpcNumber.HasValue && z.CpcNumber == 13)
                    && x.CustomsEntryCodeDate.HasValue && from < x.CustomsEntryCodeDate.Value
                    && x.CustomsEntryCodeDate < to);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateReportTitle(from, to));

            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("Id", "Import Book No Ref", GridDisplayType.TextValue),
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
                        new AxisDetailsModel("FaultCode", "Fault Code", GridDisplayType.TextValue),
                        new AxisDetailsModel("ReportedFault", "Reported Fault", GridDisplayType.TextValue),
                        new AxisDetailsModel("Analysis", GridDisplayType.TextValue),
                        new AxisDetailsModel("Cit", GridDisplayType.TextValue)
                    });

            var values = new List<CalculationValueModel>();

            // foreach (var assemblyFail in fails)
            // {
            // this.ExtractDetails(values, assemblyFail, weeks);
            // }
            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/logistics/import-books/{rowId}"));
            model.RowHeader = "Id";

            return model;
        }

        public ResultsModel GetAssemblyFailsDetailsReportExport(DateTime fromDate, DateTime toDate)
        {
            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.TextValue,
                null,
                this.GenerateReportTitle(fromDate, toDate));

            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("Week", GridDisplayType.TextValue),
                        new AxisDetailsModel("Date Found", GridDisplayType.TextValue) { AllowWrap = false },
                        new AxisDetailsModel("PartNumber", "Part Number", GridDisplayType.TextValue)
                            {
                                AllowWrap = false
                            },
                        new AxisDetailsModel("BoardPartNumber", "Board Part Number", GridDisplayType.TextValue)
                            {
                                AllowWrap = false
                            },
                        new AxisDetailsModel("Fails", GridDisplayType.TextValue),
                        new AxisDetailsModel("CircuitPartNumber", "Circuit Part Number", GridDisplayType.TextValue)
                            {
                                AllowWrap = false
                            },
                        new AxisDetailsModel("FaultCode", "Fault Code", GridDisplayType.TextValue),
                        new AxisDetailsModel("ReportedFault", "Reported Fault", GridDisplayType.TextValue),
                        new AxisDetailsModel("Analysis", GridDisplayType.TextValue),
                        new AxisDetailsModel("Cit", GridDisplayType.TextValue),
                        new AxisDetailsModel("Entered By", GridDisplayType.TextValue),
                        new AxisDetailsModel("Completed By", GridDisplayType.TextValue)
                    });

            // var filterQueries = this.GetAssemblyFailDataQueries(fromDate, toDate, null, null, null, null, null, null);
            // var fails = this.GetFails(filterQueries);
            var values = new List<CalculationValueModel>();

            // foreach (var assemblyFail in fails)
            // {
            // this.ExtractExportDetails(values, assemblyFail, weeks);
            // }
            reportLayout.SetGridData(values);
            var model = reportLayout.GetResultsModel();
            model.RowHeader = "Id";

            return model;
        }

        private void ExtractExportDetails(ICollection<CalculationValueModel> values, ImportBook impbook)
        {
            this.ExtractDetails(values, impbook);

            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = impbook.Id.ToString(),
            // ColumnId = "Date Found",
            // TextDisplay = impbook.DateTimeFound.ToString("dd-MMM-yyyy")
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "Entered By",
            // TextDisplay = assemblyFail.EnteredBy?.FullName
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "Completed By",
            // TextDisplay = assemblyFail.CompletedBy?.FullName
            // });
        }

        private void ExtractDetails(ICollection<CalculationValueModel> values, ImportBook impbook)
        {
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "Week",
            // TextDisplay = this.linnWeekService.GetWeek(assemblyFail.DateTimeFound, weeks).WWSYY
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "PartNumber",
            // TextDisplay = assemblyFail.WorksOrder?.PartNumber
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "BoardPartNumber",
            // TextDisplay = assemblyFail.BoardPartNumber
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "Fails",
            // TextDisplay = assemblyFail.NumberOfFails.ToString()
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "FaultCode",
            // TextDisplay = assemblyFail.FaultCode?.FaultCode
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "CircuitPartNumber",
            // TextDisplay = assemblyFail.CircuitPart
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "ReportedFault",
            // TextDisplay = assemblyFail.ReportedFault
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(), ColumnId = "Analysis", TextDisplay = assemblyFail.Analysis
            // });
            // values.Add(
            // new CalculationValueModel
            // {
            // RowId = assemblyFail.Id.ToString(),
            // ColumnId = "Cit",
            // TextDisplay = assemblyFail.CitResponsible?.Name
            // });
        }

        private string GenerateReportTitle(DateTime fromDate, DateTime toDate)
        {
            return $"IPR Impbooks between {fromDate:dd-MMM-yyyy} and {toDate:dd-MMM-yyyy}. ";
        }

        private IEnumerable<CalculationValueModel> CalculatedValues(
            IEnumerable<ImportBook> impbooks

            // AssemblyFailGroupBy groupBy,
        )
        {
            // switch (groupBy)
            // {
            // case AssemblyFailGroupBy.BoardPartNumber:
            // return fails.Select(
            // f => new CalculationValueModel
            // {
            // RowId = f.BoardPartNumber ?? string.Empty,
            // ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber
            // .ToString(),
            // Quantity = f.NumberOfFails
            // });
            // case AssemblyFailGroupBy.FaultCode:
            // return fails.Select(
            // f => new CalculationValueModel
            // {
            // RowId = f.FaultCode?.FaultCode ?? string.Empty,
            // RowTitle = f.FaultCode?.Description,
            // ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber
            // .ToString(),
            // Quantity = f.NumberOfFails
            // });
            // case AssemblyFailGroupBy.Board:
            // return fails.Select(
            // f => new CalculationValueModel
            // {
            // RowId = string.IsNullOrEmpty(f.BoardPartNumber)
            // ? string.Empty
            // :
            // f.BoardPartNumber.IndexOf('/') >= 0
            // ?
            // f.BoardPartNumber.Substring(0, f.BoardPartNumber.IndexOf('/'))
            // : f.BoardPartNumber,
            // ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber
            // .ToString(),
            // Quantity = f.NumberOfFails
            // });
            // case AssemblyFailGroupBy.CitCode:
            // return fails.Select(
            // f => new CalculationValueModel
            // {
            // RowId = f.CitResponsible?.Code ?? string.Empty,
            // RowTitle = f.CitResponsible?.Name,
            // ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber
            // .ToString(),
            // Quantity = f.NumberOfFails
            // });
            // case AssemblyFailGroupBy.CircuitPartNumber:
            // return fails.Select(
            // f => new CalculationValueModel
            // {
            // RowId = f.CircuitPart ?? string.Empty,
            // ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber
            // .ToString(),
            // Quantity = f.NumberOfFails
            // });
            // case AssemblyFailGroupBy.Person:
            // return fails.Select(
            // f => new CalculationValueModel
            // {
            // RowId = f.PersonResponsible?.Id.ToString() ?? string.Empty,
            // RowTitle = f.PersonResponsible?.FullName ?? string.Empty,
            // ColumnId = this.linnWeekService.GetWeek(f.DateTimeFound, weeks).LinnWeekNumber
            // .ToString(),
            // Quantity = f.NumberOfFails
            // });
            // default:
            // throw new ArgumentOutOfRangeException(nameof(groupBy), groupBy, null);
            // }
        }
    }
}
