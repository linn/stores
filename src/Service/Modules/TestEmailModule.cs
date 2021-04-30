namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.Extensions;
    using Nancy.ModelBinding;

    public sealed class TestEmailModule : NancyModule
    {
        private readonly IEmailService emailService;

        public TestEmailModule(IEmailService emailService)
        {
            this.emailService = emailService;
            this.Post("/inventory/test-email", parameters => this.GetSalesOutlets());
        }

        private object GetSalesOutlets()
        {
            var resource = this.Bind<TestEmailResource>();

            this.emailService.SendEmail(
                resource.ToAddress,
                resource.ToName,
                resource.FromAddress,
                resource.FromName,
                resource.Subject,
                resource.Body,
                null);

            return this.Negotiate
                .WithStatusCode(200)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        public class TestEmailResource
        {
            public string ToAddress { get; set; }

            public string ToName { get; set; }

            public string FromAddress { get; set; }

            public string FromName { get; set; }

            public string Subject { get; set; }

            public string Body { get; set; }
        }
    }
}
