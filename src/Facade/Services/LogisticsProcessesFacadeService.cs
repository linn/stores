namespace Linn.Stores.Facade.Services
{
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public class LogisticsProcessesFacadeService : ILogisticsProcessesFacadeService
    {
        private readonly ILogisticsLabelService logisticsLabelService;

        private readonly IConsignmentService consignmentService;

        public LogisticsProcessesFacadeService(
            ILogisticsLabelService logisticsLabelService,
            IConsignmentService consignmentService)
        {
            this.logisticsLabelService = logisticsLabelService;
            this.consignmentService = consignmentService;
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
                            resource.ConsignmentId.Value,
                            resource.FirstItem.Value,
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
                            resource.ConsignmentId.Value,
                            resource.FirstItem.Value,
                            resource.LastItem,
                            resource.UserNumber,
                            resource.NumberOfCopies);
                    }
                    catch (ProcessException exception)
                    {
                        return new BadRequestResult<ProcessResult>(exception.Message);
                    }

                    break;

                case "Address":
                    try
                    {
                        if (resource.AddressId == null)
                        {
                            return new BadRequestResult<ProcessResult>("Address Id not supplied");
                        }

                        labelServiceResult = this.logisticsLabelService.PrintAddressLabel(
                            resource.AddressId.Value, 
                            resource.Line1,
                            resource.Line2,
                            resource.UserNumber,
                            resource.NumberOfCopies);
                    }
                    catch (ProcessException exception)
                    {
                        return new BadRequestResult<ProcessResult>(exception.Message);
                    }

                    break;

                case "General":
                    try
                    {
                        labelServiceResult = this.logisticsLabelService.PrintGeneralLabel(
                            resource.Line1,
                            resource.Line2,
                            resource.Line3,
                            resource.Line4,
                            resource.Line5,
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

            return new SuccessResult<ProcessResult>(labelServiceResult);
        }

        public async Task<IResult<ProcessResult>> PrintConsignmentDocuments(ConsignmentRequestResource resource)
        {
            ProcessResult result;

            try
            {
                result = await this.consignmentService.PrintConsignmentDocuments(resource.ConsignmentId, resource.UserNumber);
            }
            catch (ProcessException exception)
            {
                return new BadRequestResult<ProcessResult>(exception.Message);
            }

            return new SuccessResult<ProcessResult>(result);
        }

        public IResult<ProcessResult> SaveConsignmentDocuments(ConsignmentRequestResource resource)
        {
            ProcessResult result;

            try
            {
                result = this.consignmentService.SaveConsignmentDocuments(resource.ConsignmentId);
            }
            catch (ProcessException exception)
            {
                return new BadRequestResult<ProcessResult>(exception.Message);
            }

            return new SuccessResult<ProcessResult>(result);
        }
    }
}
