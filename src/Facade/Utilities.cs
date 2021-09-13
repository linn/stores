namespace Linn.Stores.Facade
{
    using System;

    using Linn.Stores.Domain.LinnApps;

    public static class Utilities
    {
        public static string GetPrintAddress(Address address)
        {
            if (address == null)
            {
                return string.Empty;
            }

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
