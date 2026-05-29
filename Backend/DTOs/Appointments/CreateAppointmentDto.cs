
namespace Backend.DTOs.Appointments;

public class CreateAppointmentDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public int? PatientId { get; set; }

    public int DoctorId { get; set; }
    public int ClinicId { get; set; }
    public int CategoryId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan TimeSlot { get; set; }
    public string? Notes { get; set; }
}