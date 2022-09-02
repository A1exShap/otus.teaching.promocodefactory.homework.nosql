using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Otus.Teaching.Pcf.Administration.Core.Domain
{
    public class BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
    }
}