namespace Linn.Stores.Service.Tests.EmployeesModuleSpecs
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

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var employeeA = new Employee { Id = 118, FullName = "First Employee" };
            var employeeB = new Employee { Id = 13224, FullName = "Employee numba 2" };

            this.EmployeesService.GetAllEmployees().Returns(
                new SuccessResult<IEnumerable<Employee>>(new List<Employee> { employeeA, employeeB }));

            this.Response = this.Browser.Get(
                "/inventory/employees",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.EmployeesService.Received().GetAllEmployees();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<EmployeeResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Id == 118);
            resource.Should().Contain(a => a.Id == 13224);
        }
    }
}
