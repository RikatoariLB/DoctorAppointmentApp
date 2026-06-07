
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DTOs.Categories;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        
        if (category == null)
            return NotFound();
            
        return Ok(category);
    }
    [HttpPost]
    public async Task<ActionResult<Category>> Create(CreateCategoryDto dto)
    {
        if (await _context.Categories.AnyAsync(c => c.Name == dto.Name))
            return BadRequest("A category with this name already exists");
        
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        if (await _context.Categories.AnyAsync(c => c.Name == dto.Name && c.CategoryId != id))
            return BadRequest("A category with this name already exists");
        
        category.Name = dto.Name;
        category.Description = dto.Description;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.CategoryId == id);
        
        if (category == null)
            return NotFound();

        if (category.Appointments.Any())
            return BadRequest("Cannot delete category with existing appointments");
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}