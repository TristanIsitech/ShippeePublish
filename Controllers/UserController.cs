using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;


    public UserController(ILogger<UserController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }



    [HttpPost("connect")]
    public async Task<IActionResult> GetStudentByMailPassword([FromBody] UserConnectDto userco)
    {
        // Cherche si un user existe avec l'email et le mdp renseigner
        User? personne = _context.Users.FirstOrDefault(i => i.email == userco.id && i.password == userco.password);

        // si il n'y a aucun user 
        if (personne == null)
        {
            // on regarde si il existe un user avec le mail
            User? testemail = _context.Users.FirstOrDefault(i => i.email == userco.id);

            // on regarde si il existe un user avec le mail
            User? testpassword = _context.Users.FirstOrDefault(i => i.password == userco.password);

            Dictionary<string, string> erreur = new Dictionary<string, string>();
            erreur.Add("connexion", "false");

            // si il y a pas d'user avec se mail
            if (testemail == null)
            {
                // erreur mail
                erreur.Add("erreur", "Cette adresse mail n'existe pas !");
            }
            else
            {
                // si il existe un user avec ce mail on verifie le mdp
                if (testpassword == null)
                {
                    // si y en a pas erreur mdp
                    erreur.Add("erreur", "Ce mot de passe ne correspond pas à l'adresse mail saisie !");
                }
            }

            // renvoie l'erreur
            return Ok(erreur);
        }

        // on initialise un user filtrer sur etudiant
        StudentDto student = new StudentDto();

        // on initialise un user filtrer sur recruteur
        RecruiterDto recruiter = new RecruiterDto();

        if (personne.id_type_user == 1)
        {
            //on met dans le user filtrer les donées que l'on veut recup
            student.connexion = true;
            student.id = personne.id;
            student.surname = personne.surname;
            student.firstname = personne.firstname;
            student.email = personne.email;
            student.picture = personne.picture;
            student.is_online = personne.is_online;

            Type_User? type_user = _context.Type_Users.FirstOrDefault(i => i.id == personne.id_type_user);
            student.type_user = type_user;

            student.description = personne.description;
            student.web_site = personne.web_site;
            student.cv = personne.cv;
            student.cp = personne.cp;
            student.city = personne.city;
            student.birthday = personne.birthday;
            student.is_conveyed = personne.is_conveyed;

            // on recup donnée de la relation many_to_many selon l'id de l'user trouvé
            var users = _context.Users
                .Include(x => x.skills)
                .Where(x => x.id == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json = System.Text.Json.JsonSerializer.Serialize(users, options);
            var jsonDoc = JsonDocument.Parse(json);
            var root = jsonDoc.RootElement;

            foreach (var user in root.EnumerateArray())
            {
                var valRecup = System.Text.Json.JsonSerializer.Deserialize<User>(user);

                if (valRecup != null)
                {
                    // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                    Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                    if (valRecup.skills != null)
                    {
                        foreach (Student_Skill skill in valRecup.skills)
                        {
                            if (skill != null)
                            {
                                Skill? skillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                if (skillss != null)
                                {
                                    if (skillss.title != null)
                                    {
                                        skillDico.Add(skillss.id, skillss.title);
                                    }
                                }
                            }
                        }
                    }
                    student.skills = skillDico;
                }
            }

            List<Annoucement>? annonce = await _context.Annoucements.Where(a => a.id_user == personne.id).ToListAsync();
            List<AnnoucementStudentDto> annonceDto = _mapper.Map<List<AnnoucementStudentDto>>(annonce);

            foreach (Annoucement pseudoannonce in annonce)
            {
                Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == pseudoannonce.id_status);
                Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == pseudoannonce.id_naf_division);
                Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == pseudoannonce.id_diplome);

                foreach (AnnoucementStudentDto dtoAnn in annonceDto)
                {
                    if (dtoAnn.id == pseudoannonce.id)
                    {
                        dtoAnn.status = state;
                        if (naf_div != null)
                        {
                            dtoAnn.naf_division_title = naf_div.title;
                        }

                        if(diplome != null)
                        {
                            dtoAnn.diplome = diplome.diplome;
                        }
                    }
                }
            }

            student.annoucements = annonceDto;

            var favoris = _context.Users
                .Include(x => x.favorites_annoucements)
                .Where(x => x.id == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var options2 = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json2 = System.Text.Json.JsonSerializer.Serialize(favoris, options2);
            var jsonDoc2 = JsonDocument.Parse(json2);
            var root2 = jsonDoc2.RootElement;

            foreach (var user in root2.EnumerateArray())
            {
                var valRecup = System.Text.Json.JsonSerializer.Deserialize<User>(user);

                if (valRecup != null)
                {
                    // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                    List<AnnoucementFavoriteRecruiterDto> favorieDico = new List<AnnoucementFavoriteRecruiterDto>();

                    if (valRecup.favorites_annoucements != null)
                    {
                        foreach (Favorite favorie in valRecup.favorites_annoucements)
                        {
                            if (favorie != null)
                            {
                                Annoucement? annoucement = _context.Annoucements.FirstOrDefault(a => a.id == favorie.id_annoucement);
                                if (annoucement != null)
                                {
                                    AnnoucementFavoriteRecruiterDto? annoncefavorie = _mapper.Map<AnnoucementFavoriteRecruiterDto>(annoucement);

                                    Annoucement? theAnnonce = _context.Annoucements.FirstOrDefault(a => a.id == favorie.id_annoucement);
                                    if (theAnnonce != null)
                                    {
                                        User? recruteur = _context.Users.FirstOrDefault(u => u.id == theAnnonce.id_user);
                                        RecruiterFavoriteDto? userRecruteur = _mapper.Map<RecruiterFavoriteDto>(recruteur);

                                        if (recruteur != null)
                                        {
                                            Company? company = _context.Companies.FirstOrDefault(i => i.siren == recruteur.id_company);
                                            CompanyDto? companyDto = _mapper.Map<CompanyDto>(company);
                                            userRecruteur.company = companyDto;
                                        }
                                        annoncefavorie.user = userRecruteur;
                                        annoncefavorie.favorite = true;

                                        Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == theAnnonce.id_status);
                                        annoncefavorie.status = state;

                                        Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == theAnnonce.id_naf_division);
                                        if (naf_div != null)
                                        {
                                            annoncefavorie.naf_division_title = naf_div.title;
                                        }

                                        Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == theAnnonce.id_diplome);
                                        if(diplome != null)
                                        {
                                            annoncefavorie.diplome = diplome.diplome;
                                        }   

                                        var qualification = _context.Annoucements
                                            .Include(x => x.skills)
                                                .ThenInclude(x => x.Skill)
                                            .Where(x => x.id_user == theAnnonce.id_user && x.id == theAnnonce.id)
                                            .ToList();

                                        // format json car sinon impossible a lire les donénes
                                        var options3 = new JsonSerializerOptions
                                        {
                                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                                        };

                                        var json3 = System.Text.Json.JsonSerializer.Serialize(qualification, options3);
                                        var jsonDoc3 = JsonDocument.Parse(json3);
                                        var root3 = jsonDoc3.RootElement;

                                        foreach (var user2 in root3.EnumerateArray())
                                        {
                                            var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user2);

                                            if (valRecup2 != null)
                                            {
                                                // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                                                Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                                                if (valRecup2.skills != null)
                                                {
                                                    foreach (Qualification skill in valRecup2.skills)
                                                    {
                                                        Skill? sskillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                                        if (sskillss != null)
                                                        {
                                                            if (sskillss.title != null)
                                                            {
                                                                skillDico.Add(sskillss.id, sskillss.title);
                                                            }
                                                        }
                                                    }
                                                }
                                                annoncefavorie.qualifications = skillDico;
                                            }
                                        }
                                    }
                                    favorieDico.Add(annoncefavorie);
                                }
                            }
                        }
                    }
                    student.favorites = favorieDico;
                }
            }

            var recents = _context.Users
                .Include(x => x.recents_annoucements)
                .Where(x => x.id == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var options4 = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json4 = System.Text.Json.JsonSerializer.Serialize(recents, options4);
            var jsonDoc4 = JsonDocument.Parse(json4);
            var root4 = jsonDoc4.RootElement;

            foreach (var user2 in root4.EnumerateArray())
            {
                var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<User>(user2);

                if (valRecup2 != null)
                {
                    if (valRecup2.recents_annoucements != null)
                    {
                        List<Recent> valRecup = new List<Recent>();

                        valRecup2.recents_annoucements.Sort(delegate (Recent x, Recent y)
                        {
                            return x.consult_date.CompareTo(y.consult_date);
                        });
                        valRecup = valRecup2.recents_annoucements;
                        valRecup.Reverse();

                        List<AnnoucementRecentStudentDto> recentAnnoucement = new List<AnnoucementRecentStudentDto>();

                        var numtest = 0;

                        foreach (Recent recentsAnnonce in valRecup)
                        {
                            numtest++;
                            if (numtest <= 5)
                            {
                                Annoucement? annoucement = _context.Annoucements.FirstOrDefault(a => a.id == recentsAnnonce.id_annoucement);

                                if (annoucement != null)
                                {
                                    AnnoucementRecentStudentDto? annoncerecent = _mapper.Map<AnnoucementRecentStudentDto>(annoucement);

                                    if (student.favorites != null)
                                    {
                                        foreach (AnnoucementFavoriteRecruiterDto annonecfav in student.favorites)
                                        {
                                            if (annoncerecent.id == annonecfav.id)
                                            {
                                                annoncerecent.favorite = true;
                                                break;
                                            }
                                            else
                                            {
                                                annoncerecent.favorite = false;
                                            }
                                        }
                                    }

                                    User? recruteur = _context.Users.FirstOrDefault(u => u.id == annoucement.id_user);
                                    RecruiterFavoriteDto? userRecruteur = _mapper.Map<RecruiterFavoriteDto>(recruteur);

                                    if (recruteur != null)
                                    {
                                        Company? company = _context.Companies.FirstOrDefault(i => i.siren == recruteur.id_company);
                                        CompanyDto? companyDto = _mapper.Map<CompanyDto>(company);
                                        userRecruteur.company = companyDto;
                                    }
                                    annoncerecent.user = userRecruteur;

                                    Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == annoucement.id_status);
                                    annoncerecent.status = state;

                                    Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == annoucement.id_naf_division);
                                    if (naf_div != null)
                                    {
                                        annoncerecent.naf_division_title = naf_div.title;
                                    }

                                    Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == annoucement.id_diplome);
                                    if(diplome != null)
                                    {
                                        annoncerecent.diplome = diplome.diplome;
                                    }  

                                    var qualification = _context.Annoucements
                                        .Include(x => x.skills)
                                            .ThenInclude(x => x.Skill)
                                        .Where(x => x.id_user == annoucement.id_user && x.id == annoucement.id)
                                        .ToList();

                                    // format json car sinon impossible a lire les donénes
                                    var options3 = new JsonSerializerOptions
                                    {
                                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                                    };

                                    var json3 = System.Text.Json.JsonSerializer.Serialize(qualification, options3);
                                    var jsonDoc3 = JsonDocument.Parse(json3);
                                    var root3 = jsonDoc3.RootElement;

                                    foreach (var user3 in root3.EnumerateArray())
                                    {
                                        var valRecup3 = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user3);

                                        if (valRecup3 != null)
                                        {
                                            // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                                            Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                                            if (valRecup3.skills != null)
                                            {
                                                foreach (Qualification skill in valRecup3.skills)
                                                {
                                                    Skill? sskillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                                    if (sskillss != null)
                                                    {
                                                        if (sskillss.title != null)
                                                        {
                                                            skillDico.Add(sskillss.id, sskillss.title);
                                                        }
                                                    }
                                                }
                                            }
                                            annoncerecent.qualifications = skillDico;
                                        }
                                    }

                                    recentAnnoucement.Add(annoncerecent);
                                }
                            }
                        }

                        student.recent_announcements = recentAnnoucement;

                    }
                }
            }

            var chats = _context.Chats
                .Where(x => x.id_sender == personne.id || x.id_recipient == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var chat_json = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json5 = System.Text.Json.JsonSerializer.Serialize(chats, chat_json);
            var jsonDoc5 = JsonDocument.Parse(json5);
            var all_chat = jsonDoc5.RootElement;

            List<Int32> lesId = new List<Int32>();

            List<ProfileChatDto> lesChats = new List<ProfileChatDto>();

            foreach (var chat in all_chat.EnumerateArray())
            {
                var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Chat>(chat);

                if (valRecup2 != null)
                {
                    if (valRecup2.id_recipient == personne.id)
                    {
                        if (valRecup2.id_sender != null)
                        {
                            if (lesId.Contains((Int32)valRecup2.id_sender) == false)
                            {
                                lesId.Add((Int32)valRecup2.id_sender);
                            }
                        }
                    }

                    if (valRecup2.id_sender == personne.id)
                    {
                        if (valRecup2.id_recipient != null)
                        {
                            if (lesId.Contains((Int32)valRecup2.id_recipient) == false)
                            {
                                lesId.Add((Int32)valRecup2.id_recipient);
                            }
                        }
                    }
                }
            }

            foreach (Int32 nb in lesId)
            {
                ProfileChatDto profilechatdto = new ProfileChatDto();

                User? pers = _context.Users.FirstOrDefault(i => i.id == nb);

                if (pers != null)
                {
                    profilechatdto.id_people = pers.id;
                    profilechatdto.user_firstname = pers.firstname;
                    profilechatdto.user_surname = pers.surname;
                    profilechatdto.user_picture = pers.picture;
                    profilechatdto.user_is_online = pers.is_online;
                }

                List<ChatDto> listchatdto = new List<ChatDto>();

                foreach (var chat in all_chat.EnumerateArray())
                {
                    var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Chat>(chat);

                    if (valRecup2 != null)
                    {
                        if (valRecup2.id_recipient == nb || valRecup2.id_sender == nb)
                        {
                            ChatDto thechatdto = new ChatDto();
                            thechatdto.id = valRecup2.id;
                            thechatdto.content = valRecup2.content;
                            thechatdto.send_time = valRecup2.send_time;
                            Chat_State? statu = _context.Chat_Status.FirstOrDefault(i => i.id == valRecup2.status);
                            if (statu != null)
                            {
                                thechatdto.status = statu.status;
                            }

                            if (valRecup2.id_recipient == personne.id)
                            {
                                thechatdto.who = false;
                            }

                            if (valRecup2.id_sender == personne.id)
                            {
                                thechatdto.who = true;
                            }

                            listchatdto.Add(thechatdto);
                        }
                    }
                }
                profilechatdto.chat = listchatdto;
                lesChats.Add(profilechatdto);
            }

            student.convs = lesChats;

            List<Recent_Search>? recent_search = await _context.Recents_Searches.Where(a => a.id_user == personne.id).ToListAsync();
            List<Recent_SearchDto> recent_searchDto = _mapper.Map<List<Recent_SearchDto>>(recent_search);

            student.recent_searchs = recent_searchDto;

            List<Annoucement>? test = await _context.Annoucements.Where(f => f.id_type == 2).ToListAsync();

            var random = new System.Random();
            test.Sort((x, y) => random.Next(-1, 2));

            List<AnnoucementRecentStudentDto> selectqualif = new List<AnnoucementRecentStudentDto>();

            foreach (Annoucement add in test)
            {
                AnnoucementRecentStudentDto testt = _mapper.Map<AnnoucementRecentStudentDto>(add);
                
                Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == add.id_status);
                testt.status = state;

                Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == add.id_naf_division);
                if (naf_div != null)
                {
                    testt.naf_division_title = naf_div.title;
                }

                Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == add.id_diplome);
                if(diplome != null)
                {
                    testt.diplome = diplome.diplome;
                }  

                var qualification = _context.Annoucements
                    .Include(x => x.skills)
                        .ThenInclude(x => x.Skill)
                    .Where(x => x.id == add.id)
                    .ToList();

                // format json car sinon impossible a lire les donénes
                var options3 = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };

                var json3 = System.Text.Json.JsonSerializer.Serialize(qualification, options3);
                var jsonDoc3 = JsonDocument.Parse(json3);
                var root3 = jsonDoc3.RootElement;

                foreach (var user2 in root3.EnumerateArray())
                {
                    var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user2);

                    if (valRecup2 != null)
                    {
                        // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                        Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                        if (valRecup2.skills != null)
                        {
                            foreach (Qualification skill in valRecup2.skills)
                            {
                                Skill? sskillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                if (sskillss != null)
                                {
                                    if (sskillss.title != null)
                                    {
                                        skillDico.Add(sskillss.id, sskillss.title);
                                    }
                                }


                            }
                        }
                        testt.qualifications = skillDico;

                        // return Ok(student.skills);
                        var compte = 0;
                        foreach (var skil in skillDico)
                        {
                            if (student.skills != null)
                            {
                                foreach (var skiluser in student.skills)
                                {
                                    if (skiluser.Value == skil.Value)
                                    {
                                        compte++;
                                    }
                                }
                            }
                        }
                        if (compte > 0)
                        {
                            selectqualif.Add(testt);
                        }
                    }
                }
            }

            List<AnnoucementRecentStudentDto> selectlocqualif = new List<AnnoucementRecentStudentDto>();

            foreach (AnnoucementRecentStudentDto annod in selectqualif)
            {
                if (selectlocqualif.Count < 10)
                {
                    if (annod.user != null)
                    {
                        if (annod.user.company != null)
                        {
                            if (annod.user.company.cp != null)
                            {
                                if (student.cp != null)
                                {
                                    if (annod.user.company.cp.Substring(0, 2) == student.cp.Substring(0, 2))
                                    {
                                        selectlocqualif.Add(annod);
                                    }
                                }

                            }

                        }
                    }
                }
            }

            student.select_announcements = selectlocqualif;

            List<AnnoucementRecentStudentDto> justLocalisation = new List<AnnoucementRecentStudentDto>();

            foreach (Annoucement add in test)
            {
                AnnoucementRecentStudentDto testt = _mapper.Map<AnnoucementRecentStudentDto>(add);

                if (selectlocqualif.Count < 10)
                {
                    if (testt.user != null)
                    {
                        if (testt.user.company != null)
                        {
                            if (testt.user.company.cp != null)
                            {
                                if (student.cp != null)
                                {
                                    if (testt.user.company.cp.Substring(0, 2) == student.cp.Substring(0, 2))
                                    {
                                        Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == add.id_status);
                                        testt.status = state;

                                        Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == add.id_naf_division);
                                        if (naf_div != null)
                                        {
                                            testt.naf_division_title = naf_div.title;
                                        }

                                        Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == add.id_diplome);
                                        if(diplome != null)
                                        {
                                            testt.diplome = diplome.diplome;
                                        } 

                                        var qualification = _context.Annoucements
                                            .Include(x => x.skills)
                                                .ThenInclude(x => x.Skill)
                                            .Where(x => x.id == add.id)
                                            .ToList();

                                        // format json car sinon impossible a lire les donénes
                                        var options3 = new JsonSerializerOptions
                                        {
                                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                                        };

                                        var json3 = System.Text.Json.JsonSerializer.Serialize(qualification, options3);
                                        var jsonDoc3 = JsonDocument.Parse(json3);
                                        var root3 = jsonDoc3.RootElement;

                                        foreach (var user2 in root3.EnumerateArray())
                                        {
                                            var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user2);

                                            if (valRecup2 != null)
                                            {
                                                // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                                                Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                                                if (valRecup2.skills != null)
                                                {
                                                    foreach (Qualification skill in valRecup2.skills)
                                                    {
                                                        Skill? sskillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                                        if (sskillss != null)
                                                        {
                                                            if (sskillss.title != null)
                                                            {
                                                                skillDico.Add(sskillss.id, sskillss.title);
                                                            }
                                                        }


                                                    }
                                                }
                                                testt.qualifications = skillDico;

                                                // return Ok(student.skills);
                                                var compte = 0;
                                                foreach (var skil in skillDico)
                                                {
                                                    if (student.skills != null)
                                                    {
                                                        foreach (var skiluser in student.skills)
                                                        {
                                                            if (skiluser.Value == skil.Value)
                                                            {
                                                                compte++;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (compte > 0)
                                                {
                                                    selectqualif.Add(testt);
                                                }
                                            }
                                        }

                                        justLocalisation.Add(testt);
                                    }
                                }

                            }

                        }
                    }
                }
            }

            student.loc_announcements = justLocalisation;

            return Ok(student);
        }
        else
        {
            //on met dans le user filtrer les donées que l'on veut recup
            recruiter.connexion = true;
            recruiter.id = personne.id;
            recruiter.surname = personne.surname;
            recruiter.firstname = personne.firstname;
            recruiter.email = personne.email;
            recruiter.picture = personne.picture;
            recruiter.is_online = personne.is_online;

            Type_User? type_user = _context.Type_Users.FirstOrDefault(i => i.id == personne.id_type_user);
            recruiter.type_user = type_user;

            Company? company = _context.Companies.FirstOrDefault(i => i.siren == personne.id_company);
            CompanyDto? companyDto = _mapper.Map<CompanyDto>(company);
            recruiter.company = companyDto;

            // Annoucement Recruiter
            List<Annoucement>? annonce = await _context.Annoucements.Where(a => a.id_user == personne.id).ToListAsync();
            List<AnnoucementRecruiterDto> annonceDto = _mapper.Map<List<AnnoucementRecruiterDto>>(annonce);

            foreach (Annoucement pseudoannonce in annonce)
            {
                Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == pseudoannonce.id_status);
                Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == pseudoannonce.id_naf_division);
                Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == pseudoannonce.id_diplome);

                foreach (AnnoucementRecruiterDto dtoAnn in annonceDto)
                {
                    if (dtoAnn.id == pseudoannonce.id)
                    {
                        dtoAnn.status = state;
                        if (naf_div != null)
                        {
                            dtoAnn.naf_division_title = naf_div.title;
                        }
                        
                        if(diplome != null)
                        {
                            dtoAnn.diplome = diplome.diplome;
                        } 

                        var qualification = _context.Annoucements
                            .Include(x => x.skills)
                                .ThenInclude(x => x.Skill)
                            .Where(x => x.id_user == personne.id && x.id == pseudoannonce.id)
                            .ToList();

                        // format json car sinon impossible a lire les donénes
                        var options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                        };

                        var json = System.Text.Json.JsonSerializer.Serialize(qualification, options);
                        var jsonDoc = JsonDocument.Parse(json);
                        var root = jsonDoc.RootElement;

                        foreach (var user in root.EnumerateArray())
                        {
                            var valRecup = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user);

                            if (valRecup != null)
                            {
                                // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"
                                Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                                if (valRecup.skills != null)
                                {
                                    foreach (Qualification skill in valRecup.skills)
                                    {
                                        if (skill.Skill != null)
                                        {
                                            skillDico.Add(skill.Skill.id, skill.Skill.title!);
                                        }
                                    }
                                }

                                dtoAnn.qualifications = skillDico;
                            }
                        }
                    }
                }
            }

            recruiter.annoucements = annonceDto;

            var favoris = _context.Users
                .Include(x => x.favorites_annoucements)
                .Where(x => x.id == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var options2 = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json2 = System.Text.Json.JsonSerializer.Serialize(favoris, options2);
            var jsonDoc2 = JsonDocument.Parse(json2);
            var root2 = jsonDoc2.RootElement;

            foreach (var favorite in root2.EnumerateArray())
            {
                var valRecup = System.Text.Json.JsonSerializer.Deserialize<User>(favorite);

                if (valRecup != null)
                {
                    if (valRecup.favorites_annoucements != null)
                    {
                        List<AnnoucementFavoriteStudentDto> listefavorie = new List<AnnoucementFavoriteStudentDto>();
                        foreach (Favorite fav in valRecup.favorites_annoucements)
                        {
                            Annoucement? annoucement = _context.Annoucements.FirstOrDefault(a => a.id == fav.id_annoucement);
                            AnnoucementFavoriteStudentDto? annoncefavorie = _mapper.Map<AnnoucementFavoriteStudentDto>(annoucement);

                            if (annoucement != null)
                            {
                                User? user = _context.Users.FirstOrDefault(a => a.id == annoucement.id_user);
                                StudentFavoriteDto? userdto = _mapper.Map<StudentFavoriteDto>(user);
                                annoncefavorie.user = userdto;

                                Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == annoucement.id_status);
                                annoncefavorie.status = state;

                                Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == annoucement.id_naf_division);
                                if (naf_div != null)
                                {
                                    annoncefavorie.naf_division_title = naf_div.title;
                                }

                                Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == annoucement.id_diplome);
                                if(diplome != null)
                                {
                                    annoncefavorie.diplome = diplome.diplome;
                                } 
                            }

                            listefavorie.Add(annoncefavorie);

                        }
                        recruiter.favorites = listefavorie;
                    }
                }
            }

            var recents = _context.Users
                .Include(x => x.recents_annoucements)
                .Where(x => x.id == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var options4 = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json4 = System.Text.Json.JsonSerializer.Serialize(recents, options4);
            var jsonDoc4 = JsonDocument.Parse(json4);
            var root4 = jsonDoc4.RootElement;

            foreach (var user2 in root4.EnumerateArray())
            {
                var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<User>(user2);

                if (valRecup2 != null)
                {
                    if (valRecup2.recents_annoucements != null)
                    {
                        List<AnnoucementRecentRecruiterDto> recentAnnoucement = new List<AnnoucementRecentRecruiterDto>();

                        List<Recent> valRecup = new List<Recent>();

                        valRecup2.recents_annoucements.Sort(delegate (Recent x, Recent y)
                        {
                            return x.consult_date.CompareTo(y.consult_date);
                        });
                        valRecup = valRecup2.recents_annoucements;
                        valRecup.Reverse();

                        var numtest = 0;

                        foreach (Recent annonce_recent in valRecup)
                        {
                            numtest++;

                            if (numtest <= 5)
                            {
                                Annoucement? annoucement = _context.Annoucements.FirstOrDefault(a => a.id == annonce_recent.id_annoucement);

                                if (annoucement != null)
                                {
                                    AnnoucementRecentRecruiterDto? annoncerecent = _mapper.Map<AnnoucementRecentRecruiterDto>(annoucement);

                                    if (recruiter.favorites != null)
                                    {
                                        foreach (AnnoucementFavoriteStudentDto annonecfav in recruiter.favorites)
                                        {
                                            if (annoncerecent.id == annonecfav.id)
                                            {
                                                annoncerecent.favorite = true;
                                                break;
                                            }
                                            else
                                            {
                                                annoncerecent.favorite = false;
                                            }
                                        }
                                    }

                                    Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == annoucement.id_status);
                                    annoncerecent.status = state;

                                    Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == annoucement.id_naf_division);
                                    if (naf_div != null)
                                    {
                                        annoncerecent.naf_division_title = naf_div.title;
                                    }

                                    Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == annoucement.id_diplome);
                                    if(diplome != null)
                                    {
                                        annoncerecent.diplome = diplome.diplome;
                                    } 

                                    recentAnnoucement.Add(annoncerecent);
                                }
                            }
                        }
                        recruiter.recent_announcements = recentAnnoucement;
                    }

                }


            }

            var chats = _context.Chats
                .Where(x => x.id_sender == personne.id || x.id_recipient == personne.id)
                .ToList();

            // format json car sinon impossible a lire les donénes
            var chat_json = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var json5 = System.Text.Json.JsonSerializer.Serialize(chats, chat_json);
            var jsonDoc5 = JsonDocument.Parse(json5);
            var all_chat = jsonDoc5.RootElement;

            List<Int32> lesId = new List<Int32>();

            List<ProfileChatDto> lesChats = new List<ProfileChatDto>();

            foreach (var chat in all_chat.EnumerateArray())
            {
                var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Chat>(chat);

                if (valRecup2 != null)
                {
                    if (valRecup2.id_recipient == personne.id)
                    {
                        if (valRecup2.id_sender != null)
                        {
                            if (lesId.Contains((Int32)valRecup2.id_sender) == false)
                            {
                                lesId.Add((Int32)valRecup2.id_sender);
                            }
                        }
                    }

                    if (valRecup2.id_sender == personne.id)
                    {
                        if (valRecup2.id_recipient != null)
                        {
                            if (lesId.Contains((Int32)valRecup2.id_recipient) == false)
                            {
                                lesId.Add((Int32)valRecup2.id_recipient);
                            }
                        }
                    }
                }
            }

            foreach (Int32 nb in lesId)
            {
                ProfileChatDto profilechatdto = new ProfileChatDto();

                User? pers = _context.Users.FirstOrDefault(i => i.id == nb);

                if (pers != null)
                {
                    profilechatdto.id_people = pers.id;
                    profilechatdto.user_firstname = pers.firstname;
                    profilechatdto.user_surname = pers.surname;
                    profilechatdto.user_picture = pers.picture;
                    profilechatdto.user_is_online = pers.is_online;
                }

                List<ChatDto> listchatdto = new List<ChatDto>();

                foreach (var chat in all_chat.EnumerateArray())
                {
                    var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Chat>(chat);

                    if (valRecup2 != null)
                    {
                        if (valRecup2.id_recipient == nb || valRecup2.id_sender == nb)
                        {
                            ChatDto thechatdto = new ChatDto();
                            thechatdto.id = valRecup2.id;
                            thechatdto.content = valRecup2.content;
                            thechatdto.send_time = valRecup2.send_time;
                            Chat_State? statu = _context.Chat_Status.FirstOrDefault(i => i.id == valRecup2.status);
                            if (statu != null)
                            {
                                thechatdto.status = statu.status;
                            }

                            if (valRecup2.id_recipient == personne.id)
                            {
                                thechatdto.who = false;
                            }

                            if (valRecup2.id_sender == personne.id)
                            {
                                thechatdto.who = true;
                            }

                            listchatdto.Add(thechatdto);
                        }
                    }
                }
                profilechatdto.chat = listchatdto;
                lesChats.Add(profilechatdto);
            }

            recruiter.convs = lesChats;

            List<Recent_Search>? recent_search = await _context.Recents_Searches.Where(a => a.id_user == personne.id).ToListAsync();
            List<Recent_SearchDto> recent_searchDto = _mapper.Map<List<Recent_SearchDto>>(recent_search);

            recruiter.recent_searchs = recent_searchDto;


            List<Annoucement>? test = await _context.Annoucements.Where(f => f.id_type == 1).ToListAsync();

            var random = new System.Random();
            test.Sort((x, y) => random.Next(-1, 2));

            List<AnnoucementRecentRecruiterDto> selectqualif = new List<AnnoucementRecentRecruiterDto>();

            foreach (Annoucement add in test)
            {
                AnnoucementRecentRecruiterDto testt = _mapper.Map<AnnoucementRecentRecruiterDto>(add);

                Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == add.id_status);
                testt.status = state;

                Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == add.id_naf_division);
                if (naf_div != null)
                {
                    testt.naf_division_title = naf_div.title;
                }

                Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == add.id_diplome);
                if(diplome != null)
                {
                    testt.diplome = diplome.diplome;
                } 

                var qualification = _context.Annoucements
                    .Include(x => x.skills)
                        .ThenInclude(x => x.Skill)
                    .Where(x => x.id_user == recruiter.id)
                    .ToList();

                // format json car sinon impossible a lire les donénes
                var options3 = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };

                var json3 = System.Text.Json.JsonSerializer.Serialize(qualification, options3);
                var jsonDoc3 = JsonDocument.Parse(json3);
                var root3 = jsonDoc3.RootElement;

                foreach (var user2 in root3.EnumerateArray())
                {
                    var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user2);

                    if (valRecup2 != null)
                    {
                        Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                        if (valRecup2.skills != null)
                        {
                            foreach (Qualification skill in valRecup2.skills)
                            {
                                Skill? sskillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                if (sskillss != null)
                                {
                                    if (sskillss.title != null)
                                    {
                                        skillDico.Add(sskillss.id, sskillss.title);
                                    }
                                }


                            }
                        }


                        var users = _context.Users
                            .Include(x => x.skills)
                            .Where(x => x.id == add.id_user)
                            .ToList();

                        // format json car sinon impossible a lire les donénes
                        var options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                        };

                        var json = System.Text.Json.JsonSerializer.Serialize(users, options);
                        var jsonDoc = JsonDocument.Parse(json);
                        var root = jsonDoc.RootElement;

                        Dictionary<Int32, string> skillDico2 = new Dictionary<Int32, string>();

                        foreach (var user in root.EnumerateArray())
                        {
                            var valRecup = System.Text.Json.JsonSerializer.Deserialize<User>(user);

                            if (valRecup != null)
                            {
                                // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"

                                if (valRecup.skills != null)
                                {
                                    foreach (Student_Skill skill in valRecup.skills)
                                    {
                                        if (skill != null)
                                        {
                                            Skill? skillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                            if (skillss != null)
                                            {
                                                if (skillss.title != null)
                                                {
                                                    skillDico2.Add(skillss.id, skillss.title);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        var compte = 0;
                        foreach (var skil in skillDico)
                        {
                            if (skillDico2 != null)
                            {
                                foreach (var skiluser in skillDico2)
                                {
                                    if (skiluser.Value == skil.Value)
                                    {
                                        compte++;
                                    }
                                }
                            }
                        }
                        if (compte > 0)
                        {
                            selectqualif.Add(testt);
                        }
                    }
                }

            }

            List<AnnoucementRecentRecruiterDto> selectlocqualif = new List<AnnoucementRecentRecruiterDto>();

            foreach (AnnoucementRecentRecruiterDto annod in selectqualif)
            {
                if (selectlocqualif.Count < 10)
                {
                    if (annod.user != null)
                    {
                        if (annod.user.cp != null)
                        {

                            if (recruiter.company.cp != null)
                            {
                                if (annod.user.cp.Substring(0, 2) == recruiter.company.cp.Substring(0, 2))
                                {
                                    selectlocqualif.Add(annod);
                                }
                            }
                        }
                    }
                }
            }

            recruiter.select_announcements = selectlocqualif;

            List<AnnoucementRecentRecruiterDto> justLocalisation = new List<AnnoucementRecentRecruiterDto>();

            foreach (Annoucement add in test)
            {
                AnnoucementRecentRecruiterDto testt = _mapper.Map<AnnoucementRecentRecruiterDto>(add);

                if (selectlocqualif.Count < 10)
                {
                    if (testt.user != null)
                    {
                        if (testt.user.cp != null)
                        {
                            if (recruiter.company.cp != null)
                            {
                                if (testt.user.cp.Substring(0, 2) == recruiter.company.cp.Substring(0, 2))
                                {
                                    Annoucement_State? state = _context.Annoucement_Status.FirstOrDefault(i => i.id == add.id_status);
                                    testt.status = state;

                                    Naf_Division? naf_div = _context.Naf_Divisions.FirstOrDefault(n => n.id == add.id_naf_division);
                                    if (naf_div != null)
                                    {
                                        testt.naf_division_title = naf_div.title;
                                    }

                                    Diplome? diplome = _context.Diplomes.FirstOrDefault(d => d.id == add.id_diplome);
                                    if(diplome != null)
                                    {
                                        testt.diplome = diplome.diplome;
                                    } 

                                    var qualification = _context.Annoucements
                                        .Include(x => x.skills)
                                            .ThenInclude(x => x.Skill)
                                        .Where(x => x.id_user == recruiter.id)
                                        .ToList();

                                    // format json car sinon impossible a lire les donénes
                                    var options3 = new JsonSerializerOptions
                                    {
                                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                                    };

                                    var json3 = System.Text.Json.JsonSerializer.Serialize(qualification, options3);
                                    var jsonDoc3 = JsonDocument.Parse(json3);
                                    var root3 = jsonDoc3.RootElement;

                                    foreach (var user2 in root3.EnumerateArray())
                                    {
                                        var valRecup2 = System.Text.Json.JsonSerializer.Deserialize<Annoucement>(user2);

                                        if (valRecup2 != null)
                                        {
                                            Dictionary<Int32, string> skillDico = new Dictionary<Int32, string>();

                                            if (valRecup2.skills != null)
                                            {
                                                foreach (Qualification skill in valRecup2.skills)
                                                {
                                                    Skill? sskillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                                    if (sskillss != null)
                                                    {
                                                        if (sskillss.title != null)
                                                        {
                                                            skillDico.Add(sskillss.id, sskillss.title);
                                                        }
                                                    }


                                                }
                                            }


                                            var users = _context.Users
                                                .Include(x => x.skills)
                                                .Where(x => x.id == add.id_user)
                                                .ToList();

                                            // format json car sinon impossible a lire les donénes
                                            var options = new JsonSerializerOptions
                                            {
                                                ReferenceHandler = ReferenceHandler.IgnoreCycles
                                            };

                                            var json = System.Text.Json.JsonSerializer.Serialize(users, options);
                                            var jsonDoc = JsonDocument.Parse(json);
                                            var root = jsonDoc.RootElement;

                                            Dictionary<Int32, string> skillDico2 = new Dictionary<Int32, string>();

                                            foreach (var user in root.EnumerateArray())
                                            {
                                                var valRecup = System.Text.Json.JsonSerializer.Deserialize<User>(user);

                                                if (valRecup != null)
                                                {
                                                    // pour les skills on créer un dictionnaire pour avoir les skills de type "id": "value"

                                                    if (valRecup.skills != null)
                                                    {
                                                        foreach (Student_Skill skill in valRecup.skills)
                                                        {
                                                            if (skill != null)
                                                            {
                                                                Skill? skillss = _context.Skills.FirstOrDefault(s => s.id == skill.id_skill);
                                                                if (skillss != null)
                                                                {
                                                                    if (skillss.title != null)
                                                                    {
                                                                        skillDico2.Add(skillss.id, skillss.title);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            var compte = 0;
                                            foreach (var skil in skillDico)
                                            {
                                                if (skillDico2 != null)
                                                {
                                                    foreach (var skiluser in skillDico2)
                                                    {
                                                        if (skiluser.Value == skil.Value)
                                                        {
                                                            compte++;
                                                        }
                                                    }
                                                }
                                            }
                                            if (compte > 0)
                                            {
                                                selectqualif.Add(testt);
                                            }
                                        }
                                    }

                                    justLocalisation.Add(testt);
                                }
                            }
                        }
                    }
                }

            }

            recruiter.loc_announcements = justLocalisation;

            return Ok(recruiter);
        }
    }

    [HttpPost("AddStudent")]
    public async Task<IActionResult> CreateStudent([FromForm] FileModel cv_picture, [FromForm] StudentCreateDto student)
    {
        User? userexist = _context.Users.FirstOrDefault(s => s.email == student.email);

        if(userexist == null)
        {
            User? user = _mapper.Map<User>(student);

            user.is_online = true;
            user.id_type_user = 1;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            User? usertest = _context.Users.FirstOrDefault(s => s.email == student.email);

            string folderPath = @"./document/" + usertest.id;
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string name_cv = "cv_" + usertest.firstname + "_" + usertest.surname + ".pdf";
            string name_picture = "picture_" + usertest.firstname + "_" + usertest.surname + ".png";

            string path = Path.Combine(folderPath, name_cv);
            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                cv_picture.file_cv.CopyTo(stream);
            }

            string path2 = Path.Combine(folderPath, name_picture);
            using(Stream stream2 = new FileStream(path2, FileMode.Create))
            {
                cv_picture.file_picture.CopyTo(stream2);
            }

            usertest.cv = name_cv;
            usertest.picture = name_picture;

            _context.Users.Update(usertest);
            await _context.SaveChangesAsync();

            return Ok("creer");
        }

        return Ok("existe");
        
    }

    [HttpPost("AddRecruiter")]
    public async Task<IActionResult> CreateRecruiter(RecruiterCreateDto recruiter)
    {
        Company? company = _context.Companies.FirstOrDefault(s => s.siren == recruiter.id_company);

        if(company != null)
        {
            User? user = _mapper.Map<User>(recruiter);
            user.id_type_user = 2;
            user.is_online = true;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("recruter bien créé");
        }

        return Ok("recruter pas bien créé");
    }

    [HttpPut("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(string mail, string password)
    {
        User? personne = _context.Users.FirstOrDefault(i => i.email == mail);

        if(personne != null)
        {
            personne.password = password;
            _context.Users.Update(personne);
            await _context.SaveChangesAsync();
        }
        
        return Ok("password bien modifié");
    }

    

}