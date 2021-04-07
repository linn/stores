namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Resources.Wand;

    public class WandLogResultResourceBuilder : IResourceBuilder<WandLog>
    {
        public WandLogResource Build(WandLog wandLog)
        {
            if (wandLog == null)
            {
                return null;
            }

            return new WandLogResource
                       {
                           ArticleNumber = wandLog.ArticleNumber,
                           ConsignmentId = wandLog.ConsignmentId,
                           WandString = wandLog.WandString,
                           ContainerNo = wandLog.ContainerNo,
                           DateWanded = wandLog.DateWanded.ToString("o"),
                           EmployeeNumber = wandLog.EmployeeNumber,
                           Id = wandLog.Id,
                           OrderLine = wandLog.OrderLine,
                           OrderNumber = wandLog.OrderNumber,
                           QtyWanded = wandLog.QtyWanded,
                           SeriaNumber1 = wandLog.SeriaNumber1,
                           SeriaNumber2 = wandLog.SeriaNumber2,
                           ItemNo = wandLog.ItemNo,
                           TransType = wandLog.TransType
                       };
        }

        public string GetLocation(WandLog wandLog)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<WandLog>.Build(WandLog wandLog) => this.Build(wandLog);
    }
}
