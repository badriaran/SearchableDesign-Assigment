using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SearchableDesign.Models;
using SearchableDesign.Repository.Repository.AccountRepository;
using SearchableDesign.UI.Models;
using System.Diagnostics;
using System.Net.Sockets;
using NuGet.Protocol;
using System.Security.Principal;

namespace SearchableDesign.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository   _accountRepository;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public HomeController(ILogger<HomeController> logger, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            this._accountRepository = accountRepository;
            this._HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(string usercode, string password)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                if (!string.IsNullOrEmpty(usercode))
                {
                    var user = _accountRepository.GetUserProfile(usercode);

                    if (user.Status == "SUCCESS")
                    {
                        _HttpContextAccessor.HttpContext.Session.SetString("UserCode", usercode);
                        
                        //var UserCode = Context.Session.GetString("UserCode");
                        json.Message = user.Message;
                        json.Success = true;
                        json.redirection_url = "ManageEmployee/Index";
                        return Json(json);
                    }
                    else
                    {
                        json.Success = false;
                        json.Message = user.Message;
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                json.Message = ex.Message.ToString();
                json.Success = false;
            }

            return Json(json);
        }
        public ActionResult LogOff()
        {
            _HttpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
