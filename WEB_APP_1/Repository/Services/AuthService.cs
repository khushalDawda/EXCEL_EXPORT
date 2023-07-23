using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_APP.Repository.Interface;

namespace WEB_APP.Repository.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:ExcelAPI");

        }
        public Task<T> LoginAsync<T>(LoginRequestModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/UsersAuth/register"
            });
        }

        public Task<T> IsAnyUserPresent<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = null,
                Url = villaUrl + "/api/UsersAuth/IsUserPresent"
            });
        }

        public Task<T> GetRoles<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Data = null,
                Url = villaUrl + "/api/UsersAuth/GetRole"
            });
        }

        public Task<T> GetUserInfo<T>(string username)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = username,
                Url = villaUrl + "/api/UsersAuth/GetUserInfoGet"
            });
        }

        public Task<T> GetAllUsers<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/UsersAuth/GetAllUser"
            });
        }

        public Task<T> GetMaxSocietyId<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = null,
                Url = villaUrl + "/api/UsersAuth/GetMaxSocietyId"
            });
        }
    }
}
