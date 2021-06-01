using Chat.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat.Service
{
    public class ChatService : IChatService
    {
        private readonly RedisService _redisService;
        private readonly MongoService _mongoService;

        public ChatService(RedisService redisService, MongoService mongoService)
        {
            _redisService = redisService;
            _mongoService = mongoService;
        }

        public async Task<List<ChatModel>> GetMessageByChannel(string channel)
        {
            FilterDefinition<ChatModel> filter = Builders<ChatModel>.Filter.Eq(m => m.Channel, channel);

            return await _mongoService.Chat.Find(filter).SortBy(x=>x.CreatedOn).ToListAsync();
        }

        public async Task SetMessage(ChatModel chatModel)
        {
            var message = JsonSerializer.Serialize(chatModel);

            var pubsub = _redisService.ConnectionMultiplexer.GetSubscriber();

            await pubsub.PublishAsync(chatModel.Channel, message);

            await _mongoService.Chat.InsertOneAsync(chatModel);
        }
    }
}
