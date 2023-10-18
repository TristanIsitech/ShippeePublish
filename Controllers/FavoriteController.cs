using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FavoriteController> _logger;
    private readonly IMapper _mapper;


    public FavoriteController(ILogger<FavoriteController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost("AddFavorite")]
    public async Task<IActionResult> CreateFavorite(FavoriteDto fav)
    {
        Favorite favorie = new Favorite();
        favorie.id_annoucement = fav.id_annoucement;
        favorie.id_user = fav.id_user;

        Favorite? exist = _context.Favorites.FirstOrDefault(i => i.id_annoucement == favorie.id_annoucement && i.id_user == favorie.id_user);

        if(exist == null)
        {
            await _context.Favorites.AddAsync(favorie);
            await _context.SaveChangesAsync();

            return Ok("Favorie ajouté");
        }
        else{
            _context.Favorites.Remove(exist);
            await _context.SaveChangesAsync();

            return Ok("Favorie retiré");
        }

        
    }

}