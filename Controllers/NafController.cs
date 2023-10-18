using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class NafController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NafController> _logger;
    private readonly IMapper _mapper;


    public NafController(ILogger<NafController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpGet("list_naf_section_division")]
    public async Task<IActionResult> GetNaf()
    {
        List<Naf_Division> list_naf_division = await _context.Naf_Divisions.ToListAsync();
        


        List<Naf_Section> list_naf_section = await _context.Naf_Sections.ToListAsync();


        List<NafDto> naf_dto = new List<NafDto>();


        foreach(Naf_Section nafsec in list_naf_section)
        {
            List<NafDivisonDto> list_naf_division_dto = new List<NafDivisonDto>();
            NafDto naft = new NafDto();


            naft.id_naf_section = nafsec.id;
            naft.name_naf_section = nafsec.title;

            foreach(Naf_Division nafdiv in list_naf_division)
            {
                if(nafdiv.id_naf_section == nafsec.id)
                {
                    NafDivisonDto listtest = _mapper.Map<NafDivisonDto>(nafdiv);
                    list_naf_division_dto.Add(listtest);
                }
            }

            naft.naf_division = list_naf_division_dto;

            naf_dto.Add(naft);
        }

        return Ok(naf_dto);
    }
    
    [HttpGet("list_naf_section")]
    public async Task<IActionResult> GetNafSection()
    {
        List<Naf_Section> list_naf_section = await _context.Naf_Sections.ToListAsync();

        return Ok(list_naf_section);
    }

}