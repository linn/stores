namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    public interface IConsignmentShipfileFacadeService
    {
        IResult<IEnumerable<ConsignmentShipfile>> GetShipfiles();

        IResult<IEnumerable<ConsignmentShipfile>> SendEmails(ConsignmentShipfilesSendEmailsRequestResource toSend);

        IResult<ConsignmentShipfile> DeleteShipfile(int id);

        IResult<ConsignmentShipfile> Add(ConsignmentShipfileResource resource);
    }
}
