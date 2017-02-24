using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class ProductsBL
    {
        public IQueryable<Product> GetProducts()//fowarding the the call to the get users method from usersRepository
        {
            
            return new ProductsRepository().GetProducts(); //getusers is found  in the users repository
        }
        public IQueryable<Product> GetProducts(string input)//calling the method in usersrepository
        {
            if (input == string.Empty)
            {
                return GetProducts();//return a list with all users if search box is empty(above method)
            }
            else
            {
                return new ProductsRepository().GetProducts(input);
            }
        }
        //to show the details of a book
        public Product GetProduct(int productId)
        {
            return new ProductsRepository().GetProduct(productId);
        }

        public IQueryable<Category> GetBookCategories()
        {
            return new ProductsRepository().GetProductCategories();
        }

        public void AddProduct(Product p)
        {
               new ProductsRepository().AddProduct(p);
        }
        //we did string isb in the parameters sos htat the user must input only the isbn not all the book details
        public void DeleteProduct(int productId)
        {
            ProductsRepository pr = new ProductsRepository();
            if (pr.GetProduct(productId) != null)
            {
                pr.DeleteProduct(pr.GetProduct(productId));
            }
            else
            {
                throw new Exception("Product does not exist");
            }
        }

        //we did not search for the isbn here because we are already searching for it in the books repsoitory.
        public void UpdateProduct(Product newProduct)
        {
            new ProductsRepository().UpdateProduct(newProduct);
        }
    }
}
