namespace Linn.Stores.Facade.Tests.ConsignmentShipfilesFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAdding: ContextBase
    {
        private IResult<ConsignmentShipfile> result;

        [SetUp]
        public void SetUp()
        {
            this.ConsignmentRepository.FindBy(Arg.Any<Expression<Func<Consignment, bool>>>())
                .Returns(new Consignment { ConsignmentId = 1 });
            this.DatabaseService.GetIdSequence("SHIPFILE_EMAIL_SEQ").Returns(2);

            this.result = this.Sut.Add(new ConsignmentShipfileResource { InvoiceNumbers = "123" });
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.result.GetType().Should().Be(typeof(CreatedResult<ConsignmentShipfile>));
            ((CreatedResult<ConsignmentShipfile>)this.result).Data.Id.Should().Be(2);
        }
    }
}
