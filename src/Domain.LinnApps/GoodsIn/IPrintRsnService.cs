namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    public interface IPrintRsnService
    {
        void PrintRsn(int rsnNumber, int userNumber, string copy);
    }
}
