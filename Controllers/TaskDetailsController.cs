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

    public class TaskDetailsController : AuthorizedController
    {
        private TaskContext db = new TaskContext();
        const int pageSize = 13;


        // GET: TaskDetails
        public ActionResult Index()
        {
 
            Employee_Status obj = new Employee_Status();
            obj.employees = db.Employees.ToList();
            obj.statuses = db.Statuses.ToList();
            obj.tasks = db.Tasks.ToList();
            obj.projects = db.Projects.ToList();
           

            obj.tasksCount = obj.tasks.Count;
            obj.tasksPageSize = pageSize;
            obj.tasksPageNumber = 1;
            obj.tasks = obj.tasks.Take(pageSize).ToList();

            return View(obj);
        }


        // Search
        public ActionResult TaskSearchPage(string SearchBox, int pageIndex = 1)
        {
            Employee_Status obj = new Employee_Status();
            obj.employees = db.Employees.ToList();
            obj.statuses = db.Statuses.ToList();
            obj.projects = db.Projects.ToList();
           
            if (SearchBox != null && SearchBox != " ")
            {
                obj.tasks = (from t in db.Tasks
                                        where
                                            t.TaskName.Contains(SearchBox)
                                            || t.Description.Contains(SearchBox)
                                            || t.employee.EmpName.Contains(SearchBox)
                                            || t.status.StatusName.Contains(SearchBox)
                                            || t.project.ProjectName.Contains(SearchBox)
                                        select t).ToList();
            }
            else
            {
                obj.tasks = db.Tasks.ToList();
                ViewData["Error"] = "Error message text.";
            }
        
            obj.tasksCount = obj.tasks.Count;
            obj.tasksPageSize = pageSize;

            obj.tasksPageNumber = pageIndex;

            obj.tasks = obj.tasks.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            obj.tasksSearchBox = SearchBox;

            return PartialView("_TasksList", obj);
        }


        [Authorize(Users = "admin_PM@hotmail.com")]
        // GET: TaskDetails/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmpName");
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");

            return View();
        }

        // POST: TaskDetails/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Create([Bind(Include = "EmployeeId,ProjectId,TaskName,StatusId,Description,Assign_Dt")] TaskDetail taskDetail)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(taskDetail);
                db.SaveChanges();
                TempData["Success"] = "The employee was inserted with success!";
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmpName", taskDetail.EmployeeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", taskDetail.StatusId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", taskDetail.ProjectId);
            
          
            return View(taskDetail);

        }

        [Authorize(Users = "admin_PM@hotmail.com")]
        // GET: TaskDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskDetail taskDetail = db.Tasks.Find(id);
            if (taskDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmpName", taskDetail.EmployeeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", taskDetail.StatusId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", taskDetail.ProjectId);
            
           return View(taskDetail);
        }

        // POST: TaskDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Edit([Bind(Include = "EmployeeId,ProjectId,TaskName,StatusId,Description,Assign_Dt")] TaskDetail taskDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskDetail).State = EntityState.Added;
                db.SaveChanges();
                TempData["Success_edit"] = "The information was edited.";
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmpName", taskDetail.EmployeeId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "StatusName", taskDetail.StatusId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", taskDetail.ProjectId);
            return View(taskDetail);
        }

        [Authorize(Users = "admin_PM@hotmail.com")]
        // GET: TaskDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskDetail taskDetail = db.Tasks.Find(id);
            if (taskDetail == null)
            {
                return HttpNotFound();
            }
            return View(taskDetail);
        }

        // POST: TaskDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskDetail taskDetail = db.Tasks.Find(id);
            db.Tasks.Remove(taskDetail);
            db.SaveChanges();
            TempData["Success_delete"] = "The information was deleted.";
            return RedirectToAction("Index");
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
