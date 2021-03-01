namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;
    using Linn.Stores.Resources.Requisitions;

    public class RequisitionActionResourceBuilder : IResourceBuilder<RequisitionActionResult>
    {
        public RequisitionActionResource Build(RequisitionActionResult requisitionActionResult)
        {
            return new RequisitionActionResource
            {
                Message = requisitionActionResult.Message,
                Success = requisitionActionResult.Success,
                RequisitionHeader = new RequisitionHeaderResource
                                        {
                                            ReqNumber = requisitionActionResult.RequisitionHeader.ReqNumber,
                                            Document1 = requisitionActionResult.RequisitionHeader.Document1
                                        }
            };
        }

        public string GetLocation(RequisitionActionResult requisitionActionResult)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<RequisitionActionResult>.Build(RequisitionActionResult requisitionActionResult) =>
            this.Build(requisitionActionResult);
    }
}
