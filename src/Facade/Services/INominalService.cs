namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface INominalService
    {
        IResult<Nominal> GetNominalForDepartment(string department);
    }
}