using MissionControl.Models.DB;
using System.Data.Entity;

namespace MissionControl.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=umbracoDbDSN")
        {
        }

        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<MissionReport> reports { get; set; }
        public virtual DbSet<MissionImage> images { get; set; }
        public virtual DbSet<Facilitys> facilitys { get; set; }
        public virtual DbSet<ApiKeys> apikeys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MyUser>()
            //    .HasMany(x => x.Movies);

            //modelBuilder.Entity<Movie>()
            //    .HasRequired(x => x.MyUser);

            modelBuilder.Entity<User>()
                .HasMany(x => x.missionreports)
                .WithRequired(x => x.user)
                .HasForeignKey(x => x.UserId)
                ;

            modelBuilder.Entity<MissionReport>()
                .HasMany(x => x.mission_images)
                .WithRequired(x=>x.mission_report)
                .HasForeignKey(x=>x.MissionReportId)
                ;
        }
    }
}/**/