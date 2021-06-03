namespace Linn.Stores.Domain.LinnApps.Tests.PackingListServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    using NUnit.Framework;

    public class WhenBuildingPackingList : ContextBase
    {
        [Test]
        public void WhenOneConsignmentItem()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(1, 1, "350/E/U/2", 1m),
                                     new PackingListItem(999999, 999999, "<<__ End of input __>>", 0m)
                                 };
            var result = this.Sut.BuildPackingList(dataResult).ToList();

            result.Count.Should().Be(1);
            result.First().ContentsDescription.Should().Be("1 350/E/U/2");
        }

        [Test]
        public void WhenMultipleItemsAndSomeHaveNoBoxInformation()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(null, 1, "9 STAND 110/S", 9),
                                     new PackingListItem(null, 2, "1 STAND 110/S", 1),
                                     new PackingListItem(null, 3, "9 STAND 110/S", 9),
                                     new PackingListItem(null, 4, "9 STAND 110/S", 9),
                                     new PackingListItem(null, null, "<<__ End of input __>>", 0),
                                     new PackingListItem(null, null, "BPLINTH", 1),
                                     new PackingListItem(null, null, "W/S PLATING/P", 2),
                                     new PackingListItem(null, null, "TO-PLATE", 1)
                                 };
            var result = this.Sut.BuildPackingList(dataResult).ToList();

            result.Count.Should().Be(6);

            result.ElementAt(0).Box.Should().Be(1);
            result.ElementAt(0).To.Should().Be(1);
            result.ElementAt(0).Count.Should().Be(1);

            result.ElementAt(1).Box.Should().Be(2);
            result.ElementAt(1).Count.Should().Be(1);
            result.ElementAt(1).To.Should().Be(2);

            result.ElementAt(2).Box.Should().Be(3);
            result.ElementAt(2).To.Should().Be(4);
            result.ElementAt(2).Count.Should().Be(2);

            result.ElementAt(3).Count.Should().Be(1);
            result.ElementAt(4).Count.Should().Be(1);
            result.ElementAt(5).Count.Should().Be(1);
        }

        [Test]
        public void WhenMultipleItemsAndNonIntegerTotals()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(1, 1, "1 350B/K UPG2", 1m),
                                     new PackingListItem(1, 2, "1 350B/K UPG2", 1m),
                                     new PackingListItem(1, 3, "1 350B/K UPG2", 1m),
                                     new PackingListItem(1, 4, ".5 350/BLK/E/2", 0.5m),
                                     new PackingListItem(1, 5, ".5 350/BLK/E/2", 0.5m),
                                     new PackingListItem(1, 6, ".5 350/BLK/E/2", 0.5m),
                                     new PackingListItem(999999, 999999, "<<__ End of input __>>", 0)
                                 };
            var result = this.Sut.BuildPackingList(dataResult);
            var packingListItems = result as PackingListItem[] ?? result.ToArray();
            packingListItems.Length.Should().Be(2);
            packingListItems.First().ContentsDescription.Should().Be("3 350B/K UPG2");
            packingListItems.First().Box.Should().Be(1);
            packingListItems.First().To.Should().Be(3);
            packingListItems.First().Count.Should().Be(3);

            packingListItems.Last().ContentsDescription.Should().Be("1.5 350/BLK/E/2");
            packingListItems.Last().Box.Should().Be(4);
            packingListItems.Last().To.Should().Be(6);
            packingListItems.Last().Count.Should().Be(3);
        }

        [Test]
        public void WhenCommaInDescription()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 1),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 8),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 8),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 1),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 24),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 8),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 24),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 10),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 8),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 10),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 1),
                                     new PackingListItem(null, 1, "10 MISS 293, 8 MECH 845", 1),
                                     new PackingListItem(999999, 999999, "<<__ End of input __>>", 0)
                                 };

            var result = this.Sut.BuildPackingList(dataResult).ToList();

            result.Count.Should().Be(1);
            result.First().Box.Should().Be(1);
            result.First().To.Should().Be(1);
            result.First().Count.Should().Be(1);
            result.First().ContentsDescription.Should().Be("10 MISS 293, 8 MECH 845");
        }

        [Test]
        public void WhenMultipleItemsAndSomeHaveCommaInDescription()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(null, 1, "1 K10/DRUM/1", 1),
                                     new PackingListItem(null, 2, "1 CONN 899/1", 1),
                                     new PackingListItem(null, 3, "2 LP12/UPG/1, 1 T-KABLE/5", 1),
                                     new PackingListItem(null, 3, "2 LP12/UPG/1, 1 T-KABLE/5", 1),
                                     new PackingListItem(null, 3, "2 LP12/UPG/1, 1 T-KABLE/5", 1),
                                     new PackingListItem(null, 3, "2 LP12/UPG/1, 1 T-KABLE/5", 1)
                                 };

            var result = this.Sut.BuildPackingList(dataResult).ToList();
            result.Count.Should().Be(3);
            result.ElementAt(0).ContentsDescription.Should().Be("1 K10/DRUM/1");
            result.ElementAt(0).Box.Should().Be(1);
            result.ElementAt(0).Count.Should().Be(1);
            result.ElementAt(0).To.Should().Be(1);

            result.ElementAt(1).ContentsDescription.Should().Be("1 CONN 899/1");
            result.ElementAt(1).Box.Should().Be(2);
            result.ElementAt(1).Count.Should().Be(1);
            result.ElementAt(1).To.Should().Be(2);

            result.ElementAt(2).ContentsDescription.Should().Be("2 LP12/UPG/1, 1 T-KABLE/5");
            result.ElementAt(2).Box.Should().Be(3);
            result.ElementAt(2).Count.Should().Be(1);
            result.ElementAt(2).To.Should().Be(3);
        }

        [Test]
        public void WhenLotsOfSameItem()
        {
            var dataResult = new List<PackingListItem>
                                 {                                 
                                     new PackingListItem(1,   1,   "1 SK ARBB",   1),
                                     new PackingListItem(1,   2,   "1 SK ALBB",   1),
                                     new PackingListItem(1,   3,   "1 SK ALBB",   1),
                                     new PackingListItem(1,   4,   "1 SK ARBB",   1),
                                     new PackingListItem(1,   5,   "1 SK ALBB",   1),
                                     new PackingListItem(1,   6,   "1 SK ARBB",   1),
                                     new PackingListItem(1,   7,   "1 SK ALBB",   1),
                                     new PackingListItem(1,   8,   "1 SK ALBB",   1),
                                     new PackingListItem(999999,  999999,  "<<__ End of input __>>",  0)
                                 };

            var result = this.Sut.BuildPackingList(dataResult).ToList();
            result.Count.Should().Be(6);

            result.All(x => x.Pallet == 1).Should().BeTrue();

            result.ElementAt(0).Box.Should().Be(1);
            result.ElementAt(0).To.Should().Be(1);
            result.ElementAt(0).Count.Should().Be(1);
            result.ElementAt(0).ContentsDescription.Should().Be("1 SK ARBB");

            result.ElementAt(1).Box.Should().Be(2);
            result.ElementAt(1).To.Should().Be(3);
            result.ElementAt(1).Count.Should().Be(2);
            result.ElementAt(1).ContentsDescription.Should().Be("2 SK ALBB");

            result.ElementAt(2).Box.Should().Be(4);
            result.ElementAt(2).To.Should().Be(4);
            result.ElementAt(2).Count.Should().Be(1);
            result.ElementAt(2).ContentsDescription.Should().Be("1 SK ARBB");

            result.ElementAt(3).Box.Should().Be(5);
            result.ElementAt(3).To.Should().Be(5);
            result.ElementAt(3).Count.Should().Be(1);
            result.ElementAt(3).ContentsDescription.Should().Be("1 SK ALBB");

            result.ElementAt(4).Box.Should().Be(6);
            result.ElementAt(4).To.Should().Be(6);
            result.ElementAt(4).Count.Should().Be(1);
            result.ElementAt(4).ContentsDescription.Should().Be("1 SK ARBB");

            result.ElementAt(5).Box.Should().Be(7);
            result.ElementAt(5).To.Should().Be(8);
            result.ElementAt(5).Count.Should().Be(2);
            result.ElementAt(5).ContentsDescription.Should().Be("2 SK ALBB");
        }

        // [Test]
        // public void WhenTrailingNullLine()
        // {
        //     var dataResult = new List<PackingListItem>
        //                          {
        //                              	new PackingListItem(null, 1,	"1 KLI DSM/H/3/B",	1),
	       //                              new PackingListItem(null, 2,	"1 TRAMPOLIN/2",	1),
	       //                              new PackingListItem(null, 3,	"1 KRYSTAL/B, 1 TRAMPOLIN/2, 1 KLI HDMI UPG, 1 350/E/U/2",	1),
	       //                              new PackingListItem(null, 3,	"1 KRYSTAL/B, 1 TRAMPOLIN/2, 1 KLI HDMI UPG, 1 350/E/U/2",	1),
	       //                              new PackingListItem(null, 3,	"1 KRYSTAL/B, 1 TRAMPOLIN/2, 1 KLI HDMI UPG, 1 350/E/U/2",	1),
	       //                              new PackingListItem(null, 3,	"1 KRYSTAL/B, 1 TRAMPOLIN/2, 1 KLI HDMI UPG, 1 350/E/U/2",	1),
	       //                              new PackingListItem(null, 3,	"1 KRYSTAL/B, 1 TRAMPOLIN/2, 1 KLI HDMI UPG, 1 350/E/U/2",	1),
	       //                              new PackingListItem(null, 4,	"1 TRAMPOLIN/2"	,1),
	       //                              new PackingListItem(null, 5,	"1 AK DS/UPG/3",	1),
        //                                 new PackingListItem(999999,	999999,	"<<__ End of input __>>",	0),
        //                                 new PackingListItem(null, null, string.Empty, null)
        //                          };
        // }
    }
}
