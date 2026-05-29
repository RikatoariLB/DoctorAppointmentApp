
namespace Backend.DTOs.Appointments;

public class UpdateAppointmentDto
{
    public DateTime AppointmentDate { get; set; }
    public TimeSpan TimeSlot { get; set; }
    public string? Notes { get; set; }
}