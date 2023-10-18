
using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Aspose.Cells;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template:"api/[controller]")]
public class AjoutDonneeBddController : ControllerBase
{
    private readonly ApplicationDbContext _context;


    private readonly ILogger<AjoutDonneeBddController> _logger;


    public AjoutDonneeBddController(ILogger<AjoutDonneeBddController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Naf_Section")]
    public async Task<IActionResult> GetAjout_Naf_Section()
    {
        Workbook wb = new Workbook("./DonneeImporter/Naf_Section.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Naf_Section naf = new Naf_Section();
                naf.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                naf.title = Convert.ToString(worksheet.Cells[i, 1].Value);

                await _context.Naf_Sections.AddAsync(naf);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les naf_sections sont bien ajoutés");
    }



    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Naf_Division")]
    public async Task<IActionResult> GetAjout_Naf_Division()
    {
        Workbook wb = new Workbook("./DonneeImporter/Naf_Division.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Naf_Division naf = new Naf_Division();
                naf.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                naf.id_naf_section = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                naf.title = Convert.ToString(worksheet.Cells[i, 2].Value);

                await _context.Naf_Divisions.AddAsync(naf);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les naf_divisons sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Effective")]
    public async Task<IActionResult> GetAjout_Effective()
    {
        Workbook wb = new Workbook("./DonneeImporter/Effectif.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Effective effectif = new Effective();
                effectif.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                effectif.type = Convert.ToString(worksheet.Cells[i, 1].Value);

                await _context.Effectives.AddAsync(effectif);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les effectifs sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Company")]
    public async Task<IActionResult> GetAjout_Company()
    {
        Workbook wb = new Workbook("./DonneeImporter/Company.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Company company = new Company();
                company.siren = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                company.name = Convert.ToString(worksheet.Cells[i, 1].Value);
                company.id_naf = Convert.ToInt32(worksheet.Cells[i, 2].Value);
                company.picture = Convert.ToString(worksheet.Cells[i, 3].Value);
                company.street = Convert.ToString(worksheet.Cells[i, 4].Value);
                company.cp = Convert.ToString(worksheet.Cells[i, 5].Value);
                company.city = Convert.ToString(worksheet.Cells[i, 6].Value);
                company.legal_form = Convert.ToString(worksheet.Cells[i, 7].Value);
                company.id_effective = Convert.ToInt32(worksheet.Cells[i, 8].Value);
                company.web_site = Convert.ToString(worksheet.Cells[i, 9].Value);
                company.payment = Convert.ToBoolean(worksheet.Cells[i, 10].Value);

                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les companies sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Type_User")]
    public async Task<IActionResult> GetAjout_Type_User()
    {
        Workbook wb = new Workbook("./DonneeImporter/Type_User.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Type_User type_user = new Type_User();
                type_user.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                type_user.title = Convert.ToString(worksheet.Cells[i, 1].Value);


                await _context.Type_Users.AddAsync(type_user);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les type users sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_User")]
    public async Task<IActionResult> GetAjout_User()
    {
        Workbook wb = new Workbook("./DonneeImporter/User.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                User user = new User();
                user.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                user.surname = Convert.ToString(worksheet.Cells[i, 1].Value);
                user.firstname = Convert.ToString(worksheet.Cells[i, 2].Value);
                user.email = Convert.ToString(worksheet.Cells[i, 3].Value);
                user.password = Convert.ToString(worksheet.Cells[i, 4].Value);
                user.picture = Convert.ToString(worksheet.Cells[i, 5].Value);
                user.is_online = Convert.ToBoolean(worksheet.Cells[i, 6].Value);
                user.id_type_user = Convert.ToInt32(worksheet.Cells[i, 7].Value);
                user.description = Convert.ToString(worksheet.Cells[i, 8].Value);
                user.web_site = Convert.ToString(worksheet.Cells[i, 9].Value);
                user.cv = Convert.ToString(worksheet.Cells[i, 10].Value);
                user.cp = Convert.ToString(worksheet.Cells[i, 11].Value);
                user.city = Convert.ToString(worksheet.Cells[i, 12].Value);
                user.birthday = Convert.ToDateTime(worksheet.Cells[i, 13].Value);
                user.is_conveyed = Convert.ToBoolean(worksheet.Cells[i, 14].Value);
                if(Convert.ToInt32(worksheet.Cells[i, 15].Value) != 0)
                {
                    user.id_company = Convert.ToInt32(worksheet.Cells[i, 15].Value);
                }
                


                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les users sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Skill")]
    public async Task<IActionResult> GetAjout_Skill()
    {
        Workbook wb = new Workbook("./DonneeImporter/Skill.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Skill skill = new Skill();
                skill.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                skill.title = Convert.ToString(worksheet.Cells[i, 1].Value);

                await _context.Skills.AddAsync(skill);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les skills sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Student_Skill")]
    public async Task<IActionResult> GetAjout_Student_Skill()
    {
        Workbook wb = new Workbook("./DonneeImporter/StudentSkills.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Student_Skill studentskill = new Student_Skill();
                studentskill.id_user = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                studentskill.id_skill = Convert.ToInt32(worksheet.Cells[i, 1].Value);

                await _context.Student_Skills.AddAsync(studentskill);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les student_skill sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Status")]
    public async Task<IActionResult> GetAjout_Status()
    {
        Workbook wb = new Workbook("./DonneeImporter/Status.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Annoucement_State status = new Annoucement_State();
                status.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                status.status = Convert.ToString(worksheet.Cells[i, 1].Value);

                await _context.Annoucement_Status.AddAsync(status);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les status sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Diplome")]
    public async Task<IActionResult> GetAjout_Diplome()
    {
        Workbook wb = new Workbook("./DonneeImporter/Diplome.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Diplome diplome = new Diplome();
                diplome.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                diplome.diplome = Convert.ToString(worksheet.Cells[i, 1].Value);

                await _context.Diplomes.AddAsync(diplome);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les diplômes sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Annonce")]
    public async Task<IActionResult> GetAjout_Annonce()
    {
        Workbook wb = new Workbook("./DonneeImporter/Annonce.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Annoucement annonce = new Annoucement();
                annonce.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                annonce.id_user = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                annonce.title = Convert.ToString(worksheet.Cells[i, 2].Value);
                annonce.description = Convert.ToString(worksheet.Cells[i, 3].Value);
                annonce.publish_date = Convert.ToDateTime(worksheet.Cells[i, 4].Value);
                annonce.id_type = Convert.ToInt32(worksheet.Cells[i, 5].Value);
                annonce.id_status = Convert.ToInt32(worksheet.Cells[i, 6].Value);
                annonce.id_naf_division = Convert.ToInt32(worksheet.Cells[i, 7].Value);

                annonce.id_diplome = Convert.ToInt32(worksheet.Cells[i, 8].Value);

                await _context.Annoucements.AddAsync(annonce);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les annonces sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Qualification")]
    public async Task<IActionResult> GetAjout_Qualification()
    {
        Workbook wb = new Workbook("./DonneeImporter/Qualification.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Qualification qualif = new Qualification();
                qualif.id_annoucement = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                qualif.id_skill = Convert.ToInt32(worksheet.Cells[i, 1].Value);

                await _context.Qualifications.AddAsync(qualif);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les qualifs sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Favorite")]
    public async Task<IActionResult> GetAjout_Favorite()
    {
        Workbook wb = new Workbook("./DonneeImporter/Favorite.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Favorite favoris = new Favorite();
                favoris.id_user = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                favoris.id_annoucement = Convert.ToInt32(worksheet.Cells[i, 1].Value);

                await _context.Favorites.AddAsync(favoris);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les favoris sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Recent")]
    public async Task<IActionResult> GetAjout_Recent()
    {
        Workbook wb = new Workbook("./DonneeImporter/Recent.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Recent recent = new Recent();
                recent.id_user = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                recent.id_annoucement = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                recent.consult_date = Convert.ToDateTime(worksheet.Cells[i, 2].Value);

                await _context.Recents.AddAsync(recent);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les recents sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Chat_Status")]
    public async Task<IActionResult> GetAjout_Chat_Status()
    {
        Workbook wb = new Workbook("./DonneeImporter/chat_status.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Chat_State chat_status = new Chat_State();
                chat_status.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                chat_status.status = Convert.ToString(worksheet.Cells[i, 1].Value);

                await _context.Chat_Status.AddAsync(chat_status);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les chat_status sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Chat")]
    public async Task<IActionResult> GetAjout_Chat()
    {
        Workbook wb = new Workbook("./DonneeImporter/chat.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Chat chat = new Chat();
                chat.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                chat.id_sender = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                chat.id_recipient = Convert.ToInt32(worksheet.Cells[i, 2].Value);
                chat.content = Convert.ToString(worksheet.Cells[i, 3].Value);
                chat.send_time = Convert.ToDateTime(worksheet.Cells[i, 4].Value);
                chat.status = Convert.ToInt32(worksheet.Cells[i, 5].Value);

                await _context.Chats.AddAsync(chat);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les chat_status sont bien ajoutés");
    }

    [ApiExplorerSettings(IgnoreApi=true)]
    [HttpGet("Ajout_Recent_Search")]
    public async Task<IActionResult> GetAjout_Recent_Search()
    {
        Workbook wb = new Workbook("./DonneeImporter/recent_search.xls");

        WorksheetCollection collection = wb.Worksheets;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
            Worksheet worksheet = collection[worksheetIndex];

            int rows = worksheet.Cells.MaxDataRow;
            int cols = worksheet.Cells.MaxDataColumn;

            for (int i = 0; i <= rows; i++)
            {
                Recent_Search recent_search = new Recent_Search();
                recent_search.id = Convert.ToInt32(worksheet.Cells[i, 0].Value);
                recent_search.id_user = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                recent_search.text = Convert.ToString(worksheet.Cells[i, 2].Value);

                await _context.Recents_Searches.AddAsync(recent_search);
                await _context.SaveChangesAsync();
            }
        }

        return Ok("Les chat_status sont bien ajoutés");
    }

    [HttpGet("Ajouter")]
    public async Task<IActionResult> GetAjouter()
    {
        List<string> ajoutBDD = new List<string>{
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Naf_Section",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Naf_Division",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Effective",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Company",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Type_User",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_User",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Skill",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Student_Skill",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Status",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Diplome",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Annonce",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Qualification",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Favorite",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Recent",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Chat_Status",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Chat",
            "https://localhost:7061/api/AjoutDonneeBdd/Ajout_Recent_Search"
        };

        using (var httpClient = new HttpClient())
        {
            foreach(string add in ajoutBDD)
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), add))  
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*"); 

                    var response = await httpClient.SendAsync(request);
                }
            }
        }

        return Ok("Toutes les données sont bien ajoutées");
    }

    [HttpGet("Supprimer")]
    public async Task<IActionResult> GetSupprimer()
    {
        _context.Recents_Searches.RemoveRange(_context.Recents_Searches);
        _context.Chats.RemoveRange(_context.Chats);
        _context.Chat_Status.RemoveRange(_context.Chat_Status);
        _context.Recents.RemoveRange(_context.Recents);
        _context.Favorites.RemoveRange(_context.Favorites);
        _context.Qualifications.RemoveRange(_context.Qualifications);
        _context.Annoucements.RemoveRange(_context.Annoucements);
        _context.Diplomes.RemoveRange(_context.Diplomes);
        _context.Annoucement_Status.RemoveRange(_context.Annoucement_Status);
        _context.Student_Skills.RemoveRange(_context.Student_Skills);
        _context.Skills.RemoveRange(_context.Skills);
        _context.Users.RemoveRange(_context.Users);
        _context.Type_Users.RemoveRange(_context.Type_Users);
        _context.Companies.RemoveRange(_context.Companies);
        _context.Effectives.RemoveRange(_context.Effectives);
        _context.Naf_Divisions.RemoveRange(_context.Naf_Divisions);
        _context.Naf_Sections.RemoveRange(_context.Naf_Sections);
        _context.SaveChanges();

        return Ok("Toutes les données sont bien supprimées");
    }
}