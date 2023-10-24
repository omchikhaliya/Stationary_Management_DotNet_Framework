using GroceryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryManagment.Controllers
{
    public class PurchaseController : Controller
    {
        Grocery_managementEntities db = new Grocery_managementEntities();

        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplayPurchase()
        {
            List<purchase> list = db.purchases.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult PurchaseProduct()
        {
            // Create a new instance of the purchase model
            var purchaseModel = new purchase();

            // Get a list of product names
            List<string> productList = db.products.Select(x => x.product_name).ToList();
            

            // Set the SelectList for the dropdown
            ViewBag.ProductName = new SelectList(productList);

            // Pass the purchaseModel to the view
            return View(purchaseModel);
        }


        [HttpPost]
        public ActionResult PurchaseProduct(purchase pur)
        {   
            db.purchases.Add(pur);
            string product_name = pur.purchase_product;
            
            product prd = db.products.FirstOrDefault(p => p.product_name == product_name);
            prd.product_quantity = Convert.ToString(Convert.ToInt32(prd.product_quantity) + Convert.ToInt32(pur.purchase_quantity));

            db.SaveChanges();
            
            return RedirectToAction("DisplayPurchase");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            
            purchase p = db.purchases.Where(x => x.id == id).SingleOrDefault();
            return View(p);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            purchase p = db.purchases.Where(x => x.id == id).SingleOrDefault();
            return View(p);
        }

        [HttpPost]
        public ActionResult Delete(int id, purchase per)
        {

            purchase p = db.purchases.Where(x => x.id == id).SingleOrDefault();
            if (p != null)
            {
                db.purchases.Remove(p);
                db.SaveChanges();
            }
            return RedirectToAction("DisplayPurchase");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            purchase p = db.purchases.Where(x => x.id == id).SingleOrDefault();
            List<string>list = db.products.Select(x => x.product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(int id,purchase pur)
        {
            purchase p = db.purchases.Where(x => x.id == id).SingleOrDefault();
            p.purchase_date = pur.purchase_date;
            p.purchase_product = pur.purchase_product;
            p.purchase_quantity = pur.purchase_quantity;
            db.SaveChanges();
            return RedirectToAction("DisplayPurchase");
        }

    }
}