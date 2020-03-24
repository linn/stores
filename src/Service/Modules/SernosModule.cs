namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class SernosModule : NancyModule
    {
        private readonly ISernosSequencesService sernosSequencesService;

        public SernosModule(ISernosSequencesService sernosSequencesFacadeService)
        {
            this.sernosSequencesService = sernosSequencesFacadeService;
            this.Get("inventory/sernos-sequences", _ => this.GetSernosSequences());
        }

        private object GetSernosSequences()
        {
            var results = this.sernosSequencesService.GetSequences();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}