namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps.Models.Emails;

    public interface IShipfilePdfBuilder
    {
        Task<Stream> BuildPdf(ConsignmentShipfileEmailModel emailModel);
    }
}
