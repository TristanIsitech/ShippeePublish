using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class DiplomeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DiplomeController> _logger;
    private readonly IMapper _mapper;


    public DiplomeController(ILogger<DiplomeController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpGet("list_diplome")]
    public async Task<IActionResult> GetDiplome()
    {
        List<Diplome> list_diplome = await _context.Diplomes.ToListAsync();

        return Ok(list_diplome);
    }

}