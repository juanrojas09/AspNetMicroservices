using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly ICatalogContext context;
        public ProductRepositories(ICatalogContext context)
        {
            this.context = context;

        }

        /// <summary>
        /// Metodo que trae todos los productos del catalogo
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var Products = await context.Product.Find(p => true).ToListAsync();
            return Products;
        }
        public async Task CreateProduct(Product product)
        {
            await context.Product.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await context.Product.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await context.Product.ReplaceOneAsync(filter:g=>g.Id == product.Id, replacement: product);
            
            //e primer metodo devuelve true si la operacion fue reconocida por el server y el otro devuelve el numero de docs modificados
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        
        /// <summary>
        /// Metodo que trae un producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  async Task<Product> GetProduct(string id)
        {
            var Product = await context.Product.Find(p=>p.Id == id).FirstOrDefaultAsync();
            return Product;
        }

        /// <summary>
        /// Metodo que trae un producto por categoria
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var Product = await context.Product.FindAsync(p => p.Category.Equals(categoryName));
            return Product.ToEnumerable();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            //la definicion del filtro viene del mongo db drive
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await context.Product.Find(filter).ToListAsync();
        }

        

     
    }
}
