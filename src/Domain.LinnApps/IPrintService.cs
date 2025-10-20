namespace Linn.Stores.Domain.LinnApps
{
    using System.Threading.Tasks;

    public interface IPrintService
    {
        Task<PrintResult> PrintDocument(
            string printerUri,
            string documentType,
            int documentNumber,
            bool showTermsAndConditions,
            bool showPrices);
    }
}
