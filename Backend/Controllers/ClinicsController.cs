
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DTOs.Clinics;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ClinicsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Clinic>>> GetAll()
    {
        var clinics = await _context.Clinics.ToListAsync();
        return Ok(clinics);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Clinic>> GetById(int id)
    {
        var clinic = await _context.Clinics.FindAsync(id);
        
        if (clinic == null)
            return NotFound();
            
        return Ok(clinic);
    }
    [HttpPost]
    public async Task<ActionResult<Clinic>> Create(CreateClinicDto dto)
    {
        if (await _context.Clinics.AnyAsync(c => c.Name == dto.Name && c.Address == dto.Address))
            return BadRequest("A clinic with this name and address already exists");
        
        var clinic = new Clinic
        {
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone
        };
        
        _context.Clinics.Add(clinic);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = clinic.ClinicId }, clinic);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateClinicDto dto)
    {
        var clinic = await _context.Clinics.FindAsync(id);
        if (clinic == null)
            return NotFound();

        if (await _context.Clinics.AnyAsync(c => c.Name == dto.Name && c.Address == dto.Address && c.ClinicId != id))
            return BadRequest("A clinic with this name and address already exists");
        
        clinic.Name = dto.Name;
        clinic.Address = dto.Address;
        clinic.Phone = dto.Phone;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clinic = await _context.Clinics
            .Include(c => c.Doctors)
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.ClinicId == id);
        
        if (clinic == null)
            return NotFound();

        if (clinic.Doctors.Any())
            return BadRequest("Cannot delete clinic with existing doctors");
        
        if (clinic.Appointments.Any())
            return BadRequest("Cannot delete clinic with existing appointments");
        
        _context.Clinics.Remove(clinic);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}