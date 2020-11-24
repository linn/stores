namespace Linn.Stores.Facade.Tests.EmployeesServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var employees = new List<Employee>
                                {
                                    new Employee { Id = 0, FullName = "Mr Employee" },
                                    new Employee { Id = 1, FullName = "Mrs Employee" },
                                    new Employee
                                        {
                                            Id = 2, 
                                            FullName = 
                                            "Mr Invalid", 
                                            DateInvalid = DateTime.UnixEpoch
                                        }
                                };
            this.EmployeeRepository.FilterBy(Arg.Any<Expression<Func<Employee, bool>>>())
                .Returns(employees.AsQueryable());
        }

        [Test]
        public void ShouldSearch()
        {
            this.Sut.SearchEmployees("Mr");
            this.EmployeeRepository.Received().FilterBy(Arg.Any<Expression<Func<Employee, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            var result = this.Sut.SearchEmployees("Mr");
            result.Should().BeOfType<SuccessResult<IEnumerable<Employee>>>();
        }

        [Test]
        public void ShouldReturnOnlyValidByDefault()
        {
            var result = this.Sut.SearchEmployees("Mr");
            result.Should().BeOfType<SuccessResult<IEnumerable<Employee>>>();
            var dataResult = ((SuccessResult<IEnumerable<Employee>>)result).Data;
            dataResult.All(e => e.DateInvalid == null).Should().Be(true);
        }

        [Test]
        public void ShouldReturnInvalidWhenSpecified()
        {
            var result = this.Sut.SearchEmployees("Mr", true);
            result.Should().BeOfType<SuccessResult<IEnumerable<Employee>>>();
            var dataResult = ((SuccessResult<IEnumerable<Employee>>)result).Data;
            dataResult.Count().Should().Be(3);
        }
    }
}
