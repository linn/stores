namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public interface IConsignmentShipfileFacadeService
    {
        IResult<IEnumerable<ConsignmentShipfile>> GetShipfiles();

        IResult<IEnumerable<ConsignmentShipfile>> SendEmails(IEnumerable<ConsignmentShipfileResource> toSend);

        IResult<IEnumerable<ConsignmentShipfile>> GetEmailDetails(IEnumerable<ConsignmentShipfileResource> shipfiles);
    }
}
