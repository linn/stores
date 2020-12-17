namespace Linn.Stores.Domain.LinnApps.Tests.WhatWillDecrementReportSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWhatWillDecrementReport : ContextBase
    {
        private ResultsModel result;

        private string partNumber = "KEEL";

        private int quantity = 1;

        [SetUp]
        public void Setup()
        {
            this.ProductionTriggerLevelsService.GetWorkStationCode(this.partNumber).Returns("STATION");

            this.WwdPack.JobId().Returns(1234);

            this.ChangeRequestRepository.FilterBy(Arg.Any<Expression<Func<ChangeRequest, bool>>>()).Returns(
                new List<ChangeRequest> { new ChangeRequest { OldPartNumber = "PART 1", NewPartNumber = "NEW" } }
                    .AsQueryable());

            this.WwdWorkRepository.FilterBy(Arg.Any<Expression<Func<WwdWork, bool>>>()).Returns(
                new List<WwdWork>
                    {
                        new WwdWork
                            {
                                PartNumber = "PART 1",
                                JobId = 1234,
                                StoragePlace = "PLACE 1",
                                QuantityAtLocation = 1,
                                LocationId = 1,
                                QuantityKitted = 2,
                                Remarks = "REM 1",
                                PalletNumber = 1,
                                Part = new Part { PartNumber = "PART 1", Description = "DESC 1" }
                            },
                        new WwdWork
                            {
                                PartNumber = "PART 2",
                                JobId = 1234,
                                StoragePlace = "PLACE 2",
                                QuantityAtLocation = 2,
                                LocationId = 2,
                                QuantityKitted = 2,
                                Remarks = "REM 2",
                                PalletNumber = 2,
                                Part = new Part { PartNumber = "PART 2", Description = "DESC 2" }
                            },
                    }.AsQueryable());

            this.WwdWorkDetailsRepository.FilterBy(Arg.Any<Expression<Func<WwdWorkDetail, bool>>>()).Returns(
                new List<WwdWorkDetail>
                    {
                        new WwdWorkDetail
                            {
                                JobId = 1234,
                                LocationGroup = "GROUP 1",
                                PartNumber = "PART 1",
                                Quantity = 1,
                                State = "STATE 1"
                            },
                        new WwdWorkDetail
                            {
                                JobId = 1234,
                                LocationGroup = "GROUP 2",
                                PartNumber = "PART 2",
                                Quantity = 2,
                                State = "STATE 2"
                            }
                    }.AsQueryable());

            this.result = this.Sut.WhatWillDecrementReport(this.partNumber, this.quantity, null, null);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("1 x KEEL from workstation: STATION");
        }

        [Test]
        public void ShouldSetRows()
        {
            this.result.Rows.Should().HaveCount(2);
        }

        [Test]
        public void ShouldSetColumns()
        {
            this.result.Columns.Should().HaveCount(10);
        }
    }
}