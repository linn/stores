namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IStoragePlaceAuditPack
    {
        string CreateAuditReq(string auditLocation, int createdBy, string department);
    }
}
