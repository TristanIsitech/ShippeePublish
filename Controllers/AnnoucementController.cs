using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Aspose.Cells.Charts;
using System.Diagnostics.CodeAnalysis;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class AnnoucementController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AnnoucementController> _logger;
    private readonly IMapper _mapper;


    public AnnoucementController(ILogger<AnnoucementController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpGet("listannouncement")]
    public async Task<IActionResult> GetAnnouncement(int id, string cp, int diplome)
    {
        List<Annoucement> final_annonce_filter = new List<Annoucement>();

        List<Annoucement> annonce = new List<Annoucement>();

        if(id == 1)
        {
            annonce = await _context.Annoucements.Where(i => i.id_type == 2 || i.id_type == 3).ToListAsync();
        }
        else
        {
            annonce = await _context.Annoucements.Where(i => i.id_type == 1).ToListAsync();
        }


        List<Annoucement> annonce_filter_cp = new List<Annoucement>();

        if(cp != "null")
        {
            // ajout des annonces qui ont les memes codes postaux dans une liste
            foreach(Annoucement annouce in annonce)
            {
                if(annouce.id_user != null)
                {
                    User? user_cp = _context.Users.FirstOrDefault(i => i.id == annouce.id_user);
                    if(user_cp != null)
                    {
                        if(user_cp.cp != "" && user_cp.cp != null)
                        {
                            if(user_cp.cp == cp)
                            {
                                annonce_filter_cp.Add(annouce);
                            }
                        }
                        else
                        {
                            Company? company_cp = _context.Companies.FirstOrDefault(i => i.siren == user_cp.id_company);
                            
                            if(company_cp != null)
                            {
                                if(company_cp.cp == cp)
                                {
                                    annonce_filter_cp.Add(annouce);
                                }
                            }
                        }
                    }
                }
            }

            // verif si déjà dans le tableau
            foreach(Annoucement annouce in annonce)
            {
                if(annouce.id_user != null)
                {
                    User? user_cp = _context.Users.FirstOrDefault(i => i.id == annouce.id_user);
                    if(user_cp != null)
                    {
                        if(user_cp.cp != "" && user_cp.cp != null)
                        {
                            if(user_cp.cp.Substring(0, 2) == cp.Substring(0, 2))
                            {
                                if(!annonce_filter_cp.Contains(annouce))
                                {
                                    annonce_filter_cp.Add(annouce);
                                }
                            }
                        }
                        else
                        {
                            Company? company_cp = _context.Companies.FirstOrDefault(i => i.siren == user_cp.id_company);
                            
                            if(company_cp != null)
                            {
                                if(company_cp.cp != null)
                                {
                                    if(company_cp.cp.Substring(0, 2) == cp.Substring(0, 2))
                                    {
                                        if(!annonce_filter_cp.Contains(annouce))
                                        {
                                            annonce_filter_cp.Add(annouce);
                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                }
            }

            final_annonce_filter = annonce_filter_cp;
        }

        List<Annoucement> annonce_filter_diplome = new List<Annoucement>();

        if(diplome != 0)
        {
            if(final_annonce_filter.Count == 0)
            {
                foreach(Annoucement annoncediplome in annonce)
                {
                    if(annoncediplome.id_diplome == diplome)
                    {
                        annonce_filter_diplome.Add(annoncediplome);
                    }
                }
            }
            else
            {
                foreach(Annoucement annoncediplome in final_annonce_filter)
                {
                    if(annoncediplome.id_diplome == diplome)
                    {
                        annonce_filter_diplome.Add(annoncediplome);
                    }
                }
            }

            final_annonce_filter = annonce_filter_diplome;
        }

        List<Annoucement> testt = _mapper.Map<List<Annoucement>>(final_annonce_filter);

        return Ok(testt);
    }

    [HttpPut("UpdateStatusAnnouncement")]
    public async Task<IActionResult> UpdateStatus(StatusAnnouncementDto stateAnnouncement)
    {

        Annoucement? exist = _context.Annoucements.FirstOrDefault(i => i.id == stateAnnouncement.id_annoucement);
        
        if(exist != null)
        {
            exist.id_status = stateAnnouncement.id_statut;
            _context.Annoucements.Update(exist);
            await _context.SaveChangesAsync();
        }
        

        return Ok("statut de l'annonce modifier");
    }

    [HttpPost("AddAnnouncement")]
    public async Task<IActionResult> AddAnnouncement(int user_id, string title, string description, int type_id, int division_naf_id, int diplome_id, int[] skills)
    {
        Annoucement newannouncement = new Annoucement();
        newannouncement.id_user = user_id;
        newannouncement.title = title;
        newannouncement.description = description;
        newannouncement.publish_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        newannouncement.id_type = type_id;
        newannouncement.id_status = 2;
        newannouncement.id_naf_division = division_naf_id;
        newannouncement.id_diplome = diplome_id;

        await _context.Annoucements.AddAsync(newannouncement);
        await _context.SaveChangesAsync();

        Annoucement? exist = _context.Annoucements.FirstOrDefault(i => i.id_user == user_id && i.title == title && i.description == description && i.id_type == type_id && i.id_naf_division == division_naf_id && i.id_diplome == diplome_id);

        for(int i = 0; i < skills.Length; i++)
        {
            Qualification newqualif = new Qualification();
            newqualif.id_annoucement = exist.id;
            newqualif.id_skill = skills[i];

            await _context.Qualifications.AddAsync(newqualif);
            await _context.SaveChangesAsync();
        }

        return Ok("ok");
    }

}