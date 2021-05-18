namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IPdfBuilder
    {
        Task<Stream> BuildPdf(object model, string pathToTemplate);
    }
}
