using System;
using System.Collections.Generic;
using System.Text;

namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public interface IImportBookService
    {
        void Update(ImportBook from, ImportBookInvoiceDetail invoiceDetail, ImportBookOrderDetail orderDetail, ImportBookPostEntry postEntry);
    }
}
