using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMA.Models;

namespace TaskMA.Controllers
{
    public class StatusController : Controller
    {
        private TaskContext db = new TaskContext();


      const int pageSize = 10;

        // GET: Admin
        public ActionResult Index()
        {

            Employee_Status obj = new Employee_Status();
            obj.employees = db.Employees.ToList();
            obj.statuses = db.Statuses.ToList();
            obj.projects = db.Projects.ToList();

            obj.statusCount = obj.statuses.Count;
            obj.statusPageSize = pageSize;
            obj.statusPageNumber = 1;
            obj.statuses = obj.statuses.Take(pageSize).ToList();


            return View(obj);
        }

        //Search Status
        public ActionResult StatusSearch(string statusSearchBox, int pageIndex_status = 1)
        {
            Employee_Status obj_search = new Employee_Status();

            if (statusSearchBox != null && statusSearchBox != " ")
            {

                obj_search.statuses = (from t in db.Statuses
                                       where
                                           t.StatusName.Contains(statusSearchBox)
                                       select t).ToList();
            }
            else
            {
                obj_search.statuses = db.Statuses.ToList();
            }


            obj_search.statusCount = obj_search.statuses.Count;
            obj_search.statusPageSize = pageSize;

            obj_search.statusPageNumber = pageIndex_status;

            obj_search.statuses = obj_search.statuses.Skip((pageIndex_status - 1) * pageSize).Take(pageSize).ToList();
            obj_search.statusSearchBox = statusSearchBox;

            return PartialView("_Status", obj_search);
        }



        // GET: Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = db.Statuses.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusId,StatusName")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Statuses.Add(status);
                db.SaveChanges();
                TempData["StatusSuccess"] = "The status was inserted with success!";
                return RedirectToAction("Index", "Admin");
            }

            return View(status);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = db.Statuses.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusId,StatusName")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                TempData["StatusSuccess_edit"] = "The information was edited with success!";
                return RedirectToAction("Index", "Admin");
            }
            return View(status);
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = db.Statuses.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Status status = db.Statuses.Find(id);
            db.Statuses.Remove(status);
            db.SaveChanges();
            TempData["StatusSuccess_delete"] = "The status was removed with success!";
            return RedirectToAction("Index", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
