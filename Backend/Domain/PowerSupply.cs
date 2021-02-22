using System;

namespace Domain
{
    public class PowerSupply
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public int Power { get; set; }
        public bool Modular { get; set; }
        public string PowerRating { get; set; }
    }
}