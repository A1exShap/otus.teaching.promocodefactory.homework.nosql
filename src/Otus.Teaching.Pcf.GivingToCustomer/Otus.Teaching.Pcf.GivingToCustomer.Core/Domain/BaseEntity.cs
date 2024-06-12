using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Domain
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
    }
}