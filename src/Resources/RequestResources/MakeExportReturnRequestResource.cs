namespace Linn.Stores.Resources.RequestResources
{
    using System.Collections.Generic;

    public class MakeExportReturnRequestResource
    {
        public IEnumerable<int> Rsns { get; set; }

        public bool HubReturn { get; set; }  
    }
}