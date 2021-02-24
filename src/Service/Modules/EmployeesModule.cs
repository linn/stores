namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class EmployeesModule : NancyModule
    {
        private readonly IEmployeeService employeesService;

        public EmployeesModule(IEmployeeService employeesService)
        {
            this.employeesService = employeesService;
            this.Get("inventory/employees", _ => this.GetEmployees());
        }

        private object GetEmployees()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = string.IsNullOrEmpty(resource.SearchTerm) ?
                              this.employeesService.GetAllEmployees() :
            this.employeesService.SearchEmployees(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
