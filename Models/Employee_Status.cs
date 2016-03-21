using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMA.Models
{
    public class Employee_Status
    {

        public List<Employee> employees { get; set; }

        public int employeeCount { get; set; }

        public int employeePageNumber { get; set; }

        public int employeePageSize { get; set; }

        public string employeeSearchBox { get; set; }


        public List<Status> statuses { get; set; }

        public int statusCount { get; set; }

        public int statusPageNumber { get; set; }

        public int statusPageSize { get; set; }

        public string statusSearchBox { get; set; }


        public List<TaskDetail> tasks { get; set; }

        public int tasksCount { get; set; }

        public int tasksPageNumber { get; set; }

        public int tasksPageSize { get; set; }

        public string tasksSearchBox { get; set; }

       
        public List<Project> projects { get; set; }

        public int projectsCount { get; set; }

        public int projectsPageNumber { get; set; }

        public int projectsPageSize { get; set; }

        public string projectsSearchBox { get; set; }

        



    }
}