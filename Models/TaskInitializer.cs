using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.IO;

namespace TaskMA.Models
{
    public class TaskInitializer : DropCreateDatabaseAlways<TaskContext>
    {

        protected override void Seed(TaskContext context)
        {

            var employees = new List<Employee>
            {                 
                 new Employee {EmpName = "James Dean", BirthDate = DateTime.Parse("1980-05-01"), Email = "jamesdean@gmail.com", PhoneNumber = "4334654345", Job = "Software Engineer", ImagePath = "JamesDean.jpg"},
                 new Employee {EmpName = "Janine Karson", BirthDate = DateTime.Parse("1980-03-02"), Email = "janinekarson@gmail.com", PhoneNumber = "4578954341", Job = "Software Engineer", ImagePath = "JanineKarson.jpg"},
                 new Employee {EmpName = "Peter Kapa", BirthDate = DateTime.Parse("1981-06-14"), Email = "peterkapa@hotmail.com", PhoneNumber = "4578954322", Job = "Software Engineer", ImagePath = "PeterKapa.jpg"},
                 new Employee {EmpName = "Teresa Sun", BirthDate = DateTime.Parse("1988-01-02"), Email = "teresasun@hotmail.com", PhoneNumber = "4579812311", Job = "Front-End Developer", ImagePath = "TeresaSun.jpg"},
                 new Employee {EmpName = "Miguel Anka", BirthDate = DateTime.Parse("1970-01-02"), Email = "miguelanka@hotmail.com", PhoneNumber = "4579816511", Job = "C# Developer", ImagePath = "MiguelAnka.jpg"},
            };
            foreach (var temp in employees)
            {

                context.Employees.Add(temp);

            }

            var statuses = new List<Status>
            {
                new Status{StatusName = "Open"},
                new Status{StatusName = "Close"}

            };


            foreach (var temp in statuses)
            {

                context.Statuses.Add(temp);

            }


            var projects = new List<Project>
            {
                new Project{ProjectName = "Euro Hospital", ProjectDescription ="dcdc"},
                new Project{ProjectName = "ManageCar", ProjectDescription="scdcs"}

            };


            foreach (var temp in projects)
            {

                context.Projects.Add(temp);

            }

      
            var tasks = new List<TaskDetail>
            {
                new TaskDetail{TaskName = "Data Analysis", Description = "Confirm customer's Requirements",  Assign_Dt = DateTime.Parse("2015-01-02"), EmployeeId = 1, StatusId = 1, ProjectId = 1},
                new TaskDetail{TaskName = "Data Modeling", Description = "Define and analyze data requirements needed to support the business processes",  Assign_Dt = DateTime.Parse("2015-01-02"), EmployeeId = 2, StatusId = 1, ProjectId = 1},
                new TaskDetail{TaskName = "Start Development", Description = "Development",  Assign_Dt = DateTime.Parse("2015-01-02"), EmployeeId = 3, StatusId = 1, ProjectId = 1},
            };


            foreach (var temp in tasks)
            {

                context.Tasks.Add(temp);

            }
            context.SaveChanges();


        }

    }
}