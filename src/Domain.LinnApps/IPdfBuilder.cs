namespace Linn.Stores.Domain.LinnApps
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IPdfBuilder<T>
    {
        Task<Stream> BuildPdf(T model, string pathToTemplate);
    }
}
