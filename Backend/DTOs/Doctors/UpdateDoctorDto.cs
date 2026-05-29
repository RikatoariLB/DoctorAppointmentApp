
namespace Backend.DTOs.Doctors;

public class UpdateDoctorDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SpecialityId { get; set; }
    public int ClinicId { get; set; }
}