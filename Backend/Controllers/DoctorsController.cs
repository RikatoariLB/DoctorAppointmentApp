
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DTOs.Doctors;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<DoctorSearchResultDto>>> Search([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Search term is required");
        
        var results = await _context.Doctors
            .Include(d => d.Clinic)
            .Include(d => d.Speciality)
            .Where(d => d.Name.Contains(name))
            .Select(d => new DoctorSearchResultDto
            {
                FullName = d.Name,
                ClinicName = d.Clinic.Name,
                SpecialityName = d.Speciality.Name
            })
            .ToListAsync();
        
        return Ok(results);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
    {
        var doctors = await _context.Doctors
            .Include(d => d.Speciality)
            .Include(d => d.Clinic)
            .ToListAsync();
        
        return Ok(doctors);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(int id)
    {
        var doctor = await _context.Doctors
            .Include(d => d.Speciality)
            .Include(d => d.Clinic)
            .FirstOrDefaultAsync(d => d.DoctorId == id);
        
        if (doctor == null)
            return NotFound();
        
        return doctor;
    }
    
    [HttpPost]
    public async Task<ActionResult<Doctor>> CreateDoctor(CreateDoctorDto dto)
    {
        if (await _context.Doctors.AnyAsync(d => d.Email == dto.Email))
            return BadRequest("A doctor with this email already exists");
        
        var doctor = new Doctor
        {
            Name = dto.Name,
            Email = dto.Email,
            SpecialityId = dto.SpecialityId,
            ClinicId = dto.ClinicId
        };
        
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, doctor);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor(int id, UpdateDoctorDto dto)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
            return NotFound();

        if (await _context.Doctors.AnyAsync(d => d.Email == dto.Email && d.DoctorId != id))
            return BadRequest("A doctor with this email already exists");
        
        doctor.Name = dto.Name;
        doctor.Email = dto.Email;
        doctor.SpecialityId = dto.SpecialityId;
        doctor.ClinicId = dto.ClinicId;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var doctor = await _context.Doctors
            .Include(d => d.Appointments)
            .FirstOrDefaultAsync(d => d.DoctorId == id);
        
        if (doctor == null)
            return NotFound();

        if (doctor.Appointments.Any())
            return BadRequest("Cannot delete doctor with existing appointments");
        
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}