namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface IGoodsInFacadeService
    {
        IResult<ProcessResult> DoBookIn(BookInRequestResource requestResource);
    }
}
