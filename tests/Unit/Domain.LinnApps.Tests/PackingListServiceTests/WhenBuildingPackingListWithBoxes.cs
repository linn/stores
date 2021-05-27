namespace Linn.Stores.Domain.LinnApps.Tests.PackingListServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    using NUnit.Framework;

    public class WhenBuildingPackingListWithBoxes : ContextBase
    {
        [Test]
        public void ExampleOne()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(null, 1, "UNIDISK SC/P "),
                                     new PackingListItem(null, 2, "UNIDISK SC/S/P"),
                                     new PackingListItem(null, 3, "UNIDISK SC/S/P"),
                                     new PackingListItem(null, 4, "KATAN BLK"),
                                     new PackingListItem(null, 5, "KATAN STD/1/S"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(null, 6, "KNEKT-RCU/US"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         null,
                                         7,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167")
                                 };
            var result = this.Sut.BuildPackingList(dataResult);
            result.Count().Should().Be(6);
        }

        [Test]
        public void Example2()
        {
            var dataResult = new List<PackingListItem>
                                 {
                                     new PackingListItem(null, 1, "9 STAND 110/S"),
                                     new PackingListItem(null, 2, "1 STAND 110/S"),
                                     new PackingListItem(null, 3, "9 STAND 110/S"),
                                     new PackingListItem(null, 4, "9 STAND 110/S"),
                                     new PackingListItem(null, null, "BPLINTH"),
                                     new PackingListItem(null, null, "W/S PLATING/P"),
                                     new PackingListItem(null, null, "TO-PLATE")
                                 };
        
            this.Sut.BuildPackingList(dataResult).Count().Should().Be(6);
        }
    }
}
