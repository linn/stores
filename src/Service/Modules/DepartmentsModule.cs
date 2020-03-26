namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class DepartmentsModule : NancyModule
    {
        private readonly IDepartmentsService departmentsService;

        private readonly INominalService nominalService;

        public DepartmentsModule(
            IDepartmentsService departmentsFacadeService,
            INominalService nominalService)
        {
            this.departmentsService = departmentsFacadeService;
            this.Get("inventory/departments", _ => this.GetDepartments());

            this.nominalService = nominalService;
            this.Get(
                "inventory/nominal-for-department/{dept}", 
                parameters => this.GetNominalForDepartment(parameters.dept));
        }

        private object GetDepartments()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = this.departmentsService.GetOpenDepartments(resource.SearchTerm);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetNominalForDepartment(string dept)
        {
            var result = this.nominalService.GetNominalForDepartment(dept);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}