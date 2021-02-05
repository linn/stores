namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;

    public interface IWarehouseFacadeService
    {
        IResult<string> MoveAllPalletsToUpper();

        IResult<string> MovePalletToUpper(int palletNumber, string reference);
    }
}
