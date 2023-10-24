using GroceryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryManagment.Controllers
{
    public class SaleController : Controller
    {
        Grocery_managementEntities db = new Grocery_managementEntities();

        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplaySale()
        {
            List<sale> list = db.sales.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult SaleProduct()
        {
            // Create a new instance of the purchase model
            var saleModel = new sale();

            // Get a list of product names
            List<string> productList = db.products.Select(x => x.product_name).ToList();

            // Set the SelectList for the dropdown
            ViewBag.ProductName = new SelectList(productList);

            // Pass the purchaseModel to the view
            return View(saleModel);
        }


        [HttpPost]
        public ActionResult saleProduct(sale s)
        {
            db.sales.Add(s);

            string product_name = s.sale_product;

            product prd = db.products.FirstOrDefault(p => p.product_name == product_name);
            prd.product_quantity = Convert.ToString(Convert.ToInt32(prd.product_quantity) - Convert.ToInt32(s.sale_quantity));

            db.SaveChanges();

            return RedirectToAction("Displaysale");
        }


        [HttpGet]
        public ActionResult Details(int id)
        {

            sale s = db.sales.Where(x => x.id == id).SingleOrDefault();
            return View(s);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            sale s = db.sales.Where(x => x.id == id).SingleOrDefault();
            return View(s);
        }

        [HttpPost]
        public ActionResult Delete(int id, sale sa)
        {

            sale s = db.sales.Where(x => x.id == id).SingleOrDefault();
            if (s != null)
            {
                db.sales.Remove(s);
                db.SaveChanges();
            }
            return RedirectToAction("DisplaySale");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            sale s = db.sales.Where(x => x.id == id).SingleOrDefault();
            List<string> list = db.products.Select(x => x.product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View(s);
        }

        [HttpPost]
        public ActionResult Edit(int id, sale sa)
        {
            sale s = db.sales.Where(x => x.id == id).SingleOrDefault();
            s.sale_date = sa.sale_date;
            s.sale_product = sa.sale_product;
            s.sale_quantity = sa.sale_quantity;
            db.SaveChanges();
            return RedirectToAction("Displaysale");
        }
    }
}