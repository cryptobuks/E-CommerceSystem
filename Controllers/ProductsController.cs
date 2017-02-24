using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Common;

namespace Assignment.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [HttpGet]
        public ActionResult ProductCatalog()
        {
            IQueryable<Product> listProducts = new ProductsBL().GetProducts(); //i am storing the list of books
            List<Product> listProduct = new List<Product>();
            foreach (Product p in listProducts) {
                if (p.Available == true) {
                    listProduct.Add(p);
                }
            }
          
            return View(listProduct);
 
        }

        [HttpPost]
        public ActionResult ProductCatalog(string input)//we have to call the method in the business logic layer
        {
            IQueryable<Product> foundProducts = new ProductsBL().GetProducts(input);
            List<Product> listProduct = new List<Product>();
            foreach (Product p in foundProducts)
            {
                if (p.Available == true)
                {
                    listProduct.Add(p);
                }
            }
            return View(foundProducts); //the returned list of users will he be displayed
                                     //in the same view tha was loaded in the first action
        }
        
        public ActionResult Details(int productId)//list details of the product
        {
            Product p= new ProductsBL().GetProduct(productId);
            return View(p);//you are passing the returned book which we are calling b as a parameter to the view
        }

        //so that when we click the register new book link this will be called
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //so that the user will input the book details
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(Product p, HttpPostedFileBase productImage)
        {
            try
            {
                //to insert an image
                string absoluteFolderPath = Server.MapPath("\\ProductImages"); //in case of the assignment, productImages instead of images
                string extOfImage = System.IO.Path.GetExtension(productImage.FileName); //get extension
                string newFilename = Guid.NewGuid() + extOfImage;
                absoluteFolderPath += "\\" + newFilename;
                productImage.SaveAs(absoluteFolderPath);

                //in the database we store the relative path Images/filename.jpg
                string relativePath = "\\Images\\" + newFilename;
                p.Image = relativePath;
               
                new ProductsBL().AddProduct(p); //to add the book in the database
                ViewData["msg"] = "Success";
                ModelState.Clear(); //to clear the boxes
            }
            catch
            {
                ViewData["msg"] = "Registration of product failed. Please check details.";
            }


            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int productId)
        {
            new ProductsBL().DeleteProduct(productId);
            //to display the mose recent list of books in the dabatese
            return RedirectToAction("ProductCatalog");
        }

        //getting the book with the isbn in order to edit its details
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Update(int productId)
        {
            //you must do a try catch in order to handle bad inserted data for example one must enter an integer and he enters a string
            Product productToEdit = new ProductsBL().GetProduct(productId);
            return View(productToEdit);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Update(Product modifiedDetails)
        {
            //you must do a try catch in order to handle bad inserted data for example one must enter an integer and he enters a string
            new ProductsBL().UpdateProduct(modifiedDetails);
            return RedirectToAction("ProductCatalog");
        }
    }
}