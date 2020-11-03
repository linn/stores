namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class AssemblyTechnologiesResourceBuilder : IResourceBuilder<IEnumerable<AssemblyTechnology>>
    {
        private readonly AssemblyTechnologyResourceBuilder accountingCompaniesResourceBuilder =
            new AssemblyTechnologyResourceBuilder();

        public IEnumerable<AssemblyTechnologyResource> Build(IEnumerable<AssemblyTechnology> accountingCompanies)
        {
            return accountingCompanies
                .Select(a => this.accountingCompaniesResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<AssemblyTechnology>>.Build(IEnumerable<AssemblyTechnology> accountingCompaniess) => this.Build(accountingCompaniess);

        public string GetLocation(IEnumerable<AssemblyTechnology> accountingCompaniess)
        {
            throw new NotImplementedException();
        }
    }
}