namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Wand;

    using NUnit.Framework;

    public class WhenGettingWandStringSuggestionForNotSerialNumbered
    {
        private string suggestedWandString;

        [SetUp]
        public void SetUp()
        {
            this.suggestedWandString = WandService.WandStringSuggestion("N", 1, 4, "12345");
        }

        [Test]
        public void ShouldSuggestWandString()
        {
            this.suggestedWandString.Should().Be("1212345/4");
        }
    }
}
