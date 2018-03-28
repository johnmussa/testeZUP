using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TesteZUPJAMP.Models
{
    public class DBPOIContext : DbContext
    {
        public DbSet<POI> PointsOfInterest { get; set; }




    }
}