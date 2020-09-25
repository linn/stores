namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenCreatingAndUserHasPrivileges : ContextBase
    {
        private Part part;

        private Part result;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part();
           this.privileges = new List<string> { "part.admin" };

           this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
           // this.PartRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).ReturnsNull();
           this.PartRepository.FilterBy(Arg.Any<Expression<Func<Part, bool>>>())
               .Returns(new List<Part>
                            {
                                new Part
                                    {
                                        PartNumber = "CAP 431"
                                    }
                            }.AsQueryable());
           this.TemplateRepository.FindById(Arg.Any<string>()).Returns(new PartTemplate());
           this.PartPack.PartRoot(Arg.Any<string>()).Returns("ROOT");
           this.result = this.Sut.CreatePart(this.part, this.privileges);
        }

        [Test]
        public void ShouldReturnNewPart()
        {
            this.result.Should().BeOfType<Part>();
        }

        [Test]
        public void ShouldSetOrderHold()
        {
            this.result.OrderHold.Should().Be("N");
        }
    }
}