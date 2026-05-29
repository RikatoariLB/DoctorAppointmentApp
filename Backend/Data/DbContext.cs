
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Email).HasMaxLength(255).IsRequired();
            entity.Property(p => p.Phone).HasMaxLength(20).IsRequired();
            entity.Property(p => p.SSN).HasMaxLength(50);
            entity.Property(p => p.Gender).HasMaxLength(20);
            entity.Property(p => p.TaxNumber).HasMaxLength(50);
            entity.Property(p => p.Religion).HasMaxLength(50);
            entity.Property(p => p.DriversLicense).HasMaxLength(50);
            entity.Property(p => p.MedicalInsuranceNumber).HasMaxLength(100);
            entity.Property(p => p.PasswordHash).HasMaxLength(255);
            
            entity.HasIndex(p => p.Email).IsUnique();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.Property(d => d.Name).HasMaxLength(200).IsRequired();
            entity.Property(d => d.Email).HasMaxLength(255).IsRequired();
            entity.HasIndex(d => d.Email).IsUnique();
            entity.HasIndex(d => d.SpecialityId);
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(200).IsRequired();
            entity.Property(c => c.Address).HasMaxLength(500).IsRequired();
            entity.Property(c => c.Phone).HasMaxLength(20).IsRequired();
            entity.HasIndex(c => c.Name).IsUnique();
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.Property(s => s.Name).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(a => a.Status).HasMaxLength(50).IsRequired();
            entity.Property(a => a.Notes).HasMaxLength(1000);            
            entity.HasIndex(a => a.AppointmentDate);
        });

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Speciality)
            .WithMany(s => s.Doctors)
            .HasForeignKey(d => d.SpecialityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Clinic)
            .WithMany(c => c.Doctors)
            .HasForeignKey(d => d.ClinicId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Clinic)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ClinicId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Category)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasIndex(a => a.AppointmentDate);
        
        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.SpecialityId);

        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.Email)
            .IsUnique();

        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.Email)
            .IsUnique();

        modelBuilder.Entity<Clinic>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}