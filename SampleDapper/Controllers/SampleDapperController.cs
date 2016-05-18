using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SampleBO;
using SampleDAL;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace SampleDapper.Controllers
{
    public class SampleDapperController : Controller
    {
        // GET: SampleDapper
        public ActionResult Index()
        {
            return Content("Hello World !");
        }

        public JsonResult GetPengarang()
        {
            PengarangDAL service = new PengarangDAL();
            var results = service.GetAllData();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetBooks()
        {
            using (BooksDAL service = new BooksDAL())
            {
                //no join
                //var results = service.GetAll();

                //with pengarang
                //var results = service.GetAll(false,true);

                //with category
                //var results = service.GetAll(false, false, true);

                //with both
                var results = service.GetAll(true);

                return Json(results, JsonRequestBehavior.AllowGet);
            }
        }

    }
}