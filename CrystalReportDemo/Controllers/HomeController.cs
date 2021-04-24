using CrystalDecisions.CrystalReports.Engine;
using CrystalReportDemo.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrystalReportDemo.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext databaseContext = new DatabaseContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(databaseContext.Employees.ToList());
        }

        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "CrystalReport.rpt"));
            rd.SetDataSource(databaseContext.Employees.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Employee_List.pdf");
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public ActionResult TestWithModel()
        {
            var list = databaseContext.Employees.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "TestWithModel.rpt"));
            rd.SetDataSource(list);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelRecord);
                stream.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/pdf", "Employee_List_Model.pdf");//"application/excel
                //return File(stream, "application/excel", "Employee_List_Model.xlsx");
                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "eNtsaRegistrationForm.xls");
                //return File(stream, "application/vnd.ms-excel", "eNtsaRegistrationForm.xls");
                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "eNtsaRegistrationForm.xlsx");
                return File(stream, "application/vnd.ms-excel", "eNtsaRegistrationForm..xls");
                 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}