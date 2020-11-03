namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IProductionTriggerLevelsService
    {
        string GetWorkStationCode(string partNumber);
    }
}