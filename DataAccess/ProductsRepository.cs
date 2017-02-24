using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class ProductsRepository : ConnectionClass
    {
        public ProductsRepository() : base() //to access base constructor public ConnectionClass()
        {
        }

        public IQueryable<Product> GetProducts() //to get the list of users . IQueryable is like a list
        {

            return Entity.Products;
        }

        public IQueryable<Product> GetProducts(string input)//searching for a product with name
        {

            return Entity.Products.Where(p => p.Name.Contains(input));
        }

        //we are going to use this method so that when we click show details link we will show the details of the product
        public Product GetProduct(int productId) //to get only one user with the productID. 
        {
            //returns a single object displaying null if not found
            return Entity.Products.SingleOrDefault(p => p.ProductID == productId); //has to be exactly like.
        }

        //to get the list of categories
        public IQueryable<Category> GetProductCategories()
        {
            return Entity.Categories;
        }

        public void AddProduct(Product p)
        {
            Entity.Products.Add(p);
            Entity.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            Entity.Products.Remove(p);
            Entity.SaveChanges();
        }


        public void UpdateProduct(Product p)
        {
            Product originalProduct = GetProduct(p.ProductID);

            originalProduct.Available = p.Available;
            originalProduct.Category = p.Category;
            originalProduct.Description = p.Description;
            originalProduct.Image = p.Image;
            originalProduct.Name = p.Name;
            originalProduct.Stock = p.Stock;
            originalProduct.ProductsPrices = p.ProductsPrices;
            originalProduct.Sale = p.Sale;


        }
    }
}
