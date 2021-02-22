using System;

namespace Domain
{
    public class Ram
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public RamType RamType { get; set; }
        public int Gb { get; set; }
        public int StickCount { get; set; }
        public int ClockFreq {get; set; }
        public bool Rgb { get; set; }
    }
}