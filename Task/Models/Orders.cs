using System;

namespace Task.Models
{
    public class Orders
    {
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderQuantity { get; set; }
        public int Sales { get; set; }
        public string ShipMode { get; set; }
        public int Profit { get; set; }
        public int UnitPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSegment { get; set; }
        public string ProductCategory { get; set; }
    }
}

//OrderID   OrderDate OrderQuantity	Sales	ShipMode	Profit	UnitPrice	CustomerName	CustomerSegment	ProductCategory