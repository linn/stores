namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IShipfilePdfBuilder
    {
        Task<Stream> BuildPdf(ConsignmentShipfile shipfile);
    }
}
