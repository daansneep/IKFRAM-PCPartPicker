using System;

namespace Domain
{
    public class StorageDevice
    {
        public int Id { get; set; }
        public Part Part { get; set; }
        public int Gb { get; set; }
        public int Tb { get; set; }
        public bool Ssd { get; set; }
    }
}