namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Resources.Requisitions;

    public class RequisitionResourceBuilder : IResourceBuilder<RequisitionHeader>
    {
        public RequisitionResource Build(RequisitionHeader requisition)
        {
            return new RequisitionResource
            {
                ReqNumber = requisition.ReqNumber,
                Document1 = requisition.Document1,
                PartNumber = requisition.PartNumber,
                UnitOfMeasure = requisition.Part?.OurUnitOfMeasure,
                QcInfo = requisition.Part?.QcInformation,
                DocumentType = requisition.Document1Name,
                QcState = requisition.Part?.QcOnReceipt == "Y" ? "QUARANTINE" : "PASS",
                PartDescription = requisition.Part?.Description,
                QtyReceived = requisition.Qty,
                StorageType = requisition.ToLocation?.StorageType,
                Lines = requisition.Lines.Select(l => new RequisitionLineResource
                                                          {
                                                              TransactionCode = l.TransactionCode,
                                                              Line = l.LineNumber,
                                                          }),
                Links = this.BuildLinks(requisition).ToArray()
            };
        }

        public string GetLocation(RequisitionHeader requisitionHeader)
        {
            return $"/logistics/requisitions/{requisitionHeader.ReqNumber}";
        }

        object IResourceBuilder<RequisitionHeader>.Build(RequisitionHeader requisition) => this.Build(requisition);

        private IEnumerable<LinkResource> BuildLinks(RequisitionHeader requisition)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(requisition) };
        }
    }
}
