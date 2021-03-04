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

    public class WhenGettingNominalForDepartment : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var n = new List<NominalAccount> 
                        { 
                            new NominalAccount 
                                {
                                    Department = new Department { DepartmentCode = "CODE", Description = "DESC" },
                                    Nominal = new Nominal { NominalCode = "CODE", Description = "DESC" }
                                }
                        };

            this.NominalAccountsService.GetNominalAccounts("CODE").Returns(
                new SuccessResult<IEnumerable<NominalAccount>>(n));

            this.Response = this.Browser.Get(
                "/inventory/nominal-accounts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "CODE");
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
            this.NominalAccountsService.Received().GetNominalAccounts("CODE");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<NominalAccount>>();
            resource.Count().Should().Be(1);
        }
    }
}
