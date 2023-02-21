using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BtyonFramework.Models
{
    public class LogModel
    {
        [Key]
        public int Id{ get; set; }
        public string Log{ get; set; }
    }
}