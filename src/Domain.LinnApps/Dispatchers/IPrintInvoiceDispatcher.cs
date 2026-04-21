namespace Linn.Stores.Domain.LinnApps.Dispatchers
{
    public interface IPrintInvoiceDispatcher
    {
        void PrintInvoice(string printerUri, string documentType, int documentNumber, bool showTermsAndConditions, bool showPrices);
    }
}
