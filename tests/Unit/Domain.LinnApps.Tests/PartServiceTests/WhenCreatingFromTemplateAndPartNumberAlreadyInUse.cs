namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingFromTemplateAndPartNumberAlreadyInUse : ContextBase
    {
        private Part part;

        private List<string> privileges;

        private Exception result;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part { StockControlled = "N" };
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
            this.PartRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part());
            this.TemplateRepository.FindById(Arg.Any<string>()).Returns(new PartTemplate());
            this.PartPack.PartRoot(Arg.Any<string>()).Returns("ROOT");
            this.result = Assert.Throws<CreatePartException>(() => this.Sut.CreatePart(this.part, this.privileges));
        }

        [Test]
        public void ShouldThrowException()
        {
            this.result.Should().BeOfType<CreatePartException>();
        }

        [Test]
        public void ShouldSuggestValidNextNumber()
        {
            this.result.Message.Should()
                .Be("A Part with that Part Number already exists. Why not try 432");
        }
    }
}