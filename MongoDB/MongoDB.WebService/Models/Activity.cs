using MongDB.Persistent;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.WebService.Models
{
    [BsonIgnoreExtraElements]
    public class Activity : IAggregateRoot
    {
        public string Id { get; set; }

        public string Comment { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime yourDate { get; set; }

        public List<People> People { get; set; }
    }
}
