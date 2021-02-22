using System;

namespace Domain
{
    public class GraphicsCard
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public double ClockFreq { get; set; }
        public int Gb { get; set; }
        public string RamType { get; set; }
        public bool CrossSli { get; set; }
        public bool Rgb { get; set; }
    }
}