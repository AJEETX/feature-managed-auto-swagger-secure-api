using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain
{
    public interface IProductService
    {
        List<Product> Products { get; }

        Product GetById(int id);

        void Add(Product product);

        void Update(Product product);

        void Delete(int id);
    }

    internal class ProductService : IProductService
    {
        public List<Product> Products => DataStore.Products;

        public Product GetById(int id)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);

            return product;
        }

        public void Add(Product product)
        {
            Products.ToList().Add(product);
        }

        public void Update(Product product)
        {
            var actualProduct = GetById(product.Id);

            actualProduct.ProductName = product.ProductName;

            actualProduct.Description = product.Description;
        }

        public void Delete(int id)
        {
            var actualProduct = GetById(id);

            Products.ToList().Remove(actualProduct);
        }
    }
}