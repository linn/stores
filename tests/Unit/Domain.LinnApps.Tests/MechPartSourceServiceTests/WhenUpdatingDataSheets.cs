namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingDataSheets : ContextBase
    {
        private MechPartSource from;

        private MechPartSource to;

       [SetUp]
        public void SetUp()
        {
            this.from = new MechPartSource
                            {
                                Part = new Part
                                           {
                                               DataSheets = new List<PartDataSheet>
                                                                {
                                                                    new PartDataSheet
                                                                        {
                                                                            Sequence = 1,
                                                                            PartNumber = "PART",
                                                                            PdfFilePath = "/path"
                                                                        }
                                                                }
                                           }
                            };
            this.to = new MechPartSource
                            {
                                Part = new Part
                                           {
                                               DataSheets = new List<PartDataSheet>
                                                                {
                                                                    new PartDataSheet
                                                                        {
                                                                            PartNumber = "PART",
                                                                            PdfFilePath = "/new-path-1"
                                                                        },
                                                                    new PartDataSheet
                                                                        {
                                                                            PartNumber = "PART",
                                                                            PdfFilePath = "/path-2"
                                                                        },
                                                                    new PartDataSheet
                                                                        {
                                                                            PartNumber = "PART",
                                                                            PdfFilePath = "/path-3"
                                                                        }
                                                                }
                                            }
                            };
            this.AuthorisationService.HasPermissionFor(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(true);
            this.Sut.Update(this.to, this.from, new List<string>());
        }

        [Test]
        public void ShouldUpdate()
        {
            this.from.Part.DataSheets.Count().Should().Be(3);
        }
    }
}
