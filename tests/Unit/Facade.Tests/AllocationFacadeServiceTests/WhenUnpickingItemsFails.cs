﻿namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenUnpickingItemsFails : ContextBase
    {
        private AccountOutletRequestResource resource;

        private IResult<IEnumerable<SosAllocDetail>> results;

        [SetUp]
        public void SetUp()
        {
            this.resource = new AccountOutletRequestResource { JobId = 1, AccountId = 2, OutletNumber = 3 };
            this.AllocationService.UnpickItems(1, 2, 3)
                .Throws(new UnpickItemsException("something"));

            this.results = this.Sut.UnpickItems(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationService.Received().UnpickItems(1, 2, 3);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<BadRequestResult<IEnumerable<SosAllocDetail>>>();
            var dataResult = (BadRequestResult<IEnumerable<SosAllocDetail>>)this.results;
            dataResult.Message.Should().Be("something");
        }
    }
}
