namespace Linn.Stores.Domain.LinnApps.Dispatchers
{
    public interface IPrintConsignmentNoteDispatcher
    {
        void PrintConsignmentNote(int consignmentId, string printerUri);
        
        void SaveConsignmentNote(int consignmentId, string fileName);
    }
}
