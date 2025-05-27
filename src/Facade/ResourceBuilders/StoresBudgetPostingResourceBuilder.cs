namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StoresBudgetPostingResourceBuilder : IResourceBuilder<StoresBudgetPosting>
    {
        public StoresBudgetPostingResource Build(StoresBudgetPosting posting)
        {
            return new StoresBudgetPostingResource
                       {
                           BudgetId = posting.BudgetId,
                           Sequence = posting.Sequence,
                           Quantity = posting.Quantity,
                           DebitOrCredit = posting.DebitOrCredit,
                           NominalAccount = posting.NominalAccount == null
                                                ? null
                                                : new NominalAccountResource
                                                      {
                                                          DepartmentCode = posting.NominalAccount.Department.DepartmentCode,
                                                          DepartmentDescription = posting.NominalAccount.Department.Description,
                                                          NominalCode = posting.NominalAccount.Nominal.NominalCode,
                                                          Description = posting.NominalAccount.Nominal.Description,
                                                          NominalAccountId = posting.NominalAccount.NominalAccountId
                                                      },
                           Product = posting.Product,
                           Building = posting.Building,
                           Vehicle = posting.Vehicle,
                           Person = posting.Person
                       };
        }

        public string GetLocation(StoresBudgetPosting posting)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StoresBudgetPosting>.Build(StoresBudgetPosting posting) => this.Build(posting);
    }
}
