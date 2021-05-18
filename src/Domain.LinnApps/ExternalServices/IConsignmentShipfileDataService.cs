namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models.Emails;

    public interface IConsignmentShipfileDataService
    {
        ConsignmentShipfileEmailModel BuildEmailModel(int consignmentId, int outletAddressId);
    }
}
