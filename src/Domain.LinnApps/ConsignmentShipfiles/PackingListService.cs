namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;
    using System.Linq;

    public class PackingListService : IPackingListService
    {
        // groups items into lines based on the IsIdenticalItem() test, and sums up quantities for the groups accordingly
        public IEnumerable<PackingListItem> BuildPackingList(IEnumerable<PackingListItem> dataResult)
        {
            var resultGroups = new List<PackingListItem>();
            var packingListItems = dataResult as PackingListItem[] ?? dataResult.ToArray();
            var qtyOfIdenticalItems = 0m;
            var prevWasIdentical = false;

            var minBox = 1;
            var maxBox = 1;
            
            for (var i = 0; i < packingListItems.Length; i++)
            {
                var current = packingListItems.ElementAt(i);
             
                // the first item we see, so add it to a new group unless it is the <<__ End of Input __> delimiter returned by the query
                if (i == 0 && current.ContentsDescription != "<<__ End of input __>>")
                {
                    resultGroups.Add(packingListItems.First());
                    resultGroups.First().Count = current.Box;
                    resultGroups.First().To = current.Box;
                    qtyOfIdenticalItems += packingListItems.First().Quantity;
                    continue;
                }

                var prev = packingListItems.ElementAt(i - 1);

                // no box information, so add item since it needs to be on its own line witha count of 1
                if (current.Box == null && current.ContentsDescription != "<<__ End of input __>>")
                {
                    var group = new PackingListItem(
                        current.Pallet,
                        current.Box,
                        current.ContentsDescription,
                        current.Quantity)
                                    {
                                        Count = 1
                                    };
                    resultGroups.Add(group);
                    continue;
                }

                // we've come to the end of a group of 'identical' items (or the end of the list), so update the description for the previous group
                if ((prevWasIdentical && !IsIdenticalItem(prev, current)) 
                    || i == packingListItems.Length - 1 
                    || current.ContentsDescription == "<<__ End of input __>>")
                {
                    var group = resultGroups.Last();

                    // descriptions with commas in them are already totalised
                    if (!group.ContentsDescription.Contains(","))
                    {
                        var desc = group.ContentsDescription;

                        // remove the individual item quantity from the string
                        var item = desc.Remove(0, desc.IndexOf(' ') + 1);

                        // and replace it with the counted up total
                        group.ContentsDescription = $"{qtyOfIdenticalItems} {item}";
                    }
                    
                    // the box the group starts on
                    group.Box = minBox;

                    // the box the group ends on
                    group.To = maxBox;

                    // count is the range from max -> min, inclusive
                    group.Count = maxBox - minBox + 1;
                   
                    // reset  counters for the next group
                    qtyOfIdenticalItems = 0;
                    minBox = 0;
                    maxBox = 0;
                }

                // the first time we see a new item that isn't 'identical' to the previous one, hence add it as a new group
                if (!IsIdenticalItem(prev, current) && current.ContentsDescription != "<<__ End of input __>>")
                {
                    minBox = current.Box ?? 0;
                    maxBox = current.Box ?? 0;
                    var group = new PackingListItem(
                        current.Pallet,
                        current.Box,
                        current.ContentsDescription,
                        current.Quantity)
                                    {
                                        Box = current.Box
                                    };
                    
                    group.To = current.Box;
                    group.Count = 1;
                    resultGroups.Add(group);
                    qtyOfIdenticalItems += current.Quantity;
                }
                else
                {
                    // else we've seen an item 'identical' to this before, so just count it up
                    qtyOfIdenticalItems += current.Quantity;
                    
                    if (current.Box != null && current.Box >= maxBox)
                    {
                        maxBox = (int)current.Box;
                    }
                }

                // keep track of whether the previous item was 'identical' to the one before it
                prevWasIdentical = IsIdenticalItem(prev, current);
            }

            return resultGroups;
        }

        private static bool IsIdenticalItem(PackingListItem prev, PackingListItem current)
        {
            return (current.ContentsDescription == prev.ContentsDescription || current.Box == prev.Box)
                   && (current.Pallet == prev.Pallet || current.Pallet == null);
        }
    }
}
