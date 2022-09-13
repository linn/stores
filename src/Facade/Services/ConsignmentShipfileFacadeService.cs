namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;

    public class ConsignmentShipfileFacadeService : IConsignmentShipfileFacadeService
    {
        private readonly IRepository<ConsignmentShipfile, int> repository;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IConsignmentShipfileService domainService;

        private readonly IDatabaseService databaseService;

        public ConsignmentShipfileFacadeService(
            IRepository<ConsignmentShipfile, int> repository,
            IRepository<Consignment, int> consignmentRepository,
            IConsignmentShipfileService domainService,
            ITransactionManager transactionManager,
            IDatabaseService databaseService)
        {
            this.repository = repository;
            this.consignmentRepository = consignmentRepository;
            this.domainService = domainService;
            this.transactionManager = transactionManager;
            this.databaseService = databaseService;
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

        public IResult<ConsignmentShipfile> Add(ConsignmentShipfileResource resource)
        {
            if (!int.TryParse(resource.InvoiceNumbers.Trim(), out var invoiceNo))
            {
                return new BadRequestResult<ConsignmentShipfile>("Invalid Invoice Number");
            }

            var consignment =
                this.consignmentRepository.FindBy(x => x.Invoices.Any(i => i.DocumentNumber == invoiceNo));

            if (consignment == null)
            {
                return new BadRequestResult<ConsignmentShipfile>("Consignments Not Found");
            }

            var id = this.databaseService.GetIdSequence("SHIPFILE_EMAIL_SEQ");
            var shipfile = new ConsignmentShipfile
                               {
                                   ConsignmentId = consignment.ConsignmentId,
                                   Id = id
                               };
            this.repository.Add(shipfile);
            this.transactionManager.Commit();

            return new CreatedResult<ConsignmentShipfile>(shipfile);
        }
    }
}
