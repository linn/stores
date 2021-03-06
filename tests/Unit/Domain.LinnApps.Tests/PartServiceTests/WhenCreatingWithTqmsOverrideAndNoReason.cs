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

    using NUnit.Framework;

    public class WhenCreatingWithTqmsOverrideAndNoReason : ContextBase
    {
        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.to = new Part
                          {
                              TqmsCategoryOverride = "override"
                          };
            this.privileges = new List<string> { "part.admin" };
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
            this.AuthService.HasPermissionFor(Arg.Any<string>(), this.privileges).Returns(true);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.CreatePart(this.to, this.privileges));
            ex.Message.Should()
                .Be("You must enter a reason and/or reference or project code when setting an override");
        }
    }
}