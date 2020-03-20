namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class DepartmentsModule : NancyModule
    {
        private readonly IDepartmentsService departmentsService;

        public DepartmentsModule(IDepartmentsService departmentsFacadeService)
        {
            this.departmentsService = departmentsFacadeService;
            this.Get("inventory/departments", _ => this.GetDepartments());
        }

        private object GetDepartments()
        {
            var results = this.departmentsService.GetOpenDepartments();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}