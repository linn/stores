namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IPdfService
    {
        Task<Stream> ConvertHtmlToPdf(string html, bool landscape);
    }
}
