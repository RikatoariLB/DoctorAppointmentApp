
public class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ClinicId { get; set; }
    public int CategoryId { get; set; }
    
    // Appointment details
    public DateTime AppointmentDate { get; set; }
    public TimeSpan TimeSlot { get; set; }
    public string Status { get; set; } = "Scheduled";
    public string? Notes { get; set; }

    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Clinic Clinic { get; set; } = null!;
    public Category Category { get; set; } = null!;
}