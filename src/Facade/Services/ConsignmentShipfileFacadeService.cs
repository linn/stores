namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Proxy;
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

        public IResult<IEnumerable<ConsignmentShipfile>> SendEmails(
           ConsignmentShipfilesSendEmailsRequestResource toSend)
        {
            var result = new List<ConsignmentShipfile>();
           
                foreach (var resource in toSend.Shipfiles)
                {
                    try
                    {
                        var sent = this.domainService.SendEmails(
                            new ConsignmentShipfile { Id = resource.Id, ConsignmentId = resource.ConsignmentId },
                            toSend.Test,
                            toSend.TestEmailAddress);
                        result.Add(sent);

                        this.transactionManager.Commit();
                    }
                    catch (PdfServiceException exception)
                    {
                        return new ServerFailureResult<IEnumerable<ConsignmentShipfile>>(
                            $"Error building pdf for consignment {resource.ConsignmentId}: {exception.Message}");
                    }
                    catch (Exception exception)
                    {
                        return new ServerFailureResult<IEnumerable<ConsignmentShipfile>>(
                            $"Unexpected error creating email for consignment {resource.ConsignmentId}: {exception.Message}");
                    }
                }
                
                return new SuccessResult<IEnumerable<ConsignmentShipfile>>(result);
        }

        public IResult<ConsignmentShipfile> DeleteShipfile(int id)
        {
            var toDelete = this.repository.FindBy(s => s.Id == id);
            this.repository.Remove(toDelete);
            return new SuccessResult<ConsignmentShipfile>(toDelete);
        }
    }
}
