using System;

namespace Domain
{
    public class OperatingSystem
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public double Size { get; set; }
        public bool OpenSource { get; set; }
    }
}