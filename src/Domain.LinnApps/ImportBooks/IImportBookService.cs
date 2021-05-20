using System;
using System.Collections.Generic;
using System.Text;

namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public interface IImportBookService
    {
        void Update(ImportBook from, 
                    IEnumerable<ImportBookInvoiceDetail> invoiceDetails, 
                    IEnumerable<ImportBookOrderDetail> orderDetails,
                    IEnumerable<ImportBookPostEntry> postEntries);
    }
}
