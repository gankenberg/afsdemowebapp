using InfoClima;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "HomeController Trace code.";
            System.Diagnostics.Trace.TraceInformation(string.Format("User goes through Index method"));
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            InfoClimaClient clientAPI = new InfoClimaClient(new Uri("http://52.224.182.235"), new Microsoft.Rest.BasicAuthenticationCredentials());
            var results = await clientAPI.ApiSampleDataWeatherForecastsGetAsync();
            ViewBag.Weather = results;
            return View();
        }

        public ActionResult Contact()
        {
            TelemetryClient tc = new TelemetryClient();
            Random rnd = new Random();
            try
            {
                if (rnd.Next(1, 10) > 6)
                {
                    throw new Exception("Excepción aleatoria en Random");
                }
                ViewBag.Message = "Your contact page.";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Your contact page. Ocurrió un error... se generá un item en App Insights";
                tc.TrackException(ex);
                throw;
            }

            return View();
        }
    }
}
