namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;

    public interface IGetByBridgeIdService<T, in TKey, in TResource> : IFacadeService<T, TKey, TResource, TResource>
    {
        IResult<T> GetByBridgeId(int id);
    }
}
