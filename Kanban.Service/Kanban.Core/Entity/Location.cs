namespace Kanban.Core.Entity
{
    public class Location
    {
        public int LocationId { get; set; }
        public string RackName { get; set; }
        public Shelf Shelf { get; set; }
        public ShelfSpace ShelfSpace { get; set; }
        public BoxSize BoxSize { get; set; }
        public BoxType BoxType { get; set; }
        public int ScaleId { get; set; }
        public string Item { get; set; }

        // Właściwość generująca LocationName
        public string LocationName
        { 
            get
            {
                return $"{RackName}{Shelf}{ShelfSpace}";
            }
        }
  
    }

    public enum Shelf
    {
        Shelf01 = 1,
        Shelf02 = 2,
        Shelf03 = 3,
        Shelf04 = 4,
        Shelf05 = 5,
        Shelf06 = 6,
        Shelf07 = 7
    }

    public enum ShelfSpace
    {
        Space01 = 1,
        Space02 = 2,
        Space03 = 3,
        Space04 = 4,
        Space05 = 5,
        Space06 = 6,
        Space07 = 7,
        Space08 = 8,
        Space09 = 9,
        Space10 = 10,
        Space11 = 11,
        Space12 = 12,
        Space13 = 13
    }

    public enum BoxSize
    {
        Small = 1,  // Oznacza 1 miejsce
        Medium = 2, // Oznacza 1,5 miejsca
        Large = 3   // Oznacza 2 miejsca
    }
    public enum BoxType
    {
        Planning,
        Bufab,
        Wurth
    }

}
