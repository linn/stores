namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    public class ConsignmentShipfileFacadeService : IConsignmentShipfileFacadeService
    {
        private readonly IRepository<ConsignmentShipfile, int> repository;

        private readonly ITransactionManager transactionManager;

        private readonly IConsignmentShipfileService domainService;

        public ConsignmentShipfileFacadeService(
            IRepository<ConsignmentShipfile, int> repository,
            IConsignmentShipfileService domainService,
            ITransactionManager transactionManager)
        {
            this.repository = repository;
            this.domainService = domainService;
            this.transactionManager = transactionManager;
        }

        public IResult<IEnumerable<ConsignmentShipfile>> GetShipfiles()
        {
            return new SuccessResult<IEnumerable<ConsignmentShipfile>>(this.repository.FindAll());
        }

        public IResult<ConsignmentShipfile> SendEmails(
            ConsignmentShipfileSendEmailsRequestResource toSend)
        {
            try
            {
                var result = this.domainService.SendEmails(
                    new ConsignmentShipfile
                        {
                            Id = toSend.Shipfile.Id, 
                            ConsignmentId = toSend.Shipfile.ConsignmentId
                        },
                    toSend.Test,
                    toSend.TestEmailAddress);

                if (!toSend.Test)
                {
                    this.transactionManager.Commit();
                }

                return new SuccessResult<ConsignmentShipfile>(result);
            }
            catch (Exception e)
            {
                return new ServerFailureResult<ConsignmentShipfile>(e.Message);
            }
        }

        public IResult<ConsignmentShipfile> DeleteShipfile(int id)
        {
            var toDelete = this.repository.FindBy(s => s.Id == id);
            this.repository.Remove(toDelete);
            return new SuccessResult<ConsignmentShipfile>(toDelete);
        }
    }
}
