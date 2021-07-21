namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Nancy.Facade;

    public class IEnumerableCsvResponseProcessor : CsvResponseProcessor<IEnumerable<IEnumerable<string>>>
    {
        public IEnumerableCsvResponseProcessor()
            : base(null, "results-list", 1)
        {
        }
    }
}
