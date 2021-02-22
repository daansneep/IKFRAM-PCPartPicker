using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OperatingSystem = Domain.OperatingSystem;

namespace Persistence
{
    public class DataContext : IdentityDbContext<Admin>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Part> Parts { get; set; }
        public DbSet<RamType> RamTypes { get; set; }
        public DbSet<FormFactor> FormFactors { get; set; }
        public DbSet<Socket> Sockets { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<GraphicsCard> GraphicsCards { get; set; }
        public DbSet<OperatingSystem> OperatingSystems { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<ProcessorCooler> ProcessorCoolers { get; set; }
        public DbSet<StorageDevice> StorageDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
