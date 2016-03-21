using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskMA.Models
{
    [Table("Employee")]
    public class Employee
    {   [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "The name is required")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9 ' ']*$", ErrorMessage = "Invalid characters in employee name")]
        public string EmpName { get; set; } 

        [Column(TypeName = "image")]
        //Database
        public byte[] Picture { get; set; }
        //Server
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "The Date of Birth is required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Job { get; set; }

     
    }
}