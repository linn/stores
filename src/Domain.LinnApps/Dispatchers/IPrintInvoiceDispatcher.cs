namespace Linn.Stores.Domain.LinnApps.Dispatchers
{
    public interface IPrintInvoiceDispatcher
    {
        void PrintInvoice(int documentNumber, string documentType, string copyType, string showPrices);
    }
}
