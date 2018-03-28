using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteZUPJAMP.Models
{
    public class POI
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        
    }
}