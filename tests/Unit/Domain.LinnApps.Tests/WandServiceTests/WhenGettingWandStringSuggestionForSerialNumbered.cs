namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Wand;

    using NUnit.Framework;

    public class WhenGettingWandStringSuggestionForSerialNumbered
    {
        private string suggestedWandString;

        [SetUp]
        public void SetUp()
        {
            this.suggestedWandString = WandService.WandStringSuggestion("Y", 1, 1, "12345");
        }

        [Test]
        public void ShouldSuggestWandString()
        {
            this.suggestedWandString.Should().Be("0212345?");
        }
    }
}
