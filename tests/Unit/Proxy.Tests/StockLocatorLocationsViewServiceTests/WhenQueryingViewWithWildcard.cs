namespace Linn.Stores.Proxy.Tests.StockLocatorLocationsViewServiceTests
{
    using System.Collections.Generic;
    using System.Data;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenQueryingViewWithWildcard : ContextBase
    {
        private readonly string correctQuery = "SELECT * FROM STOCK_LOCATOR_PARTS_VIEW "
                                               + "WHERE PART_NUMBER LIKE 'PART%' "
                                               + "AND DESCRIPTION LIKE 'DESC%' "
                                               + "AND LOCATION_CODE LIKE 'LOC%' "
                                               + "AND LOCATION_ID = 1 AND PALLET_NUMBER = 1 "
                                               + "AND STOCK_POOL_CODE = 'POOL' AND STATE = 'STATE' "
                                               + "AND CATEGORY = 'CAT'";

        [SetUp]
        public void SetUp()
        {
            var dataset = new DataSet();
            dataset.Tables.Add(new DataTable());

            this.DatabaseService.ExecuteQuery(Arg.Any<string>()).Returns(dataset);
            this.Sut.QueryView("PART*", 1, 1, "POOL", "STATE", "CAT", "LOC*", "DESC*");
        }

        [Test]
        public void ShouldBuildCorrectQueryString()
        {
            this.DatabaseService.Received().ExecuteQuery(this.correctQuery);
        }
    }
}
