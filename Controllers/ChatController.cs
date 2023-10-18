using ShippeeAPI.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShippeeAPI.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ChatController> _logger;
    private readonly IMapper _mapper;


    public ChatController(ILogger<ChatController> logger, ApplicationDbContext dbContext, IMapper mapper)
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost("AddChat")]
    public async Task<IActionResult> CreateMessageChat(AddChatDto sms)
    {
        string date_today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Chat chat = new Chat();
        chat.id_sender = sms.id_sender;
        chat.id_recipient = sms.id_recipient;
        chat.content = sms.content;
        chat.send_time = Convert.ToDateTime(date_today);
        chat.status = 1;

        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();

        return Ok("message envoyé");
    }

    [HttpPut("UpdateStatusChat")]
    public async Task<IActionResult> UpdateStatusChat(UpdateChatDto up_chat)
    {
        List<Chat>? chat = await _context.Chats.Where(i => i.id_sender == up_chat.id_sender && i.id_recipient == up_chat.id_user && i.status == 1).ToListAsync();

        if(chat == null) return Ok("pas de chat existant entre ces deux personnes");

        foreach(Chat chatdto in chat)
        {
            chatdto.status = 2;

            _context.Chats.Update(chatdto);
            await _context.SaveChangesAsync();
        }

        return Ok("messages lus");
    }

}