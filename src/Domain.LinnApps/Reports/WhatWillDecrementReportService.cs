﻿namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class WhatWillDecrementReportService : IWhatWillDecrementReportService
    {
        private readonly IProductionTriggerLevelsService productionTriggerLevelsService;

        private readonly IWwdPack wwdPack;

        private readonly IQueryRepository<WwdWork> wwdWorkRepository;

        private readonly IQueryRepository<WwdWorkDetail> wwdWorkDetailsRepository;

        private readonly IQueryRepository<ChangeRequest> changeRequestRepository;

        private readonly IRepository<Part, int> partsRepositry;

        private readonly IReportingHelper reportingHelper;

        public WhatWillDecrementReportService(
            IProductionTriggerLevelsService productionTriggerLevelsService,
            IWwdPack wwdPack,
            IQueryRepository<WwdWork> wwdWorkRepository,
            IQueryRepository<WwdWorkDetail> wwdWorkDetailsRepository,
            IQueryRepository<ChangeRequest> changeRequestRepository,
            IRepository<Part, int> partsRepositry,
            IReportingHelper reportingHelper)
        {
            this.productionTriggerLevelsService = productionTriggerLevelsService;
            this.wwdPack = wwdPack;
            this.wwdWorkRepository = wwdWorkRepository;
            this.wwdWorkDetailsRepository = wwdWorkDetailsRepository;
            this.changeRequestRepository = changeRequestRepository;
            this.partsRepositry = partsRepositry;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel WhatWillDecrementReport(string partNumber, int quantity, string typeOfRun, string workstationCode)
        {
            if (string.IsNullOrEmpty(workstationCode))
            {
                workstationCode = this.productionTriggerLevelsService.GetWorkStationCode(partNumber);
            }

            this.wwdPack.WWD(partNumber, workstationCode, quantity);

            var jobId = this.wwdPack.JobId();

            var changeRequests = this.changeRequestRepository.FilterBy(c => c.ChangeState == "ACCEPT").ToList();

            var wwdWorks = this.wwdWorkRepository.FilterBy(w => w.JobId == jobId).ToList();

            var parts = this.partsRepositry.FilterBy(
                p => wwdWorks.Any(w => w.PartNumber == p.PartNumber) && (p.BomType == "A" || p.BomType == "C")).ToList();

            if (typeOfRun == "SHORTAGES ONLY")
            {
                wwdWorks = wwdWorks.Where(w => w.QuantityKitted > w.QuantityAtLocation).ToList();
            }

            wwdWorks.OrderBy(w => w.StoragePlace).ThenBy(w => w.PartNumber);

            var wwdWorkDetails = this.wwdWorkDetailsRepository.FilterBy(w => w.JobId == jobId).ToList();

            changeRequests = changeRequests.Where(c => wwdWorks.Any(w => w.PartNumber == c.OldPartNumber)).ToList();

            var model = new ResultsModel
                            {
                                ReportTitle = new NameModel(
                                    $"{quantity.ToString()} x {partNumber} from workstation: {workstationCode}")
                            };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = this.SetModelRows(wwdWorks, wwdWorkDetails, parts, changeRequests);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }

        private List<CalculationValueModel> SetModelRows(
            IEnumerable<WwdWork> wwdWorks,
            IEnumerable<WwdWorkDetail> wwdWorkDetails,
            IEnumerable<Part> parts, 
            IEnumerable<ChangeRequest> changeRequests)
        {
            var values = new List<CalculationValueModel>();

            foreach (var wwdWork in wwdWorks)
            {
                var part = parts.FirstOrDefault(p => p.PartNumber == wwdWork.PartNumber);

                var wwdWorkDetail = wwdWorkDetails.FirstOrDefault(
                    w => w.JobId == wwdWork.JobId && w.PartNumber == wwdWork.PartNumber);

                var changeRequest = changeRequests.FirstOrDefault(c => c.OldPartNumber == wwdWork.PartNumber);

                string changeRemarks = string.Empty;

                if (changeRequest != null)
                {
                    changeRemarks = $"{changeRequest.OldPartNumber} change to {changeRequest.NewPartNumber}";
                }

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = wwdWork.PartNumber, ColumnId = "Part Number"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = part.Description, ColumnId = "Description"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, Quantity = wwdWork.QuantityKitted, ColumnId = "Qty Kitted"
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
                            Quantity = wwdWork.QuantityAtLocation,
                            ColumnId = "Qty at Work Station"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = wwdWork.PartNumber, TextDisplay = wwdWork.Remarks, ColumnId = "Remarks"
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
                            RowId = wwdWork.PartNumber, Quantity = wwdWorkDetail?.Quantity ?? 0, ColumnId = "Qty"
                        });
                values.Add(
                    new CalculationValueModel { RowId = wwdWork.PartNumber, TextDisplay = changeRemarks, ColumnId = "Change" });
            }

            return values;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 0, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Description")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Qty Kitted")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.Value
                               },
                           new AxisDetailsModel("Work Station Storage Place")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Qty at Work Station")
                               {
                                   SortOrder = 4, GridDisplayType = GridDisplayType.Value
                               },
                           new AxisDetailsModel("Remarks")
                               {
                                   SortOrder = 5, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Site") { SortOrder = 6, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("State") { SortOrder = 7, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Qty") { SortOrder = 8, GridDisplayType = GridDisplayType.Value },
                           new AxisDetailsModel("Change") { SortOrder = 9, GridDisplayType = GridDisplayType.TextValue }
                       };
        }
    }
}
