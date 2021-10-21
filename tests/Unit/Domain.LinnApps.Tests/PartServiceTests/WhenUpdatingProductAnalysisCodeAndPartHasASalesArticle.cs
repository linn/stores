namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenUpdatingProductAnalysisCodeAndPartHasASalesArticle : ContextBase
    {
        private Part from;

        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part
                            {
                                PartNumber = "BOX 066", 
                                ProductAnalysisCode = new ProductAnalysisCode
                                                          {
                                                              ProductCode = "OLD"
                                                          },
                                SalesArticle = new SalesArticle
                                                {
                                                    ArticleNumber = "BOX 066 "
                                                }
                            };
            this.to = new Part
                          {
                              PartNumber = "BOX 066", 
                              ProductAnalysisCode = new ProductAnalysisCode
                                                        {
                                                            ProductCode = "NEW"
                                                        }
                          };
            this.privileges = new List<string> { };
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(this.from, this.to, this.privileges));
            ex.Message.Should().Be("Cannot change product analysis code if part has a sales article.");
        }
    }
}
