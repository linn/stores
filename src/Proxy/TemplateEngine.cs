namespace Linn.Stores.Proxy
{
    using System.IO;
    using System.Threading.Tasks;

    using Linn.Stores.Domain.LinnApps;

    using Scriban;

    public class TemplateEngine : ITemplateEngine
    {
        public async Task<string> Render(object model, string templatePath)
        {
            var templateString = await File.ReadAllTextAsync(templatePath);
            var template = Template.Parse(templateString);

            var result = await template.RenderAsync(model);

            return result;
        }
    }
}
