using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            try
            {
                string powerBISchema = "Unable to retrieve schema";
                using (var client = new HttpClient())
                {
                    var schemaResult = await client.GetAsync("https://raw.githubusercontent.com/jeffhollan/MSHealthAPI/master/Power%20BI%20Dataset%20Schema.json");
                    powerBISchema = await schemaResult.Content.ReadAsStringAsync();
                    AuthenticationContext AC = new AuthenticationContext("https://login.windows.net/common/oauth2/authorize/");
                    ClientCredential cc = new ClientCredential(ConfigurationManager.AppSettings["clientId"], ConfigurationManager.AppSettings["clientSecret"]);
                    AuthenticationResult ar = await AC.AcquireTokenByAuthorizationCodeAsync(code, new Uri(ConfigurationManager.AppSettings["redirect"].ToLower()), cc);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ar.AccessToken);
                    var createResult = await client.PostAsync("https://api.powerbi.com/v1.0/myorg/datasets?defaultRetentionPolicy=None", new StringContent(powerBISchema, Encoding.UTF8, "application/json"));
                    var resultString = await createResult.Content.ReadAsStringAsync();
                    ViewBag.datasetid = (string)JObject.Parse(resultString)["id"];
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public RedirectResult startauth()
        {
            return new RedirectResult("http://bing.com");
        }
    }
}