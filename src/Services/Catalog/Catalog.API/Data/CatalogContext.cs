using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        
        public CatalogContext(IConfiguration configuration)
        {
            //Creo el cliente que instancio para conectar la db
            var client = new MongoClient(configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
            //me traigo la base de datos
            var database = client.GetDatabase(configuration.GetValue<string>("ConnectionStrings:DatabaseName"));
             Product = database.GetCollection<Product>(configuration.GetValue<string>("ConnectionStrings:CollectionName"));
            CatalogContextSeed.SeedData(Product);
        }


        //esto seria como el contexto en si con sql server, lo de abajo seria el dbset y lo de arriba la conexion a toda la db,tablas,etc
        public IMongoCollection<Product> Product { get; }
    }
}
