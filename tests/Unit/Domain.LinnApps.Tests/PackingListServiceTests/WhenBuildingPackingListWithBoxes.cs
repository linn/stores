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
                                     new PackingListItem(1, 1, "UNIDISK SC/P "),
                                     new PackingListItem(2, 1, "UNIDISK SC/S/P"),
                                     new PackingListItem(3, 1, "UNIDISK SC/S/P"),
                                     new PackingListItem(4, 1, "KATAN BLK"),
                                     new PackingListItem(5, 1, "KATAN STD/1/S"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(6, 10, "KNEKT-RCU/US"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167"),
                                     new PackingListItem(
                                         7,
                                         6,
                                         "MISS 011, 2 KNEKT-RCU/US, 150 PACK 1120, 48 CONN 1164, 48 CONN 1163, 1 KI-CPUFAN/3, 10 1P-080, 1 FAN 016, 2 MCP 167")
                                 };
            var result = this.Sut.BuildPackingList(dataResult);
            result.Count().Should().Be(6);
        }
    }
}
