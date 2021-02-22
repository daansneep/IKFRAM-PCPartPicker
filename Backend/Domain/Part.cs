using System;

namespace Domain
{
    public class Part
    {
        
        public int PartId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double PurchasePrice { get; set; }
        public double RetailPrice { get; set; }
        public double Margin { get; set; }
        public DateTime  CreationDate { get; set; }
    }
}