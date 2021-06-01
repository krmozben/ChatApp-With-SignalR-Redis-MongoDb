using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Service
{
    public interface IChatService
    {
        public Task SetMessage(ChatModel chatModel);
        public Task<List<ChatModel>> GetMessageByChannel(string channel);
    }
}
