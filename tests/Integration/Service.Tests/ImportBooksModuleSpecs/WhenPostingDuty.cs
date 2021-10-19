﻿namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingDuty : ContextBase
    {
        private readonly DateTime now = DateTime.Now;

        [SetUp]
        public void SetUp()
        {
            var postDutyResource = new PostDutyResource
                                       {
                                           CurrentUserNumber = 33107,
                                           SupplierId = 2,
                                           DatePosted = this.now.ToString("o"),
                                           OrderDetails = new List<ImportBookOrderDetailResource>(),
                                           ImpbookId = 12357
                                       };

            this.ImportBooksFacadeService.PostDuty(Arg.Any<PostDutyResource>()).Returns(
                new CreatedResult<ProcessResult>(new ProcessResult(true, "posted duty")));

            this.Response = this.Browser.Post(
                "/logistics/import-books/post-duty",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(postDutyResource);
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.ImportBooksFacadeService.Received().PostDuty(Arg.Any<PostDutyResource>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResult>();
            resource.Success.Should().BeTrue();
        }
    }
}
