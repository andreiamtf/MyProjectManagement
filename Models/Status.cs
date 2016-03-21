using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMA.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        //[Required(ErrorMessage = "The Status is required")]
        //[MaxLength(20)]
        public string StatusName { get; set; }
    }
}