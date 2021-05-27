namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;
    using System.Linq;

    public class PackingListService : IPackingListService
    {
        public IEnumerable<PackingListItem> BuildPackingList(IEnumerable<PackingListItem> dataResult)
        {
            var result = new List<PackingListItem>();

            var packingListItems = dataResult as PackingListItem[] ?? dataResult.ToArray();

            var qtyOfIdenticalItems = 0;

            var prevWasIdentical = false;
            
            for (var i = 0; i < packingListItems.Length; i++)
            {
                var current = packingListItems.ElementAt(i);

                if (current.Box == null && current.Pallet == null)
                {
                    result.Add(current);
                    continue;
                }

                if (i == 0)
                {
                    result.Add(packingListItems.First());
                    continue;
                }

                var prev = packingListItems.ElementAt(i - 1);
                if (this.IsIdenticalItem(prev, current))
                {
                    qtyOfIdenticalItems += current.Quantity;
                }
                
                if ((prevWasIdentical && !this.IsIdenticalItem(prev, current)) || i == packingListItems.Length - 1)
                {
                    // did you have the same box, or the same description?
                    result.Last().ContentsDescription = "I need to be updated if i don't contain a ,";
                }

                if (!this.IsIdenticalItem(prev, current))
                {
                    result.Add(current);
                }

                prevWasIdentical = this.IsIdenticalItem(prev, current);
            }

            return result;
        }

        private bool IsIdenticalItem(PackingListItem prev, PackingListItem current)
        {
            return (current.ContentsDescription == prev.ContentsDescription || current.Box == prev.Box)
                   && (current.Pallet == prev.Pallet || current.Pallet == null);
        }
    }
}
