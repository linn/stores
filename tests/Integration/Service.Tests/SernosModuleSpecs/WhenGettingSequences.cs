namespace Linn.Stores.Service.Tests.SernosModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSequences : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var sernosSequenceA = new SernosSequence
                                   {
                                       Sequence = "A",
                                       Description = "description A"
                                   };
            var sernosSequenceB = new SernosSequence
                                   {
                                       Sequence = "B",
                                       Description = "description B"
                                   };

            this.SernosSequencesService.GetSequences()
                .Returns(new SuccessResult<IEnumerable<SernosSequence>>(new List<SernosSequence> { sernosSequenceA, sernosSequenceB }));


            this.Response = this.Browser.Get(
                "/inventory/sernos-sequences",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.SernosSequencesService.Received().GetSequences();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<SernosSequenceResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.SequenceName == "A");
            resource.Should().Contain(a => a.SequenceName == "B");
        }
    }
}