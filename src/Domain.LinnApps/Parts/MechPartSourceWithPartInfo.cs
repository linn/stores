using System.Collections.Generic;

namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class MechPartSourceWithPartInfo : MechPartSource
    {
        public Part LinnPart { get; set; }

        public IEnumerable<PartDataSheet> DataSheets { get; set; }

        public MechPartSourceWithPartInfo(MechPartSource source)
        {
            foreach (var prop in source.GetType().GetProperties())
                GetType().GetProperty(prop.Name)?.SetValue(this, prop.GetValue(source, null), null);
        }
    }
}
