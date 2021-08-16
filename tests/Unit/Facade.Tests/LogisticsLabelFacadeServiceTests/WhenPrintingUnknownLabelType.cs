namespace Linn.Stores.Facade.Tests.LogisticsLabelFacadeServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Resources.RequestResources;

    using NUnit.Framework;

    public class WhenPrintingUnknownLabelType : ContextBase
    {
        private LogisticsLabelRequestResource resource;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.resource = new LogisticsLabelRequestResource
                                {
                                    LabelType = "Does Not Exist"
                                };
            this.action = () => this.Sut.PrintLabel(this.resource);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<ProcessException>();
        }
    }
}
