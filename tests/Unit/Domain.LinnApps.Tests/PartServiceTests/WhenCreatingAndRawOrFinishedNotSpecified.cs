﻿namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenCreatingAndRawOrFinishedNotSpecified : ContextBase
    {
        private Part part;

        private List<string> privileges;

        private Exception result;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part { StockControlled = "Y" };
            this.privileges = new List<string> { "part.admin" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
          
            this.TemplateRepository.FindById(Arg.Any<string>()).ReturnsNull();
            this.PartPack.PartRoot(Arg.Any<string>()).Returns("PART");
            this.result = Assert.Throws<CreatePartException>(() => this.Sut.CreatePart(this.part, this.privileges, false));
        }

        [Test]
        public void ShouldThrowException()
        {
            this.result.Should().BeOfType<CreatePartException>();
            this.result.Message.Should()
                .Be("Must specify raw or finished!");
        }
    }
}
