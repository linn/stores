namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models.Emails;

    public interface IConsignmentShipfileDataService
    {
        ConsignmentShipfilePdfModel GetPdfModelData(int consignmentId, int addressId);
    }
}
