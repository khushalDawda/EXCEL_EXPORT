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
    public class AccountService : BaseService, IAccountService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string AccountUrl;

        public AccountService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            AccountUrl = configuration.GetValue<string>("ServiceUrls:ExcelAPI");

        }

        public Task<T> CreateAsync<T>(List<AccountModel> dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url =  AccountUrl + "/api/Account",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = AccountUrl + "/api/Account/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = AccountUrl + "/api/Account",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = AccountUrl + "/api/Account/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(AccountModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = AccountUrl + "/api/Account/" + dto.Id,
                Token = token
            });
        }
    }
}
