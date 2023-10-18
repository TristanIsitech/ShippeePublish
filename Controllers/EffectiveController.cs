using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class EffectiveController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EffectiveController> _logger;
    private readonly IMapper _mapper;


    public EffectiveController(ILogger<EffectiveController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpGet("list_effective")]
    public async Task<IActionResult> GetEffective()
    {
        List<Effective> list_effective = await _context.Effectives.ToListAsync();

        return Ok(list_effective);
    }

}