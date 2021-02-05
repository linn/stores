namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IWarehouseFacadeService
    {
        IResult<MessageResult> MoveAllPalletsToUpper();

        IResult<MessageResult> MovePalletToUpper(int palletNumber, string reference);
    }
}
