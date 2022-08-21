using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDaiDuong_151901766.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound_404()
        {
            return View();
        }
    }
}