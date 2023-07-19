using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_API.Models;
using WEB_API.Repository.Interface;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace WEB_API.Controllers
{
    [Route("api/Loan")]
    [ApiController]
    public class ImportExcelLoanController : ControllerBase
    {
        protected ViewModels.Models.APIResponse _response;
        private readonly ILoan _loanDbService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public ImportExcelLoanController(ILoan account, IMapper mapper, IUserRepository userRepository)
        {
            _loanDbService = account;
            _mapper = mapper;
            _response = new ViewModels.Models.APIResponse();
            _userRepo = userRepository;
        }

        [HttpGet]
        //[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> getloan([FromQuery(Name = "filterOccupancy")] int? occupancy,
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

                IEnumerable<Loan> AccountList = null;


                if (Role == SD.MasterAdminRole || Role == SD.AdminRole)
                {
                    AccountList = await _loanDbService.GetAllAsync(null, pageSize: pageSize,
                    pageNumber: pageNumber);
                    AccountList = AccountList.Where(t => t.Soc_No == _userRepo.GetUserInfo(UserName).Soc_Id);
                }
                else if (Role == SD.CustomerRole)
                {
                    ApplicationUser userInfo = _userRepo.GetUserInfo(UserName);
                    AccountList = await _loanDbService.GetAllAsync(t => t.Customer_ID == userInfo.Name && t.Soc_No == userInfo.Soc_Id);

                }

                if (!string.IsNullOrEmpty(search))
                {
                    AccountList = AccountList.Where(u => u.AccountHolder_Name.ToLower().Contains(search));
                }
                Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<LoanModel>>(AccountList);
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

        [HttpGet("{CustomerId:int}", Name = "GetLoanAccountDetails")]
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
                var account = await _loanDbService.GetAsync(u => u.LoanId == CustomerId);
                if (account == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<LoanModel>(account);
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
        public async Task<ActionResult<ViewModels.Models.APIResponse>> CreateLoan([FromBody] List<LoanModel> accountModel)
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

                List<LoanModel> returnListOfAccount = new List<LoanModel>();


                var deleteRecords = await _loanDbService.GetAllAsync(u => u.GL_CODE == accountModel[0].GL_CODE && u.Soc_No == accountModel[0].Soc_No);
                if (deleteRecords != null && deleteRecords.Count > 0)
                {
                    foreach (var eachdeletedrecord in deleteRecords)
                    {
                        await _loanDbService.RemoveAsync(eachdeletedrecord);
                    }
                }
                foreach (var eachaccount in accountModel)
                {
                    //if (await _loanDbService.GetAsync(u => u.AccountNo == eachaccount.AccountNo && u.GL_CODE == eachaccount.GL_CODE && u.Customer_ID == u.Customer_ID) != null)
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

                    Loan account = _mapper.Map<Loan>(eachaccount);

                    await _loanDbService.CreateAsync(account);
                    returnListOfAccount.Add(_mapper.Map<LoanModel>(account));
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
