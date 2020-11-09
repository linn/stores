namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Persistence;

    public class MechPartSourceService : IMechPartSourceService
    {
        private readonly IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository;

        public MechPartSourceService(IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository)
        {
            this.dataSheetRepository = dataSheetRepository;
        }

        public IEnumerable<PartDataSheet> GetUpdatedDataSheets(IEnumerable<PartDataSheet> from, IEnumerable<PartDataSheet> to)
        {
            var dataSheets = from.ToList();
            var newDataSheets = to;

            foreach (var newPartDataSheet in newDataSheets)
            {
                var match = dataSheets.FirstOrDefault(s => s.Sequence == newPartDataSheet.Sequence);
                if (match != null)
                {
                    dataSheets.First(s => s.Sequence == match.Sequence).PdfFilePath =
                        newPartDataSheet.PdfFilePath;
                }
                else
                {
                    dataSheets = dataSheets.Concat(new List<PartDataSheet> {newPartDataSheet}).ToList();
                }
            }

            return dataSheets;
        }
    }
}
