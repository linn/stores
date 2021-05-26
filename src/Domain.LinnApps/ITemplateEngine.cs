namespace Linn.Stores.Domain.LinnApps
{
    using System.Threading.Tasks;

    public interface ITemplateEngine
    {
        Task<string> Render(object model, string templatePath);
    }
}
