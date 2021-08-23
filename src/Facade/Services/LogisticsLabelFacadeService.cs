﻿namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public class LogisticsLabelFacadeService : ILogisticsLabelFacadeService
    {
        private readonly ILogisticsLabelService logisticsLabelService;

        public LogisticsLabelFacadeService(ILogisticsLabelService logisticsLabelService)
        {
            this.logisticsLabelService = logisticsLabelService;
        }

        public IResult<ProcessResult> PrintLabel(LogisticsLabelRequestResource resource)
        {
            ProcessResult labelServiceResult;

            switch (resource.LabelType)
            {
                case "Carton":
                    try
                    {
                        labelServiceResult = this.logisticsLabelService.PrintCartonLabel(
                            resource.ConsignmentId,
                            resource.FirstItem,
                            resource.LastItem,
                            resource.UserNumber,
                            resource.NumberOfCopies);
                    }
                    catch (ProcessException exception)
                    {
                        return new BadRequestResult<ProcessResult>(exception.Message);
                    }

                    break;
                case "Pallet":
                    try
                    {
                        labelServiceResult = this.logisticsLabelService.PrintPalletLabel(
                            resource.ConsignmentId,
                            resource.FirstItem,
                            resource.LastItem,
                            resource.UserNumber,
                            resource.NumberOfCopies);
                    }
                    catch (ProcessException exception)
                    {
                        return new BadRequestResult<ProcessResult>(exception.Message);
                    }

                    break;
                default:
                    return new BadRequestResult<ProcessResult>($"Cannot print label type {resource.LabelType}");
            }

            return new SuccessResult<ProcessResult>(new ProcessResult(labelServiceResult.Success, labelServiceResult.Message));
        }
    }
}