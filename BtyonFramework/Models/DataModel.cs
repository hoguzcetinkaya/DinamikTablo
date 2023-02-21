using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BtyonFramework.Models
{
    public class DataModel
    {
        [Key]
        public int Id { get; set; }

        public string Data { get; set; }
    }
}