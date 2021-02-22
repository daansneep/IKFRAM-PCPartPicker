using System;

namespace Domain
{
    public class ProcessorCooler
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public Socket Socket { get; set; }
        public bool Rgb { get; set; }
        public bool Water { get; set; }
    }
}