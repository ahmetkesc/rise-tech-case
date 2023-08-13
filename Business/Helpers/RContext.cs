using Microsoft.EntityFrameworkCore;
using Model.Classes;

namespace Business.Helpers;

public class RContext : DbContext
{
    public RContext(DbContextOptions<RContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Contact
        builder.Entity<Contact>(e =>
        {
            e.ToTable("contact");
            e.HasKey(p => p.Id);

            e.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("uuid");

            e.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("text");

            e.Property(p => p.Surname)
                .HasColumnName("surname")
                .HasColumnType("text");


            e.Property(p => p.Company)
                .HasColumnName("company")
                .HasColumnType("text");

            e.Property(p => p.LocationDetail)
                .HasColumnName("location_detail")
                .HasColumnType("jsonb");
        });
        #endregion

        #region Report
        builder.Entity<Report>(e =>
        {
            e.ToTable("report");
            e.HasKey(p => p.Id);

            e.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("uuid");

            e.Property(p => p.RequestDate)
                .HasColumnName("request_date")
                .HasColumnType("timestamp(6)");

            e.Property(p => p.Detail)
                .HasColumnName("detail")
                .HasColumnType("jsonb");

            e.Property(p => p.IsReady)
                .HasColumnName("isready")
                .HasColumnType("boolean");

            e.Property(p => p.ReportSituation)
                .HasColumnName("report_situation")
                .HasColumnType("text");
        });
        #endregion
    }
    public  DbSet<Contact> Contact { get; set; }
    public  DbSet<Report> Report { get; set; }
}