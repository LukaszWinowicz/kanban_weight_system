namespace Kanban.Core.Entity
{
    public class Location
    {
        public int LocationId { get; set; }
        public int ScaleId { get; set; }
        public int RackId { get; set; }
        public int ShelfId { get; set; }

        public BoxSize Size;

        public BoxType Type;
        public string Item { get; set; } = string.Empty;
        public string Supplier { get; set; } = string.Empty;

        public enum BoxSize
        {
            Small,
            Medium,
            Large
        }

        public enum BoxType
        {
            Planning,
            Bufab,
            Wurth
        }

        public enum ShelfSpace
        {
            '01',

        }
    }
}
