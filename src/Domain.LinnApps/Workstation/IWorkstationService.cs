namespace Linn.Stores.Domain.LinnApps.Workstation
{
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public interface IWorkstationService
    {
        WorkstationTopUpStatus GetTopUpStatus();

        WorkstationTopUpStatus StartTopUpRun();

        bool CanStartNewRun();
    }
}
