using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongDB.Persistent
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbRepository(string mongoDbConnectionString, string mongoDbDatabaseName = null)
        {
            if (String.IsNullOrWhiteSpace(mongoDbDatabaseName))
            {
                mongoDbDatabaseName = mongoDbConnectionString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            }

            Contract.Assert(!String.IsNullOrWhiteSpace(mongoDbDatabaseName));

            IMongoClient mongoClient = new MongoClient(mongoDbConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoDbDatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IAggregateRoot
        {
            return _mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        public void DropCollection<TDocument>() where TDocument : IAggregateRoot
        {
            _mongoDatabase.DropCollection(typeof(TDocument).Name);
        }

        public IFindFluent<TDocument, TDocument> Find<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot
        {
            if (filter == null)
            {
                var builder = Builders<TDocument>.Filter;
                filter = builder.Empty;
            }

            return _mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name).Find(filter);
        }

        public TDocument Get<TDocument>(string id) where TDocument : IAggregateRoot
        {
            return GetCollection<TDocument>().Find(it => it.Id == id).FirstOrDefault();
        }

        public void Create<TDocument>(TDocument document) where TDocument : IAggregateRoot
        {
            GetCollection<TDocument>().InsertOne(document);
        }

        public void Replace<TDocument>(TDocument document) where TDocument : IAggregateRoot
        {
            GetCollection<TDocument>().ReplaceOne(it => it.Id == document.Id, document);
        }

        public void Delete<TDocument>(string id) where TDocument : IAggregateRoot
        {
            GetCollection<TDocument>().DeleteOne(it => it.Id == id);
        }

        public void Delete<TDocument>(TDocument document) where TDocument : IAggregateRoot
        {
            GetCollection<TDocument>().DeleteOne(it => it.Id == document.Id);
        }

        public void DeleteMany<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot
        {
            if (filter == null)
            {
                var builder = Builders<TDocument>.Filter;
                filter = builder.Empty;
            }

            _mongoDatabase.GetCollection<TDocument>(typeof(TDocument).Name).DeleteMany(filter);
        }
    }
}