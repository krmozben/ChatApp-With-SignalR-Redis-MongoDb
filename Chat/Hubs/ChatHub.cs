using Chat.Model;
using Chat.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IConfiguration _configuration;
        public ChatHub(IChatService chatService, IConfiguration configuration)
        {
            _chatService = chatService;
            _configuration = configuration;
        }

        public async Task SendMessageAsync(ChatModel chatModel)
        {
            await _chatService.SetMessage(chatModel);
        }

        public async Task SetGroupAsync(string channel)
        {
            foreach (var channelItem in _configuration.GetSection("ChannelList").Get<List<string>>())
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelItem);

            await Groups.AddToGroupAsync(Context.ConnectionId, channel);
        }

        public async void ReceiveMessageAsync(string channel, string chatModel)
        {
            await Clients.Group(channel).SendAsync("ReceiveMessage", JsonSerializer.Deserialize<ChatModel>(chatModel));
        }
    }
}
