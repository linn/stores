namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Models;

    public interface IImportBookService
    {
        void Update(ImportBook from, ImportBook to);

        IEnumerable<ImportBookExchangeRate> GetExchangeRates(string date);

        ProcessResult PostDutyForOrderDetails(
            IEnumerable<ImportBookOrderDetail> orderDetails,
            int supplierId,
            int employeeId,
            DateTime postDutyDate);
    }
}
