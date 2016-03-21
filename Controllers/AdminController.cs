using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMA.Models;
using PagedList.Mvc;
using PagedList;

namespace TaskMA.Controllers
{
    [Authorize(Users = "admin_PM@hotmail.com")]

    public class AdminController : AuthorizedController
    {

        private TaskContext db = new TaskContext();
        const int pageSize = 10;

        public AdminController()
            {
              
            }
        // GET: Admin
        public ActionResult Index()
        {

            Employee_Status obj = new Employee_Status();
            obj.employees = db.Employees.ToList();
            obj.statuses = db.Statuses.ToList();
            obj.projects = db.Projects.ToList();

            obj.employeeCount = obj.employees.Count;
            obj.employeePageSize = pageSize;
            obj.employeePageNumber = 1;
            obj.employees = obj.employees.Take(pageSize).ToList();

            obj.statusCount = obj.statuses.Count;
            obj.statusPageSize = pageSize;
            obj.statusPageNumber = 1;
            obj.statuses = obj.statuses.Take(pageSize).ToList();

            obj.projectsCount = obj.projects.Count;
            obj.projectsPageSize = pageSize;
            obj.projectsPageNumber = 1;
            obj.projects = obj.projects.Take(pageSize).ToList();

            return View(obj);
        }

 
        // Search Employee
        public ActionResult EmployeeSearch(string employeeSearchBox, int pageIndex = 1)
        {
            Employee_Status obj_search = new Employee_Status();

            if (employeeSearchBox != null && employeeSearchBox != " ")
            {
                obj_search.employees = (from t in db.Employees
                                        where
                                            t.EmpName.Contains(employeeSearchBox)
                                        select t).ToList();
            }
            else
            {
                obj_search.employees = db.Employees.ToList();
            }

            obj_search.employeeCount = obj_search.projects.Count;
            obj_search.employeePageSize = pageSize;

            obj_search.employeePageNumber = pageIndex;

            obj_search.projects = obj_search.projects.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            obj_search.employeeSearchBox = employeeSearchBox;

            return PartialView("_Employee", obj_search);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="EmployeeId,EmpName,BirthDate,Email,PhoneNumber,Job,Picture")] Employee employee, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {

                if (ImageFile != null)
                {
                    string pic = System.IO.Path.GetFileName(ImageFile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Profile"), pic);

                    //File is uploaded
                    ImageFile.SaveAs(path);
                    employee.ImagePath = pic;

                    //Save - database
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ImageFile.InputStream.CopyTo(ms);
                        employee.Picture = ms.GetBuffer();
                    }

                }
                else
                {
                    employee.ImagePath = "unknown.png";
                }
                db.Employees.Add(employee);
                db.SaveChanges();

                TempData["Success"] = "The employee was inserted with success!";
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }


            return View(employee);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmpName,BirthDate,Email,PhoneNumber,Job,Picture")] Employee employee, HttpPostedFileBase ImageFile_edit)
        {
            if (ModelState.IsValid)
            {


                if (ImageFile_edit != null)
                {
                    string pic = System.IO.Path.GetFileName(ImageFile_edit.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Profile"), pic);

                    //File is uploaded
                    ImageFile_edit.SaveAs(path);
                    employee.ImagePath = pic;

                    //Save - database
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ImageFile_edit.InputStream.CopyTo(ms);
                        employee.Picture = ms.GetBuffer();
                    }

                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //Keep the same Image
                    var excluded = new[] { "ImagePath" };

                    var entry = db.Entry(employee);
                    entry.State = EntityState.Modified;
                    foreach (var name in excluded)
                    {
                        entry.Property(name).IsModified = false;

                    }

                    db.SaveChanges();
                }



                db.SaveChanges();
               

                TempData["Success_edit"] = "The information was edited.";
                return RedirectToAction("Index");   
            }
       
            return View(employee);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
