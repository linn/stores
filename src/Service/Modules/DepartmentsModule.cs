namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class DepartmentsModule : NancyModule
    {
        private readonly IDepartmentsService departmentsService;

        private readonly INominalAccountsService nominalAccountsService;

        public DepartmentsModule(
            IDepartmentsService departmentsFacadeService,
            INominalAccountsService nominalAccountsService)
        {
            this.departmentsService = departmentsFacadeService;
            this.Get("inventory/departments", _ => this.GetDepartments());

            this.nominalAccountsService = nominalAccountsService;
            this.Get(
                "inventory/nominal-accounts", 
                parameters => this.GetNominalForDepartment());
        }

        private object GetDepartments()
        {
            var resource = this.Bind<DepartmentsRequestResource>();

            var results = resource.ProjectDeptsOnly.GetValueOrDefault() 
                              ? this.departmentsService.GetProjectDepartments() 
                              : this.departmentsService.GetOpenDepartments(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetNominalForDepartment()
        {
            var resource = this.Bind<SearchRequestResource>();
            var result = this.nominalAccountsService.GetNominalAccounts(resource.SearchTerm);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
