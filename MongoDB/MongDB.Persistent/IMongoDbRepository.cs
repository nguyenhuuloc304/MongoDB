using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongDB.Persistent
{
    public interface IMongoDbRepository
    {
        IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IAggregateRoot;

        void DropCollection<TDocument>() where TDocument : IAggregateRoot;

        IFindFluent<TDocument, TDocument> Find<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot;

        TDocument Get<TDocument>(string id) where TDocument : IAggregateRoot;

        void Create<TDocument>(TDocument document) where TDocument : IAggregateRoot;

        void Replace<TDocument>(TDocument entity) where TDocument : IAggregateRoot;

        void Delete<TDocument>(string id) where TDocument : IAggregateRoot;

        void Delete<TDocument>(TDocument document) where TDocument : IAggregateRoot;

        void DeleteMany<TDocument>(FilterDefinition<TDocument> filter = null) where TDocument : IAggregateRoot;
    }
}