using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database.Models;

namespace PeoplesPartnership.ApiRefactor.Database
{
    public class StudioContext : DbContext
    {
        
        public virtual DbSet<StudioItem> StudioItems { get; set; }
        public virtual DbSet<StudioItemType> StudioItemTypes { get; set; }

        public StudioContext(DbContextOptions<StudioContext> options) : base(options) { }
        public StudioContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //extract into contained config for each object
            modelBuilder.Entity<StudioItem>()
            .HasOne(s => s.StudioItemType)
            .WithMany(ad => ad.StudioItem)
            .HasForeignKey(ad => ad.StudioItemTypeId);

            modelBuilder.Entity<StudioItem>()
                .Property(x => x.SoldFor).HasPrecision(18, 2);
            
            modelBuilder.Entity<StudioItem>()
                .Property(x => x.Price).HasPrecision(18, 2);
            
            modelBuilder.Entity<StudioItemType>().HasData(
            new StudioItemType { StudioItemTypeId = 1, Value = "Synthesiser" },
            new StudioItemType { StudioItemTypeId = 2, Value = "Drum Machine" },
            new StudioItemType { StudioItemTypeId = 3, Value = "Effect" },
            new StudioItemType { StudioItemTypeId = 4, Value = "Sequencer" },
            new StudioItemType { StudioItemTypeId = 5, Value = "Mixer" },
            new StudioItemType { StudioItemTypeId = 6, Value = "Oscillator" },
            new StudioItemType { StudioItemTypeId = 7, Value = "Utility" }
            );
        }
    }
}
