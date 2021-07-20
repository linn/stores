namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingAndDepdendentReqMove : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] { AuthorisedAction.CreateStockLocator };

        private readonly StockLocator toDelete = new StockLocator
                                                     {
                                                         Id = 1,
                                                         PalletNumber = 1
                                                     };

        private readonly IEnumerable<StoresPallet>
            pallets = new List<StoresPallet>
                          {
                              new StoresPallet
                                  {
                                      PalletNumber = 1,
                                      AuditedByDepartmentCode = "CODE",
                                      AuditFrequencyWeeks = 26
                                  }
                          };

        [SetUp]
        public void SetUp()
        {
            this.AuthService
                .HasPermissionFor(AuthorisedAction.CreateStockLocator, this.privileges)
                .Returns(true);
            this.ReqMoveRepository.FindBy(Arg.Any<Expression<Func<ReqMove, bool>>>()).Returns(new ReqMove
                {
                    StockLocatorId = 1
                });
            this.StoresPalletRepository.FilterBy(Arg.Any<Expression<Func<StoresPallet, bool>>>())
                .Returns(this.pallets.AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<StockLocatorException>(()
                => this.Sut.DeleteStockLocator(this.toDelete, this.privileges));
            ex.Message.Should().Be("Cannot Delete Stock Locators When Dependent Req Move exists");
        }
    }
}
