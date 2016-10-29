namespace Knowlead.DomainModel.LookupModels.Core
{
    public class FOS : _CoreLookup
    {
        public FOS ParentFos { get; set; }

        public int? ParentFosId { get; set; }
    }
}