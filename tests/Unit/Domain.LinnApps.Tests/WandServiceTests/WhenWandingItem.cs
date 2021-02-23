﻿namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenWandingItem : ContextBase
    {
        private int consignmentId;

        private WandResult result;

        private string wandString;

        private string wandAction;

        private WandResult wandPackResult;

        [SetUp]
        public void SetUp()
        {
            this.wandAction = "W";
            this.consignmentId = 134;
            this.wandString = "flajdlfjd1312";
            this.wandPackResult = new WandResult { Message = "ok", Success = true };
            this.WandPack.Wand(this.wandAction, 100, this.consignmentId, this.wandString)
                .Returns(this.wandPackResult);

            this.result = this.Sut.Wand(this.wandAction, this.wandString, this.consignmentId);
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.WandPack.Received().Wand(this.wandAction, 100, this.consignmentId, this.wandString);
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be(this.wandPackResult.Message);
            this.result.Success.Should().BeTrue();
        }
    }
}
