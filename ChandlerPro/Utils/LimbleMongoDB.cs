using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChandlerPro.Utils
{
    public class LimbleMongoDB
    {
        //public static IMongoDatabase Connection()
        //{
            
        //    var client = new MongoClient("mongodb://localhost:27017");

        //    IMongoDatabase db = client.GetDatabase("limble_data");   
        //    return db;
        //}

        public static IMongoDatabase Connection()
        {
            var client = new MongoClient("mongodb+srv://chandler:rithm-chandler-" +
                "582855316@cluster0.ceq2qak.mongodb.net/?retryWrites=true&w=majority");
            IMongoDatabase db = client.GetDatabase("chandler");
            return db;
        }

    }
}

