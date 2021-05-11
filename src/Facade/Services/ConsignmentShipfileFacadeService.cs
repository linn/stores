namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ConsignmentShipfileFacadeService : IConsignmentShipfileFacadeService
    {
        private readonly IQueryRepository<ConsignmentShipfile> repository;

        private readonly IConsignmentShipfileService domainService;

        public ConsignmentShipfileFacadeService(
            IQueryRepository<ConsignmentShipfile> repository,
            IConsignmentShipfileService domainService)
        {
            this.repository = repository;
            this.domainService = domainService;
        }

        public IResult<IEnumerable<ConsignmentShipfile>> GetShipfiles()
        {
            return new SuccessResult<IEnumerable<ConsignmentShipfile>>(this.repository.FindAll());
        }

        public IResult<IEnumerable<ConsignmentShipfile>> SendEmails(
            IEnumerable<ConsignmentShipfileResource> toSend)
        {
            this.domainService.SendEmails(toSend.Select(
                s => new ConsignmentShipfile { Id = s.Id, ConsignmentId = s.ConsignmentId }));

            return new SuccessResult<IEnumerable<ConsignmentShipfile>>(
                new List<ConsignmentShipfile>());
        }
    }
}
