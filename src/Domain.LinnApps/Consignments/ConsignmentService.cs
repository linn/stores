namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class ConsignmentService : IConsignmentService
    {
        private readonly IRepository<Employee, int> employeeRepository;

        public ConsignmentService(IRepository<Employee, int> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public void CloseConsignment(Consignment consignment, int closedById)
        {
            if (closedById <= 0)
            {
                throw new ConsignmentCloseException(
                    $"Could not close consignment {consignment.ConsignmentId}. A valid closed by id must be supplied");
            }

            if (consignment.Status != "L")
            {
                throw new ConsignmentCloseException(
                    $"Could not close consignment {consignment.ConsignmentId} is already closed");
            }
        }
    }
}
