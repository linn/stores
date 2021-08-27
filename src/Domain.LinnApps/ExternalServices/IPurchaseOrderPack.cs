namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IPurchaseOrderPack
    {
        string GetDocumentType(int orderNumber);
    }
}
