using MongoDB.Driver;
using proanimafilms_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proanimafilms_api.Services
{
    public class FilmService
    {
        private readonly IMongoCollection<FeaturedFims> _featured_fims;

        public FilmService(IProAnimaFILMS_botDatabaseSettings settings)
        {
            //    var client = new MongoClient(settings.ConnectionString);
            //    var database = client.GetDatabase(settings.DatabaseName);

            var client = new MongoClient("mongodb://Valentine:0975484459@cluster0-shard-00-00-ioh5q.gcp.mongodb.net:27017,cluster0-shard-00-01-ioh5q.gcp.mongodb.net:27017,cluster0-shard-00-02-ioh5q.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = client.GetDatabase(settings.DatabaseName);

            _featured_fims = database.GetCollection<FeaturedFims>(settings.FilmsCollectionName);
        }

        public List<FeaturedFims> Get() =>
            _featured_fims.Find(film => true).ToList();

        public FeaturedFims Get(int id) =>
            _featured_fims.Find<FeaturedFims>(film => film._id == id).FirstOrDefault();

        public FeaturedFims Create(FeaturedFims film)
        {
            try
            {
                _featured_fims.InsertOne(film);
                return film;
            }
            catch (Exception) { return null; }
        }

        public void Update(int id, FeaturedFims filmIn) =>
            _featured_fims.ReplaceOne(film => film._id == id, filmIn);
    }
}
