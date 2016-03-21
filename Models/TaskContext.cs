using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TaskMA.Models
{
    public class TaskContext: DbContext
    {
        public DbSet<TaskDetail> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Status> Statuses{get; set;}
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        
        }
    }
}