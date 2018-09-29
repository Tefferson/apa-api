﻿using crosscutting.identity.models;
using domain.models;
using Microsoft.EntityFrameworkCore;

namespace infra.data.context
{
    public class ApaContext : DbContext
    {
        public ApaContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<SensorDevice>()
                .HasKey(sd => new { sd.SensorId, sd.DeviceId });

            modelBuilder
                .Entity<RecognizedSoundLog>()
                .HasKey(r => new { r.SensorId, r.LabelNumber });
        }

        public virtual DbSet<SoundLabel> SoundLabel { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<SensorDevice> SensorDevice { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<RecognizedSoundLog> RecognizedSoundLog { get; set; }
    }
}
