namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class ZeroValuedInvoiceDetailsReportService : IZeroValuedInvoiceDetailsReportService
    {
        private readonly IRepository<InvoiceDetail, InvoiceDetailKey> repository;

        private readonly IReportingHelper reportingHelper;

        public ZeroValuedInvoiceDetailsReportService(
            IRepository<InvoiceDetail, InvoiceDetailKey> repository,
            IReportingHelper reportingHelper)
        {
            this.repository = repository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetZeroValuedInvoicedItemsBetweenDates(DateTime from, DateTime to)
        {
            var lines = this.repository.FilterBy(
                x => x.Total == 0 && (x.Invoice.DocumentDate > from && x.Invoice.DocumentDate > to));

            var model = new ResultsModel
                            {
                                ReportTitle 
                                    = new NameModel($"Zero Valued Invoiced Items: {from.ToShortDateString()} - {to.ToShortDateString()}")
                            };

            var cols = new List<AxisDetailsModel>
                                  {
                                      new AxisDetailsModel("Account Id")
                                          {
                                              SortOrder = 0, GridDisplayType = GridDisplayType.TextValue
                                          },
                                      new AxisDetailsModel("Account Name")
                                          {
                                              SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                                          },
                                      new AxisDetailsModel("Country")
                                          {
                                              SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                                          },
                                      new AxisDetailsModel("Part Number")
                                          {
                                              SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                                          },
                                      new AxisDetailsModel("Part Description")
                                          {
                                              SortOrder = 4, GridDisplayType = GridDisplayType.TextValue
                                          },
                                      new AxisDetailsModel("Invoice")
                                          {
                                              SortOrder = 5, GridDisplayType = GridDisplayType.TextValue
                                          },
                                      new AxisDetailsModel("Line")
                                          {
                                              SortOrder = 6, GridDisplayType = GridDisplayType.TextValue
                                          }
                                  };

            model.AddSortedColumns(cols);

            var values = new List<CalculationValueModel>();

            foreach (var line in lines)
            {
                var rowId = $"{line.InvoiceNumber}{line.LineNumber}";

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.Invoice.AccountId.ToString(),
                            ColumnId = "Account Id"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.Invoice.Account.AccountName,
                            ColumnId = "Account Name"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.Invoice.DeliveryAddress.CountryCode,
                            ColumnId = "Country"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.PartNumber,
                            ColumnId = "Part Number"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.PartDescription,
                            ColumnId = "Part Description"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.InvoiceNumber.ToString(),
                            ColumnId = "Invoice"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            TextDisplay = line.LineNumber.ToString(),
                            ColumnId = "Line"
                        });
            }

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }
    }
}
