namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class DepartmentService : IDepartmentsService
    {
        private readonly IQueryRepository<Department> repository;

        public DepartmentService(IQueryRepository<Department> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Department>> GetOpenDepartments(string searchTerm = null)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return new SuccessResult<IEnumerable<Department>>(
                    this.repository.FilterBy(d => !d.DateClosed.HasValue 
                                                  && (d.DepartmentCode.Contains(searchTerm) 
                                                         || d.Description
                                                             .ToUpper()
                                                             .Contains(searchTerm.ToUpper()))));
            }
            return new SuccessResult<IEnumerable<Department>>(
                this.repository.FilterBy(d => !d.DateClosed.HasValue));
        }
    }
}
