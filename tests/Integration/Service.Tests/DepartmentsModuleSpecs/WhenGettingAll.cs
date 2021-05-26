namespace Linn.Stores.Service.Tests.DepartmentsModuleSpecs
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
            var departmentA = new Department
                               {
                                   DepartmentCode = "A",
                                   Description = "description A"
                               };
            var departmentB = new Department
                               {
                                   DepartmentCode = "B",
                                   Description = "description B"
                               };

            this.DepartmentsService.GetOpenDepartments()
                .Returns(new SuccessResult<IEnumerable<Department>>(new List<Department> { departmentA, departmentB }));

            this.Response = this.Browser.Get(
                "/inventory/departments",
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
            this.DepartmentsService.Received().GetOpenDepartments();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<DepartmentResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.DepartmentCode == "A");
            resource.Should().Contain(a => a.DepartmentCode == "B");
        }
    }
}