using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BtyonFramework.Db
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dbConnection")
        {

        }

        public DbSet<Models.ColumsModel> Colums{ get; set; }
        public DbSet<Models.DataModel> Data{ get; set; }
        public DbSet<Models.LogModel> Log{ get; set; }

    }
}