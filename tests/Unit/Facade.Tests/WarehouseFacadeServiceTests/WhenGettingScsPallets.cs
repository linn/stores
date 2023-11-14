namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Linn.Common.Facade;
    using Linn.Stores.Domain;
    using Linn.Stores.Domain.LinnApps.Wcs;

    using NSubstitute;
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.Models;

    public class WhenGettingScsPallets : ContextBase
    {
        private IResult<IEnumerable<ScsPallet>> result;

        [SetUp]
        public void SetUp()
        {
            var locations = new List<WarehouseLocation>()
                                {
                                    new WarehouseLocation
                                        {
                                            Location = "SA00304R",
                                            PalletId = 1,
                                            Pallet = new WarehousePallet
                                                         {
                                                             PalletId = 1,
                                                             Heat = 2,
                                                             RotationAverage = 2,
                                                             SizeCode = "M"
                                                         },
                                            AreaCode = "S",
                                            Aisle = "A",
                                            XCoord = 3,
                                            YCoord = 4
                                        }
                                };

            this.WarehouseService.GetWarehouseLocationsWithPallets().Returns(locations);

            this.result = this.Sut.GetScsPallets();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType< SuccessResult<IEnumerable<ScsPallet>>>();
            var pallets = ((SuccessResult<IEnumerable<ScsPallet>>)this.result).Data;
            pallets.Should().NotBeNull();
        }

        [Test]
        public void ShouldReturnRightNumberOfPallets()
        {
            var pallets = ((SuccessResult<IEnumerable<ScsPallet>>)this.result).Data;
            pallets.Count().Should().Be(1);
        }

        [Test]
        public void ShouldReturnPallet1InAisle()
        {
            var pallets = ((SuccessResult<IEnumerable<ScsPallet>>)this.result).Data;
            var pallet = pallets.SingleOrDefault(p => p.PalletNumber == 1);
            pallet.Should().NotBeNull();
            pallet.Area.Should().Be(2);
            pallet.Column.Should().Be(3);
            pallet.Level.Should().Be(4);
            pallet.Side.Should().Be(1);
        }
    }
}
