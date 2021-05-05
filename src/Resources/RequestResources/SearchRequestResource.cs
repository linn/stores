namespace Linn.Stores.Resources.RequestResources
{
    public class SearchRequestResource
    {
        public string SearchTerm { get; set; }

        public bool ExactOnly { get; set; } = false;
    }
}
