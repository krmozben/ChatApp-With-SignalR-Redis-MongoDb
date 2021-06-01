using Chat.Hubs;
using Chat.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Consumer
{
    public class ChatConsumer
    {
        private readonly RedisService _redisService;
        private readonly ChatHub _chatHub;
        private readonly IConfiguration _configuration;

        public ChatConsumer(RedisService redisService, ChatHub chatHub, IConfiguration configuration)
        {
            _redisService = redisService;
            _chatHub = chatHub;
            _configuration = configuration;
        }

        public void Consume()
        {
            var pubsub = _redisService.ConnectionMultiplexer.GetSubscriber();

            var channels = _configuration.GetSection("ChannelList").Get<List<string>>();

            foreach (var channel in channels)
                pubsub.SubscribeAsync(channel, (channel, chatModel) => ReceiveMessaga(channel, chatModel));

        }

        public void ReceiveMessaga(string channel, string chatModel)
        {
            _chatHub.ReceiveMessageAsync(channel, chatModel);
        }
    }
}
