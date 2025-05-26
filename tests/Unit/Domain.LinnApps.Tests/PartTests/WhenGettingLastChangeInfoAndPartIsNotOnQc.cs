namespace Linn.Stores.Domain.LinnApps.Tests.PartTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenGettingLastChangeInfoAndPartIsNotOnQc
    {
        private Part sut;
        private DateTime? resultDate;
        private Employee resultEmployee;
        private DateTime expectedDate;
        private Employee expectedEmployee;

        [SetUp]
        public void SetUp()
        {
            this.expectedEmployee = new Employee { FullName = "Test Employee" };
            this.expectedDate = new DateTime(2025, 1, 1);

            this.sut = new Part
                           {
                               QcOnReceipt = "N",
                               QcControls = new List<QcControl>
                                                {
                                                    new QcControl
                                                        {
                                                            Id = 1,
                                                            OnOrOffQc = "ON",
                                                            TransactionDate = new DateTime(2024, 1, 1),
                                                            Employee = new Employee()
                                                        },
                                                    new QcControl
                                                        {
                                                            Id = 2,
                                                            OnOrOffQc = "OFF",
                                                            TransactionDate = this.expectedDate,
                                                            Employee = this.expectedEmployee
                                                        }
                                                }
                           };

            this.resultDate = this.sut.GetDateQcFlagLastChanged();
            this.resultEmployee = this.sut.GetQcFlagLastChangedBy();
        }

        [Test]
        public void ShouldReturnDate()
        {
            this.resultDate.Should().Be(this.expectedDate);
        }

        [Test]
        public void ShouldReturnEmployee()
        {
            this.resultEmployee.Should().Be(this.expectedEmployee);
        }
    }
}
