using CrystalReportDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CrystalReportDemo.DataLayer
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext():base("SqlConnection")
        {

        }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}