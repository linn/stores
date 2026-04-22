namespace Linn.Stores.Resources.MessageDispatch
{
    public class PrintRsnMessageResource : MessageBase
    {
        public int RsnNumber { get; set; }
        
        public string FacilityCode { get; set; }
    }
}
