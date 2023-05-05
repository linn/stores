namespace Linn.Stores.Facade.Tests.StockTriggerLevelsServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Logging;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingLogMessage : ContextBase
    {
        private readonly IEnumerable<string> privileges = new[] 
                                                              { 
                                                                  AuthorisedAction.CreateStockTriggerLevel,
                                                                  AuthorisedAction.UpdateStockTriggerLevel
                                                              };

        private readonly StockTriggerLevel toDelete = new StockTriggerLevel
        {
            PartNumber = "PART",
            Id = 1,
            KanbanSize = 1,
            LocationId = 1,
            MaxCapacity = 1,
            PalletNumber = 1,
            TriggerLevel = 1
        };

        [SetUp]

        public void SetUp()
        {
            this.Repository.FindBy(Arg.Any<Expression<Func<StockTriggerLevel, bool>>>())
                .Returns(new StockTriggerLevel
                             {
                                 PartNumber = "PART",
                                 Id = 1,
                                 KanbanSize = 1,
                                 LocationId = 1,
                                 MaxCapacity = 1,
                                 PalletNumber = 1,
                                 TriggerLevel = 1
                             });

            this.Sut.DeleteStockTriggerLevel(this.toDelete.Id, 123);
        }

        [Test]
        public void ShouldDeleteStockTriggerLevel()
        {
            this.Repository.Received().Remove(Arg.Is<StockTriggerLevel>(a => a.Id == 1));
        }

        [Test]
        public void ShouldFindStockTriggerLevel()
        {
            this.Repository.Received().FindBy(Arg.Any<Expression<Func<StockTriggerLevel, bool>>>());
        }

        [Test]
        public void ShouldCallLogger()
        {
            this.LoggingService.Received().Debug(Arg.Any<string>());
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }
    }
}
