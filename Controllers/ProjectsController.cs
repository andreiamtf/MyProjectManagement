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
    public class ProjectsController : Controller
    {
        private TaskContext db = new TaskContext();
        const int pageSize = 10;

        // GET: Admin
        public ActionResult Index()
        {

            Employee_Status obj = new Employee_Status();
            obj.employees = db.Employees.ToList();
            obj.statuses = db.Statuses.ToList();
            obj.tasks = db.Tasks.ToList();
            obj.projects = db.Projects.ToList();


            obj.projectsCount = obj.projects.Count;
            obj.projectsPageSize = pageSize;
            obj.projectsPageNumber = 1;
            obj.projects = obj.projects.Take(pageSize).ToList();

            return View(obj);
        }

        // Search Employee
        public ActionResult ProjectSearch(string SearchBox, int pageIndex = 1)
        {
            Employee_Status obj_search = new Employee_Status();

            if (SearchBox != null && SearchBox != " ")
            {
                obj_search.projects = (from t in db.Projects
                                       where
                                           t.ProjectName.Contains(SearchBox)
                                       select t).ToList();
            }
            else
            {
                obj_search.projects = db.Projects.ToList();
            }

            obj_search.projectsCount = obj_search.projects.Count;
            obj_search.projectsPageSize = pageSize;

            obj_search.projectsPageNumber = pageIndex;

            obj_search.projects = obj_search.projects.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            obj_search.projectsSearchBox = SearchBox;

            return PartialView("_Project", obj_search);
        }


        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName,ProjectDescription")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                TempData["Success_project"] = "The project was inserted with success!";
                return RedirectToAction("Index", "Admin");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,ProjectDescription")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success_edit_project"] = "The project was inserted with success!";
                return RedirectToAction("Index", "Admin");
    
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            TempData["Success_delete_project"] = "The information was deleted.";
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
