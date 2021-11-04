namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingDutyWithoutPermission : ContextBase
    {
        private IEnumerable<ImportBookOrderDetail> orderDetails;

        [SetUp]
        public void SetUp()
        {
            this.orderDetails = new List<ImportBookOrderDetail>
                                    {
                                        new ImportBookOrderDetail { PostDuty = "Y" },
                                        new ImportBookOrderDetail { PostDuty = "Y" }
                                    };

            this.AuthorisationService.HasPermissionFor("import-books.admin", Arg.Any<IEnumerable<string>>())
                .Returns(false);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<PostDutyException>(
                () => this.Sut.PostDutyForOrderDetails(
                    this.orderDetails,
                    1234,
                    56789,
                    DateTime.Now,
                    new List<string> { "not-impbooks.admin" }));
            ex.Message.Should().Be("You are not authorised to post duty");
        }
    }
}
