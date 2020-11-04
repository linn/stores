namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class AssemblyTechnologyResourceBuilder : IResourceBuilder<AssemblyTechnology>
    {
        public AssemblyTechnologyResource Build(AssemblyTechnology model)
        {
            return new AssemblyTechnologyResource { Name = model.Name, Description = model.Description };
        }

        public string GetLocation(AssemblyTechnology model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<AssemblyTechnology>.Build(AssemblyTechnology assemblyTechnology) => this.Build(assemblyTechnology);
    }
}
