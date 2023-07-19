using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_API.Models;
using WEB_API.Repository.Interface;

namespace WEB_API.Controllers
{

    [Route("api/UsersAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        protected ViewModels.Models.APIResponse _response;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new ViewModels.Models.APIResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestModel model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPost("IsUserPresent")]
        public async Task<IActionResult> IsUserPresent()
        {
            if (_userRepo.AnyUserPresent())
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpGet("GetRole")]
        public async Task<IActionResult> GetRole()
        {
            var rolesList = _userRepo.GetRoles();
            if (rolesList != null)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = rolesList;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo([FromBody] string username)
        {
            var userinfo = _userRepo.GetUserInfo(username);
            if (userinfo != null)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = userinfo;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPost("GetUserInfoGet")]
        public async Task<IActionResult> GetUserInfoPost([FromBody] string username)
        {
            var userinfo = _userRepo.GetUserInfo(username);
            if (userinfo != null)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = userinfo;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPost("GetMaxSocietyId")]
        public async Task<IActionResult> GetMaxSocietyId()
        {
            var maxSocietyId = _userRepo.GetMaxSocietyId();
            if (maxSocietyId != null)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = maxSocietyId;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }




    }
}
