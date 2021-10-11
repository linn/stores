namespace Linn.Stores.Domain.LinnApps.Dispatchers
{
    public interface IPrintConsignmentNoteDispatcher
    {
        void PrintConsignmentNote(int consignmentId, string printer);
        
        void SaveConsignmentNote(int consignmentId, string fileName);
    }
}
