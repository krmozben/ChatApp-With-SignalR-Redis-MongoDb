using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Model
{
    public class ChatModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Channel { get; set; }
        public DateTime CreatedOn { get { return DateTime.Now; } set { } }
    }
}
