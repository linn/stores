namespace Linn.Stores.Domain.LinnApps.Tpk.Models
{
    public class TransferredStock : TransferableStock
    {
        public TransferredStock(TransferableStock parent, string notes)
        {
            foreach (var prop in parent.GetType().GetProperties())
            {
                this.GetType()
                    .GetProperty(prop.Name)
                    ?.SetValue(this, prop.GetValue(parent, null), null);
            }

            this.Notes = notes;
        }

        public string Notes { get; set; }
    }
}
