namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AddressResourceBuilder : IResourceBuilder<Address>
    {
        public AddressResource Build(Address address)
        {
            return new AddressResource
                       {
                           Id = address.Id,
                           Addressee = address.Addressee,
                           Addressee2 = address.Addressee2,
                           Line1 = address.Line1,
                           Line2 = address.Line2,
                           Line3 = address.Line3,
                           Line4 = address.Line4,
                           CountryCode = address.CountryCode,
                           CountryName = address.Country.DisplayName,
                           PostCode = address.PostCode,
                           DisplayAddress = this.GetPrintAddress(address)
                       };
        }

        public string GetLocation(Address address)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Address>.Build(Address address) => this.Build(address);

        private string GetPrintAddress(Address address)
        {
            var printAddress = $"{address.Addressee}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Addressee2) ? null : $"{address.Addressee2}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line1) ? null : $"{address.Line1}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line2) ? null : $"{address.Line2}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line3) ? null : $"{address.Line3}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line4) ? null : $"{address.Line4}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.PostCode) ? null : $"{address.PostCode}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Country.DisplayName) ? null : $"{address.Country.DisplayName}";

            return printAddress;
        }
    }
}
