using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SolvintechTestApp.Data;
using SolvintechTestApp.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace SolvintechTestApp.Controllers
{
    [ApiController]
    [Produces("application/json")]

    [Route("api/[controller]")]
    [Route("api/quotation")]
    public class QuotesController : Controller
    {
        public QuotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        private readonly ApplicationDbContext _db;

        [HttpGet]
        public ActionResult<string> GetData(string token, string date)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            ApplicationUser user = _db.ApplicationUser.Where(u => u.Token == token).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }

            if (!DateTime.TryParse(date, out DateTime Date))
            {
                return NotFound();
            }

            var doc = LoadXML(Date);
            if (string.IsNullOrEmpty(doc.InnerText))
            {
                return NotFound();
            }

            return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);

        }

        private XmlDocument LoadXML(DateTime date)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string cbrDate = date.ToString("dd/MM/yyyy").Replace('.', '/');

            string URL = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + cbrDate;
            XmlDocument XmlDocument = new XmlDocument();
            XmlDocument.Load(URL);

            return XmlDocument;
        }
    }
}
