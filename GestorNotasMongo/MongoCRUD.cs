using System.Configuration; 
using MongoDB.Driver;      
using System.Collections.Generic; 

public class MongoCRUD
{
    private IMongoDatabase db; 

    public MongoCRUD()
    {
       
        string connectionString = ConfigurationManager.AppSettings["MongoConnection"];
        string databaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];

    
        var client = new MongoClient(connectionString);

        
        db = client.GetDatabase(databaseName);
    }

  
    public IMongoCollection<T> ConectarColeccion<T>(string collectionName)
    {
        return db.GetCollection<T>(collectionName);
    }
}