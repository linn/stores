namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Resources.Requisitions;

    public class RequisitionMovesResourceBuilder : IRequisitionMovesResourceBuilder
    {
        public IEnumerable<RequisitionMoveSummaryResource> Build(RequisitionHeader requisition)
        {
            var moves = new List<RequisitionMoveSummaryResource>();

            foreach (var line in requisition.Lines.OrderBy(l => l.LineNumber))
            {
                moves.AddRange(
                    line.Moves.Where(b => b.Booked == "Y").OrderBy(m => m.Sequence)
                    .Select(
                        move => new RequisitionMoveSummaryResource
                                    {
                                        ReqNumber = requisition.ReqNumber,
                                        LineNumber = line.LineNumber,
                                        MoveSeq = move.Sequence,
                                        PartNumber = line.PartNumber,
                                        MoveQuantity = move.Quantity,
                                        FromLocationCode = move.StockLocator.StorageLocation?.LocationCode,
                                        FromPalletNumber = move.StockLocator.PalletNumber,
                                        ToLocationCode = move.Location?.LocationCode,
                                        ToPalletNumber = move.PalletNumber,
                                        Remarks = move.Remarks
                                    }));
            }

            return moves;
        }

        public string GetLocation(RequisitionHeader requisitionHeader)
        {
            return $"/logistics/requisitions/{requisitionHeader.ReqNumber}";
        }

        object IResourceBuilder<RequisitionHeader>.Build(RequisitionHeader requisition) => this.Build(requisition);
    }
}
