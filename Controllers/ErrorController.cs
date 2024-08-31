using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrudDenemeleri.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult GeneralError()
        {
            ViewBag.ErrorMessage = "Beklenmeyen bir hata oluştu.";
            return View("Error");
        }

        [Route("Error/UnAuthorized")]
        public IActionResult UnAuthorizedError()
        {
            ViewBag.ErrorMessage = "Yetkisiz erişim.";
            return View("UnAuthorized");
        }

        [Route("Error/BadRequest")]
        public IActionResult BadRequestError()
        {
            ViewBag.ErrorMessage = "Geçersiz Parametre.";
            return View("BadRequest");
        }

        [Route("Error/ValidationError")]
        public IActionResult ValidationError()
        {
            ViewBag.ErrorMessage = "Doğrulama Hatası.";
            return View("ValidationError");
        }

        [Route("Error/NotFound")]
        public IActionResult NotFoundError()
        {
            ViewBag.ErrorMessage = "Veri Bulunamadı.";
            return View("NotFound");
        }
    }
}
