namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Layouts;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class UnmatchedParcelReportService
    {
        private readonly IFilterByWildcardRepository<UnmatchedParcel, int> unmatchedParcelRepository;

        private readonly IReportingHelper reportingHelper;

        public UnmatchedParcelReportService(
            IFilterByWildcardRepository<UnmatchedParcel, int> unmatchedParcelRepository,
            IReportingHelper reportingHelper)
        {
            this.unmatchedParcelRepository = unmatchedParcelRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetUnmatchParcelsReport(int supplierId)
        {
            var parcels = this.unmatchedParcelRepository
                .FilterBy(a => a.SupplierId == supplierId);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.Value,
                null,
                "Parcels that have no matching Import Book");

            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("ParcelNumber", "Parcel Number", GridDisplayType.Value),
                        new AxisDetailsModel("DateCreated", "Date Created", GridDisplayType.TextValue),
                        new AxisDetailsModel("Supplier", "Supplier", GridDisplayType.TextValue),
                        new AxisDetailsModel("CarrierName", "Carrier", GridDisplayType.TextValue),
                        new AxisDetailsModel("DateReceived", "Date Received", GridDisplayType.TextValue),
                        new AxisDetailsModel("CheckedBy", "Checked By", GridDisplayType.TextValue),
                        new AxisDetailsModel("ConsignmentNumber", "Consignment Number", GridDisplayType.TextValue)
                    });

            var values = new List<CalculationValueModel>();
            foreach (var parcel in parcels)
            {
                var rowId = $"{parcel.ParcelNumber}";
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "ParcelNumber",
                        Value = parcel.ParcelNumber
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "DateCreated",
                        TextDisplay = parcel.DateCreated.ToString("o")
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "Supplier",
                        TextDisplay = $"{parcel.SupplierId} {parcel.SupplierName}"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "Carrier",
                        TextDisplay = parcel.CarrierName
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "DateReceived",
                        TextDisplay = parcel.DateReceived.ToString("o")
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "CheckedBy",
                        TextDisplay = parcel.CheckedBy
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = rowId,
                        ColumnId = "ConsignmentNumber",
                        TextDisplay = parcel.ConsignmentNumber
                    });
            }

            reportLayout.SetGridData(values);
            return reportLayout.GetResultsModel();
        }
    }
}
