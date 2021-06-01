using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public abstract class MongoBaseEntity
    {
        public ObjectId Id { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public void OnCreate()
        {
            Id = ObjectId.GenerateNewId();
            DateCreated = DateTime.Now;
        }

        public void OnUpdate()
        {
            DateModified = DateTime.Now;
        }
    }


}
