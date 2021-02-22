using System;

namespace Domain
{
    public class Motherboard
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public Socket Socket { get; set; }
        public FormFactor FormFactor { get; set; }
        public RamType RamType { get; set; }
        public string Chipset { get; set; }
        public bool Oc { get; set; }
        public bool Rgb { get; set; }
    }
}