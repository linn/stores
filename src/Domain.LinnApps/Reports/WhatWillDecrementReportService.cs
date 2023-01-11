namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class WhatWillDecrementReportService : IWhatWillDecrementReportService
    {
        private readonly IProductionTriggerLevelsService productionTriggerLevelsService;

        private readonly IWwdPack wwdPack;

        private readonly IQueryRepository<WwdWork> wwdWorkRepository;

        private readonly IQueryRepository<WwdWorkDetail> wwdWorkDetailsRepository;

        private readonly IQueryRepository<ChangeRequest> changeRequestRepository;

        private readonly IReportingHelper reportingHelper;

        public WhatWillDecrementReportService(
            IProductionTriggerLevelsService productionTriggerLevelsService,
            IWwdPack wwdPack,
            IQueryRepository<WwdWork> wwdWorkRepository,
            IQueryRepository<WwdWorkDetail> wwdWorkDetailsRepository,
            IQueryRepository<ChangeRequest> changeRequestRepository,
            IReportingHelper reportingHelper)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;
            this.wwdPack = wwdPack;
            this.wwdWorkRepository = wwdWorkRepository;
            this.wwdWorkDetailsRepository = wwdWorkDetailsRepository;
            this.changeRequestRepository = changeRequestRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel WhatWillDecrementReport(string partNumber, int quantity, string typeOfRun, string workstationCode)
        {
            if (string.IsNullOrWhiteSpace(partNumber))
            {
                throw new EmptyException("You must provide a part number");
            }

            partNumber = partNumber.ToUpper();

            if (string.IsNullOrEmpty(workstationCode))
            {
                workstationCode = this.productionTriggerLevelsService.GetWorkStationCode(partNumber);
            }

            var jobId = this.wwdPack.WWD(partNumber, workstationCode, quantity);

            var changeRequests = this.changeRequestRepository.FilterBy(c => c.ChangeState == "ACCEPT").ToList();

            var wwdWorks = this.wwdWorkRepository
                .FilterBy(w => w.JobId == jobId && (w.Part.BomType == "A" || w.Part.BomType == "C")).ToList();

            if (typeOfRun == "SHORTAGES ONLY")
            {
                wwdWorks = wwdWorks.Where(w => w.QuantityKitted > w.QuantityAtLocation).ToList();
            }

            var wwdWorkDetails = this.wwdWorkDetailsRepository.FilterBy(w => w.JobId == jobId).ToList();

            changeRequests = changeRequests.Where(c => wwdWorks.Any(w => w.PartNumber == c.OldPartNumber)).ToList();

            var model = new ResultsModel
                            {
                                ReportTitle = new NameModel(
                                    $"{quantity.ToString()} x {partNumber} from workstation: {workstationCode}")
                            };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var sortedWwdWorks = wwdWorks.OrderBy(w => w.StoragePlace == null).ThenBy(w => w.StoragePlace)
                .ThenBy(w => w.PartNumber);

            var values = this.SetModelRows(sortedWwdWorks, wwdWorkDetails, changeRequests);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            model.RowDrillDownTemplates.Add(new DrillDownModel("Id", "/inventory/reports/what-will-decrement/report?partNumber={textValue}&quantity=" + $"{quantity}&typeOfRun{typeOfRun}"));
            model.RowHeader = "Part Number";

            return model;
        }

        private List<CalculationValueModel> SetModelRows(
            IEnumerable<WwdWork> wwdWorks,
            IEnumerable<WwdWorkDetail> wwdWorkDetails,
            IEnumerable<ChangeRequest> changeRequests)
        {
            var values = new List<CalculationValueModel>();

            foreach (var wwdWork in wwdWorks)
            {
                var wwdWorkDetail = wwdWorkDetails.FirstOrDefault(
                    w => w.JobId == wwdWork.JobId && w.PartNumber == wwdWork.PartNumber);

                var changeRequest = changeRequests.FirstOrDefault(c => c.OldPartNumber == wwdWork.PartNumber);

                var changeRemarks = changeRequest != null
                                        ? $"{changeRequest.OldPartNumber} change to {changeRequest.NewPartNumber}"
                                        : string.Empty;
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = wwdWork.Part.Description, ColumnId = "Description"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber,
                            TextDisplay = wwdWork.QuantityKitted.ToString() ?? "0",
                            ColumnId = "Qty Kitted"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber,
                            TextDisplay = wwdWork.StoragePlace,
                            ColumnId = "Work Station Storage Place"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber,
                            TextDisplay = wwdWork.QuantityAtLocation.ToString() ?? "0",
                            ColumnId = "Qty at Work Station"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber,
                            TextDisplay = wwdWork.Remarks,
                            ColumnId = "Remarks",
                            Attributes = !string.IsNullOrWhiteSpace(wwdWork.Remarks) && wwdWork.Remarks.Contains("totally SHORT")
                                             ? new List<ReportAttribute>
                                                   {
                                                       new ReportAttribute(ReportAttributeType.TextColour, "red")
                                                   }
                                             : null
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = wwdWorkDetail?.LocationGroup, ColumnId = "Site"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = wwdWorkDetail?.State, ColumnId = "State"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber,
                            TextDisplay = wwdWorkDetail?.Quantity.ToString() ?? "0",
                            ColumnId = "Qty"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = changeRemarks, ColumnId = "Change"
                        });
            }

            return values;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Description")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Qty Kitted")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Work Station Storage Place", "Storage Place")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Qty at Work Station", "Qty At WS")
                               {
                                   SortOrder = 4, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Remarks")
                               {
                                   SortOrder = 5, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Site") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("State") { SortOrder = 7, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Qty") { SortOrder = 8, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Change") { SortOrder = 9, GridDisplayType = GridDisplayType.TextValue }
                       };
        }
    }
}
