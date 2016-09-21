using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyBox.Services;

namespace MoneyBox.Web.Controllers
{
    public class BoxController : Controller
    {
        private readonly IBoxService _serviceBox;
        private readonly IUserService _serviceUser;

        public BoxController(IBoxService serviceBox, IUserService serviceUser)
        {
            _serviceBox = serviceBox;
            _serviceUser = serviceUser;
        }

        // GET: Box
        public ActionResult Index()
        {
            var boxes = _serviceBox.LoadAllBoxes();
            var g1 = _serviceBox.GetGuid();
            var g2 = _serviceUser.GetGuid();
            return View(boxes);
        }

        // GET: Box/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Box/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Box/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Box/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Box/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Box/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Box/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
