namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingBatchesInRotationOrder : ContextBase
    {
        private readonly IEnumerable<StockLocator> correctResult = new List<StockLocator>
                                                                       {
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 955521,
                                                                                   BatchRef = "Q0175039",
                                                                                   PartNumber = "RES 213",
                                                                                   Quantity = 3476,
                                                                                   StockRotationDate = new DateTime(2002, 02, 10),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 955522,
                                                                                   BatchRef = "Q0189770",
                                                                                   PartNumber = "RES 213",
                                                                                   Quantity = 145,
                                                                                   StockRotationDate = new DateTime(2002, 02, 10),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 17645,
                                                                                   BatchRef = "Q0175039",
                                                                                   PartNumber = "RES 213",
                                                                                   Quantity = 4750,
                                                                                   StockRotationDate = new DateTime(2002, 10, 04),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 12647
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 955443,
                                                                                   BatchRef = "P696283",
                                                                                   PartNumber = "RES 218",
                                                                                   Quantity = 3092,
                                                                                   StockRotationDate = new DateTime(2005, 12, 29),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 955533,
                                                                                   BatchRef = "P718067",
                                                                                   PartNumber = "RES 211",
                                                                                   Quantity = 55,
                                                                                   StockRotationDate = new DateTime(2006, 08, 31),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 1110481,
                                                                                   BatchRef = "P718067",
                                                                                   PartNumber = "RES 211",
                                                                                   Quantity = 2413,
                                                                                   StockRotationDate = new DateTime(2006, 08, 31),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 960321,
                                                                                   BatchRef = "P721933",
                                                                                   PartNumber = "RES 219",
                                                                                   Quantity = 158,
                                                                                   StockRotationDate = new DateTime(2006, 10, 06),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 955942,
                                                                                   BatchRef = "P721933",
                                                                                   PartNumber = "RES 219",
                                                                                   Quantity = 28,
                                                                                   StockRotationDate = new DateTime(2006, 10, 06, 10, 33, 45),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 961791,
                                                                                   BatchRef = "P725164",
                                                                                   PartNumber = "RES 212",
                                                                                   Quantity = 791,
                                                                                   StockRotationDate = new DateTime(2006, 11, 06),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 1048800,
                                                                                   BatchRef = "P732846",
                                                                                   PartNumber = "RES 214",
                                                                                   Quantity = 56,
                                                                                   StockRotationDate = new DateTime(2007, 02, 23),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 12427
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 1190263,
                                                                                   BatchRef = "P743956",
                                                                                   PartNumber = "RES 210",
                                                                                   Quantity = 5000,
                                                                                   StockRotationDate = new DateTime(2007, 08, 08),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "STORES",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 12690
                                                                               },
                                                                           new StockLocator
                                                                               {
                                                                                   Id = 1196341,
                                                                                   BatchRef = "P743956",
                                                                                   PartNumber = "RES 210",
                                                                                   Quantity = 4093,
                                                                                   StockRotationDate = new DateTime(2007, 08, 08),
                                                                                   StockPoolCode = "LINN",
                                                                                   State = "US LOAN",
                                                                                   PalletNumber = null,
                                                                                   LocationId = 14548
                                                                               }
                                                                       };

        private IEnumerable<StockLocator> result;

        [SetUp]
        public void SetUp()
        {
            var stockLocators = new List<StockLocator>
                                    {
                                        new StockLocator
                                            {
                                                Id = 1190263,
                                                BatchRef = "P743956",
                                                PartNumber = "RES 210",
                                                Quantity = 5000,
                                                StockRotationDate = new DateTime(2007, 08, 08),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 12690,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 12690
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 1190262,
                                                BatchRef = "P743956",
                                                PartNumber = "RES 210",
                                                Quantity = 0,
                                                StockRotationDate = new DateTime(2007, 08, 08),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 13653,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 13653
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 1196341,
                                                BatchRef = "P743956",
                                                PartNumber = "RES 210",
                                                Quantity = 4093,
                                                StockRotationDate = new DateTime(2007, 08, 08),
                                                StockPoolCode = "LINN",
                                                State = "US LOAN",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 955533,
                                                BatchRef = "P718067",
                                                PartNumber = "RES 211",
                                                Quantity = 55,
                                                StockRotationDate = new DateTime(2006, 08, 31),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }

                                            },
                                        new StockLocator
                                            {
                                                Id = 1110481,
                                                BatchRef = "P718067",
                                                PartNumber = "RES 211",
                                                Quantity = 2413,
                                                StockRotationDate = new DateTime(2006, 08, 31),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 961791,
                                                BatchRef = "P725164",
                                                PartNumber = "RES 212",
                                                Quantity = 791,
                                                StockRotationDate = new DateTime(2006, 11, 06),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 17645,
                                                BatchRef = "Q0175039",
                                                PartNumber = "RES 213",
                                                Quantity = 4750,
                                                StockRotationDate = new DateTime(2002, 10, 04),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 12647,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 12647
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 955521,
                                                BatchRef = "Q0175039",
                                                PartNumber = "RES 213",
                                                Quantity = 3476,
                                                StockRotationDate = new DateTime(2002, 02, 10),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 955522,
                                                BatchRef = "Q0189770",
                                                PartNumber = "RES 213",
                                                Quantity = 145,
                                                StockRotationDate = new DateTime(2002, 02, 10),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 1190558,
                                                BatchRef = "P732846",
                                                PartNumber = "RES 214",
                                                Quantity = 0,
                                                StockRotationDate = new DateTime(2007, 02, 23),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 15565,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 15565
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 1048800,
                                                BatchRef = "P732846",
                                                PartNumber = "RES 214",
                                                Quantity = 56,
                                                StockRotationDate = new DateTime(2007, 02, 23),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 12427,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 12427
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 955443,
                                                BatchRef = "P696283",
                                                PartNumber = "RES 218",
                                                Quantity = 3092,
                                                StockRotationDate = new DateTime(2005, 12, 29),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 955942,
                                                BatchRef = "P721933",
                                                PartNumber = "RES 219",
                                                Quantity = 28,
                                                StockRotationDate = new DateTime(2006, 10, 06, 10, 33, 45),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            },
                                        new StockLocator
                                            {
                                                Id = 960321,
                                                BatchRef = "P721933",
                                                PartNumber = "RES 219",
                                                Quantity = 158,
                                                StockRotationDate = new DateTime(2006, 10, 06, 0, 0, 0),
                                                StockPoolCode = "LINN",
                                                State = "STORES",
                                                PalletNumber = null,
                                                LocationId = 14548,
                                                StorageLocation = new StorageLocation
                                                                      {
                                                                          LocationId = 14548
                                                                      }
                                            }
                                    };

            this.StockLocatorRepository.FilterByWildcard("RES 21%").Returns(stockLocators.AsQueryable());

            this.result = this.Sut.GetBatchesInRotationOrderByPart("RES 21*");
        }

        [Test]
        public void ShouldReturnCorrectResult()
        {
            for (var i = 0; i < this.correctResult.Count(); i++)
            {
                var expectedEntry = this.correctResult.ElementAt(i); 
                var resultEntry = this.result.ElementAt(i);
                expectedEntry.Id.Should().Be(resultEntry.Id);
                expectedEntry.BatchRef.Should().Be(resultEntry.BatchRef);
                expectedEntry.PartNumber.Should().Be(resultEntry.PartNumber);
                expectedEntry.Quantity.Should().Be(resultEntry.Quantity);
                expectedEntry.StockRotationDate.Should().Be(resultEntry.StockRotationDate);
                expectedEntry.StockPoolCode.Should().Be(resultEntry.StockPoolCode);
                expectedEntry.State.Should().Be(resultEntry.State);
                expectedEntry.PalletNumber.Should().Be(resultEntry.PalletNumber);
                expectedEntry.LocationId.Should().Be(resultEntry.StorageLocation.LocationId);
            }
        }
    }
}
