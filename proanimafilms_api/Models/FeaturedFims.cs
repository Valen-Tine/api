using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proanimafilms_api.Models
{
    public class FeaturedFims
    {
        [BsonId]
        public int _id { get; set; }
        public List<string> featuredfims { get; set; }
    }
}
