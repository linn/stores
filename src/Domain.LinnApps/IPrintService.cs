namespace Linn.Stores.Domain.LinnApps
{
    public interface IPrintService
    {
        void PrintDocument(
            string printerUri,
            string documentType,
            int documentNumber,
            bool showTermsAndConditions,
            bool showPrices);
    }
}
