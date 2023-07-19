using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    [Route("api/Cbill")]
    [ApiController]
    public class ImportExcelCbillController : ControllerBase
    {
        protected ViewModels.Models.APIResponse _response;
        private readonly ICbill _cbillDbService;
        private readonly IMapper _mapper;

        public ImportExcelCbillController(ICbill account, IMapper mapper)
        {
            _cbillDbService = account;
            _mapper = mapper;
            _response = new ViewModels.Models.APIResponse();
        }


        [HttpGet]
        //[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> getcbill([FromQuery(Name = "filterOccupancy")] int? occupancy,
            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                string Role = null;
                string UserName = null;
                var TokenValidations = WEB_API.Helpers.Helpers.TokenValidation(HttpContext.Request.Headers["Bearer"]);
                if (TokenValidations.Key != null && TokenValidations.Value == true)
                {
                    var list = TokenValidations.Key.Split("|");
                    Role = list[0];
                    UserName = list[1];
                }
                else if (TokenValidations.Value == false)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { "Session Expire" };
                    return _response;
                }

                IEnumerable<Cbill> AccountList = null;

                if (Role != null && Role == SD.MasterAdminRole || Role == SD.AdminRole || Role == "admin")
                {
                    AccountList = await _cbillDbService.GetAllAsync(null, pageSize: pageSize,
                        pageNumber: pageNumber);
                }


                if (!string.IsNullOrEmpty(search))
                {
                    AccountList = AccountList.Where(u => u.AccountName.ToLower().Contains(search));
                }
                Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<CbillModel>>(AccountList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpGet("{CustomerId:int}", Name = "GetCbillAccountDetails")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> GetAccounts(int CustomerId)
        {
            try
            {
                if (CustomerId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var account = await _cbillDbService.GetAsync(u => u.CbillId == CustomerId);
                if (account == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CbillModel>(account);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> CreateDeposit([FromBody] List<CbillModel> accountModel)
        {
            try
            {
                string Role = null;
                string UserName = null;
                var TokenValidations = WEB_API.Helpers.Helpers.TokenValidation(HttpContext.Request.Headers["Bearer"]);
                if (TokenValidations.Key != null && TokenValidations.Value == true)
                {
                    var list = TokenValidations.Key.Split("|");
                    Role = list[0];
                    UserName = list[1];
                }
                else if (TokenValidations.Value == false)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { "Session Expire" };
                    return _response;
                }

                List<CbillModel> returnListOfAccount = new List<CbillModel>();
                var records = await _cbillDbService.GetAllAsync();
                if (records != null && records.Count > 0)
                {
                    await _cbillDbService.DeleteDataAsync(records.ToList());
                }
                foreach (var eachaccount in accountModel)
                {
                    //if (await _cbillDbService.GetAsync(u => u.GLNAME == g && u.GL_CODE == eachaccount.GL_CODE && u.Customer_ID == u.Customer_ID) != null)
                    //{
                    //    var model = await _loanDbService.GetAsync(u => u.AccountNo == eachaccount.AccountNo && u.GL_CODE == eachaccount.GL_CODE && u.Customer_ID == u.Customer_ID);
                    //    model.Balance = eachaccount.Balance;
                    //    await _loanDbService.UpdateAsync(model);
                    //    continue;
                    //    //ModelState.AddModelError("ErrorMessages", "Account no already Exists!");
                    //    //return BadRequest(ModelState);
                    //}

                    if (eachaccount == null)
                    {
                        return BadRequest(eachaccount);
                    }

                    Cbill account = _mapper.Map<Cbill>(eachaccount);

                    await _cbillDbService.CreateAsync(account);
                    returnListOfAccount.Add(_mapper.Map<CbillModel>(account));
                    _response.StatusCode = HttpStatusCode.Created;

                    // return CreatedAtRoute("GetAccountDetails", new { CustomerId = eachaccount.Customer_ID }, _response);
                }
                _response.IsSuccess = true;
                _response.Result = returnListOfAccount;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}
