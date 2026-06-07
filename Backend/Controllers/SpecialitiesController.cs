
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DTOs.Specialities;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialitiesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public SpecialitiesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Speciality>>> GetAll()
    {
        var specialities = await _context.Specialities.ToListAsync();
        return Ok(specialities);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Speciality>> GetById(int id)
    {
        var speciality = await _context.Specialities.FindAsync(id);
        
        if (speciality == null)
            return NotFound();
            
        return Ok(speciality);
    }
    [HttpPost]
    public async Task<ActionResult<Speciality>> Create(CreateSpecialityDto dto)
    {
        if (await _context.Specialities.AnyAsync(s => s.Name == dto.Name))
            return BadRequest("A speciality with this name already exists");
        
        var speciality = new Speciality
        {
            Name = dto.Name
        };
        
        _context.Specialities.Add(speciality);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = speciality.SpecialityId }, speciality);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSpecialityDto dto)
    {
        var speciality = await _context.Specialities.FindAsync(id);
        if (speciality == null)
            return NotFound();

        if (await _context.Specialities.AnyAsync(s => s.Name == dto.Name && s.SpecialityId != id))
            return BadRequest("A speciality with this name already exists");
        
        speciality.Name = dto.Name;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var speciality = await _context.Specialities
            .Include(s => s.Doctors)
            .FirstOrDefaultAsync(s => s.SpecialityId == id);
        
        if (speciality == null)
            return NotFound();

        if (speciality.Doctors.Any())
            return BadRequest("Cannot delete speciality with existing doctors");
        
        _context.Specialities.Remove(speciality);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}