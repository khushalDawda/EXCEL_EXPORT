using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_APP.Models;
using WEB_APP.Repository.Interface;

namespace WEB_APP_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;

        public HomeController(ILogger<HomeController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            APIResponse result = await _authService.IsAnyUserPresent<APIResponse>();
            if (result != null && result.IsSuccess)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    //create a master user
                    APIResponse result1 = await _authService.RegisterAsync<APIResponse>(new RegisterationRequestModel
                    {
                        UserName = "crystal",
                        Password = "Crystal@123",
                        Role = SD.MasterAdminRole,
                        Name = "MasterAdmin"

                    });
                    if (result1 != null && result1.IsSuccess)
                    {
                        return View();
                    }
                }

            }

            if (HttpContext.User != null && HttpContext.User.Claims != null && HttpContext.User.Claims.ToList().Count > 0 && HttpContext.User.Claims.LastOrDefault().Value != null)
            {
                APIResponse result1 = await _authService.GetUserInfo<APIResponse>(HttpContext.User.Claims.ToList()[0].Value.ToString());
                if (result1 != null && result1.IsSuccess && result1.StatusCode == System.Net.HttpStatusCode.OK && result1.Result != null)
                {
                    UserData myDeserializedClass = JsonConvert.DeserializeObject<UserData>(result1.Result.ToString());
                    if (myDeserializedClass != null && myDeserializedClass.soc_Name != null)
                    {
                        return View(myDeserializedClass);
                    }
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
