namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class NominalAccountResourceBuilder : IResourceBuilder<NominalAccount>
    {
        public NominalAccountResource Build(NominalAccount account)
        {
            return new NominalAccountResource
                       {
                           NominalCode = account.Nominal.NominalCode,
                           Description = account.Nominal.Description,
                           DepartmentCode = account.Department.DepartmentCode,
                           DepartmentDescription = account.Department.Description
                       };
        }

        public string GetLocation(NominalAccount account)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<NominalAccount>.Build(NominalAccount account) => this.Build(account);
    }
}
