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
                foreach (var move in line.Moves.OrderBy(m => m.Sequence))
                {
                    moves.Add(new RequisitionMoveSummaryResource
                                  {
                                      ReqNumber = requisition.ReqNumber,
                                      LineNumber = line.LineNumber,
                                      MoveSeq = move.Sequence,
                                      PartNumber = line.PartNumber,
                                      MoveQuantity = move.Quantity
                                  });
                }
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
