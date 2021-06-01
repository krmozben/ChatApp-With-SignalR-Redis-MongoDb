using Chat.Model;
using Chat.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Service
{
    public class MongoService
    {
        public MongoService(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            Chat = database.GetCollection<ChatModel>("Chat");
        }

        public IMongoCollection<ChatModel> Chat { get; }
    }
}
