using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class SkillController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SkillController> _logger;
    private readonly IMapper _mapper;


    public SkillController(ILogger<SkillController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost("AddSkill")]
    public async Task<IActionResult> AddSkill(string name)
    {
        if(name != "")
        {
            Skill skillnew = new Skill();
            skillnew.title = name;

            await _context.Skills.AddAsync(skillnew);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }
        return Ok("il faut rentre un nom de compétence");
    }

    [HttpGet("listSkill")]
    public async Task<IActionResult> GetSkill()
    {
        List<Skill> list_Skill = await _context.Skills.ToListAsync();

        return Ok(list_Skill);
    }

}