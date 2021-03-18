namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportReturnDetailFacadeService : FacadeService<ExportReturnDetail, ExportReturnDetailKey,
        ExportReturnDetailResource, ExportReturnDetailResource>
    {
        public ExportReturnDetailFacadeService(IRepository<ExportReturnDetail, ExportReturnDetailKey> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ExportReturnDetail CreateFromResource(ExportReturnDetailResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ExportReturnDetail entity, ExportReturnDetailResource updateResource)
        {
            entity.ArticleNumber = updateResource.ArticleNumber;
            entity.LineNo = updateResource.LineNo;
            entity.Qty = updateResource.Qty;
            entity.Description = updateResource.Description;
            entity.CustomsValue = updateResource.CustomsValue;
            entity.BaseCustomsValue = updateResource.BaseCustomsValue;
            entity.TariffId = updateResource.TariffId;
            entity.ExpinvDate = updateResource.ExpinvDate != null
                                    ? DateTime.Parse(updateResource.ExpinvDate)
                                    : (DateTime?)null;
            entity.ExpinvDocumentType = updateResource.ExpinvDocumentType;
            entity.ExpinvDocumentNumber = updateResource.ExpinvDocumentNumber;
            entity.NumCartons = updateResource.NumCartons;
            entity.Weight = updateResource.Weight;
            entity.Width = updateResource.Width;
            entity.Height = updateResource.Height;
            entity.Depth = updateResource.Depth;
        }

        protected override Expression<Func<ExportReturnDetail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}