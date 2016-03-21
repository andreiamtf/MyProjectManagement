using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMA.Models
{
    public class Project
    {
        public int ProjectId {get; set;}
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
    }
}