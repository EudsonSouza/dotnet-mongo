using Api.Data;
using Api.Data.Collections;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Api.Services
{
    public class InfectadosService
    {
        IMongoCollection<Infectado> _infectadosCollection;
        public InfectadosService(MongoDatabase mongoDB)
        {
            _infectadosCollection = mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        public void Add(Infectado infectado)
        {
            infectado.OnCreate();
            _infectadosCollection.InsertOne(infectado);
        }

        public List<Infectado> GetAll()
        {
            return _infectadosCollection.Find(m => true).ToList();
        }

        public Infectado GetById(ObjectId id)
        {
            return _infectadosCollection.Find(i => i.Id == id).FirstOrDefault();
        }

        public void Update(Infectado infectado)
        {
            infectado.OnUpdate();
            _infectadosCollection.ReplaceOne(i => i.Id == infectado.Id, infectado);
        }

        public void Delete(ObjectId id)
        {
            _infectadosCollection.DeleteOne(i => i.Id == id);
        }



        
    }
}
