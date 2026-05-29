
namespace Backend.DTOs.Doctors;

public class CreateDoctorDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int SpecialityId { get; set; }
    public int ClinicId { get; set; }
}