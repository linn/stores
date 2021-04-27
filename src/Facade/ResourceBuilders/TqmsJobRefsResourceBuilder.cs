namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    public class TqmsJobRefsResourceBuilder : IResourceBuilder<IEnumerable<TqmsJobRef>>
    {
        public IEnumerable<TqmsJobRefResource> Build(IEnumerable<TqmsJobRef> jobRefs)
        {
            return jobRefs
                .OrderByDescending(o => o.JobRef)
                .Select(a => new TqmsJobRefResource { DateOfRun = a.DateOfRun.ToString("o"), JobRef = a.JobRef });
        }

        public string GetLocation(IEnumerable<TqmsJobRef> jobRefs)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<TqmsJobRef>>.Build(IEnumerable<TqmsJobRef> jobRefs)
        {
            return this.Build(jobRefs);
        }
    }
}
