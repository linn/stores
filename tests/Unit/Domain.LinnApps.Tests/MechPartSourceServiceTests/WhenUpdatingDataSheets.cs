namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenUpdatingDataSheets : ContextBase
    {
        private IEnumerable<PartDataSheet> from;

        private IEnumerable<PartDataSheet> to;

        private IEnumerable<PartDataSheet> result;

       [SetUp]
        public void SetUp()
        {
            this.from = new List<PartDataSheet>
                             {
                                 new PartDataSheet
                                     {
                                         Sequence = 1,
                                         PartNumber = "PART",
                                         PdfFilePath = "/path"
                                     }
                             };

            this.to = new List<PartDataSheet>
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
                            };
            this.result = this.Sut.GetUpdatedDataSheets(this.from, this.to);
        }

        [Test]
        public void ShouldReturnUpdated()
        {
            this.result.First().PdfFilePath.Should().Be("/new-path-1");
            this.result.Last().Sequence.Should().Be(3);
        }
    }
}
