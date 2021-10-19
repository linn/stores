namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;

    public interface IImportBookService
    {
        void Update(ImportBook from, ImportBook to);

        IEnumerable<ImportBookExchangeRate> GetExchangeRates(string date);

        void PostDutyForOrderDetails(
            IEnumerable<ImportBookOrderDetail> orderDetails,
            int supplierId,
            int employeeId,
            DateTime postDutyDate);
    }
}
