﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Services.SProduct;
using PizzeriaTNAI.Entities;
using PizzeriaTNAI.Entities.Models;

namespace PizzeriaTNAI.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,PictureUrl,Description")] Product product)
        {
            if (!ModelState.IsValid) return View(product);
            var result = Task.Run(() => _productService.SaveProductAsync(product)).Result;
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = Task.Run(() => _productService.GetProductAsync((int)id)).Result;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = Task.Run(() => _productService.DeleteProductAsync((int)id)).Result;
            return RedirectToAction("Index");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = Task.Run(() => _productService.GetProductAsync((int)id)).Result;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = Task.Run(() => _productService.GetProductAsync((int)id)).Result;

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,PictureUrl,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(product).State = EntityState.Modified;
                // db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = Task.Run(() => _productService.GetProductsAsync()).Result;
            return View(products);
        }

        public ActionResult Menu()
        {
            var products = Task.Run(() => _productService.GetProductsAsync()).Result;
            ViewBag.Menu = "active";
            return View(products);
        }
    }
}