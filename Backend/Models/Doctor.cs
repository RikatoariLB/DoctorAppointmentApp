
public class Doctor
{
    public int DoctorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int SpecialityId { get; set; }
    public int ClinicId { get; set; }

    public Speciality Speciality { get; set; } = null!;
    public Clinic Clinic { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}