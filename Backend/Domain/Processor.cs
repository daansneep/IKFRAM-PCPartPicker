using System;

namespace Domain
{
    public class Processor
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public Socket Socket { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public int ClockFreq { get; set; }
        public int TurboFreq { get; set; }
        public bool Oc { get; set; }
        public bool Graph { get; set; }
    }
}