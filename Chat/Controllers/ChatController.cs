using Chat.Model;
using Chat.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    public class ChatController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IChatService _chatService;
        public ChatController(IConfiguration configuration, IChatService chatService)
        {
            _configuration = configuration;
            _chatService = chatService;
        }
        public IActionResult Index()
        {
            ViewBag.Channels = _configuration.GetSection("ChannelList").Get<List<string>>();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<List<ChatModel>>> GetMessagesByChannel(string channel)
        {
            return await _chatService.GetMessageByChannel(channel);
        }
    }
}
