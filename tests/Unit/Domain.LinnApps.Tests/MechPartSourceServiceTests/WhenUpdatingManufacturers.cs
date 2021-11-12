namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingManufacturers : ContextBase
    {
        private MechPartSource from;

        private MechPartSource to;

        [SetUp]
        public void SetUp()
        {
            this.from = new MechPartSource
            {
                 MechPartManufacturerAlts = new List<MechPartManufacturerAlt>
                                                                {
                                                                    new MechPartManufacturerAlt
                                                                        {
                                                                            Sequence = 1,
                                                                            PartNumber = "PART",
                                                                        }
                                                                }
            };

            this.to = new MechPartSource
            {
                MechPartManufacturerAlts = new List<MechPartManufacturerAlt>
                                                                {
                                                                    new MechPartManufacturerAlt
                                                                        {
                                                                            Sequence = 1,
                                                                            PartNumber = "PART",
                                                                        },
                                                                    new MechPartManufacturerAlt
                                                                        {
                                                                            PartNumber = "PART",
                                                                        },
                                                                    new MechPartManufacturerAlt
                                                                        {
                                                                            PartNumber = "PART",
                                                                        }
                                                                }
            };
            this.AuthorisationService.HasPermissionFor(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(true);
            this.Sut.Update(this.to, this.from, new List<string>());
        }

        [Test]
        public void ShouldUpdate()
        {
            this.from.MechPartManufacturerAlts.Count().Should().Be(3);
        }
    }
}
