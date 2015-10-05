using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PowerBIHelper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> Authorize(string code)
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.datasetid = "12345-6789";
            string powerBISchema = "Unable to retrieve schema";
            using (var client = new HttpClient())
            {
                var schemaResult = await client.GetAsync("https://raw.githubusercontent.com/jeffhollan/MSHealthAPI/master/Power%20BI%20Dataset%20Schema.json");
                powerBISchema = await schemaResult.Content.ReadAsStringAsync();
            }
            ViewBag.schema = powerBISchema;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}