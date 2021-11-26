﻿using Microsoft.EntityFrameworkCore;

namespace EncounterAPI.Models
{
    public class EncounterContext : DbContext
    {
        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\Vartotojas\source\repos\EncounterDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(t => t.Username).IsUnique();
            modelBuilder.Entity<Rating>().HasKey(t => new { t.RouteId, t.UserId });
        }

    }
}