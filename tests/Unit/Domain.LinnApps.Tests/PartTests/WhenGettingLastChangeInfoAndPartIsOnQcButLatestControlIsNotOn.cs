namespace Linn.Stores.Domain.LinnApps.Tests.PartTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenGettingLastChangeInfoAndPartIsOnQcButLatestControlIsNotOn
    {
        private Part sut;
        private DateTime? resultDate;
        private Employee resultEmployee;

        [SetUp]
        public void SetUp()
        {
            this.sut = new Part
                           {
                               QcOnReceipt = "Y",
                               QcControls = new List<QcControl>
                                                {
                                                    new QcControl
                                                        {
                                                            Id = 1,
                                                            OnOrOffQc = "OFF",
                                                            TransactionDate = new DateTime(2024, 1, 1),
                                                            Employee = new Employee { FullName = "Some Employee" }
                                                        },
                                                    new QcControl
                                                        {
                                                            Id = 2,
                                                            OnOrOffQc = "OFF",
                                                            TransactionDate = new DateTime(2025, 1, 1),
                                                            Employee = new Employee { FullName = "Another Employee" }
                                                        }
                                                }
                           };

            this.resultDate = this.sut.GetDateQcFlagLastChanged();
            this.resultEmployee = this.sut.GetQcFlagLastChangedBy();
        }

        [Test]
        public void ShouldReturnNullDate()
        {
            this.resultDate.Should().BeNull();
        }

        [Test]
        public void ShouldReturnNullEmployee()
        {
            this.resultEmployee.Should().BeNull();
        }
    }
}
