using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Activity;
using Domain.Entities.Lookup;

namespace Persistance
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Users> users { get; set; }
        public DbSet<UserType> usertype { get; set; }
        public DbSet<UserMedicalInfo> usermedicalinfo { get; set; }
        public DbSet<SASSDetails> sassdetails { get; set; }
        public DbSet<LocationState> locationstate { get; set; }
        public DbSet<LocationCountry> locationcountry { get; set; }
        public DbSet<GenderType> gendertype { get; set; }
        public DbSet<MaritalStatus> maritalstatus { get; set; }
        public DbSet<ActivityType> activitytype { get; set; }
        public DbSet<IntensityLevel> intensitylevel { get; set; }
        public DbSet<DailyActivityTracking> userdailytrack  { get; set; }
        public DbSet<ActivityTracking> useractivitytrack { get; set; }
        public DbSet<GoalSetting> goalsetting { get; set; }
        public DbSet<GoalType> goaltype { get; set; }
    }
}
