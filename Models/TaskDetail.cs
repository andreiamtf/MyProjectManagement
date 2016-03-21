using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMA.Models
{
    public class TaskDetail
    {
        public int TaskDetailId { get; set; }

        [Required(ErrorMessage = "The Task Name is required")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9 ' ']*$", ErrorMessage = "Invalid characters in task name")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "The Description is required")]
        public string Description { get; set; }


        [Required(ErrorMessage = "The Date of Birh is required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Assign_Dt { get; set; }

        public int EmployeeId { get; set; }

        public int StatusId { get; set; }

        public int ProjectId { get; set; }

        public virtual Employee employee { get; set; }

        public virtual Status status { get; set; }

        public virtual Project project { get; set; }
    }
}