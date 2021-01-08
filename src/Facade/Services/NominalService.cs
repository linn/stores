namespace Linn.Stores.Facade.Services
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class NominalService : INominalService
    {
        private readonly IQueryRepository<Nominal> nominalRepository;

        private readonly IQueryRepository<Department> departmentRepository;

        private readonly IQueryRepository<NominalAccount> nominalAccountRepository;

        public NominalService(
            IQueryRepository<Nominal> nominalRepository,
            IQueryRepository<Department> departmentRepository,
            IQueryRepository<NominalAccount> nominalAccountRepository)
        {
            this.nominalRepository = nominalRepository;
            this.departmentRepository = departmentRepository;
            this.nominalAccountRepository = nominalAccountRepository;
        }

        public IResult<Nominal> GetNominalForDepartment(string department)
        {
            var nominal = this.departmentRepository.FindAll()
                .Join(
                    this.nominalAccountRepository.FilterBy(a => a.Department.DepartmentCode == department),
                dept => dept.DepartmentCode,
                nomacc => nomacc.Department.DepartmentCode,
                (dept, nomacc) => nomacc.Nominal).ToList().FirstOrDefault();
            var result = this.nominalRepository.FindBy(n => n.NominalCode.Equals(nominal.NominalCode));
            return new SuccessResult<Nominal>(result);
        }
    }
}
