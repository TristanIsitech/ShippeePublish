using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;


    public CompanyController(ILogger<CompanyController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost("AddCompany")]
    public async Task<IActionResult> CreateCompany(CompanyCreateDto company)
    {
        Company? companies = _mapper.Map<Company>(company);

        companies.payment = false;

        await _context.Companies.AddAsync(companies);
        await _context.SaveChangesAsync();

        return Ok("company bien créé");
    }

    [HttpGet("list_company")]
    public async Task<IActionResult> GetCompany()
    {
        List<Company> list_company = await _context.Companies.ToListAsync();

        List<CompanySelectDto>? companies = _mapper.Map<List<CompanySelectDto>>(list_company);

        return Ok(companies);
    }

    [HttpGet("ExistCompany")]
    public async Task<IActionResult> GetCompany(string name)
    {
        Company? personne = _context.Companies.FirstOrDefault(i => i.name.ToLower() == name.ToLower());

        if(personne != null)
        {
            return Ok(1);
        }
        
        return Ok(0);
    }

}