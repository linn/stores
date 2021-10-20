namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingFromTemplate : ContextBase
    {
        private Part part;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part
                            {
                                PartNumber = "CAP 431",
                                StockControlled = "N"
                            };
            this.privileges = new List<string> { "part.admin" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.PartRepository.FilterBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new List<Part>
                             {
                                 new Part
                                     {
                                         PartNumber = "CAP 431"
                                     }
                             }.AsQueryable());
            this.TemplateRepository.FindById(Arg.Any<string>()).Returns(new PartTemplate { NextNumber = 1 });
            this.PartPack.PartRoot(Arg.Any<string>()).Returns("CAP");
            this.Sut.CreatePart(this.part, this.privileges, true);
        }

        [Test]
        public void ShouldUpdatePartTemplateRepository()
        {
            this.TemplateRepository.Received().FindById("CAP");
        }
    }
}
