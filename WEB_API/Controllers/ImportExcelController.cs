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
using System.IdentityModel.Tokens.Jwt;

namespace WEB_API.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class ImportExcelAPIController : ControllerBase
    {
        protected ViewModels.Models.APIResponse _response;
        private readonly IAccount _accountDbService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;


        public ImportExcelAPIController(IAccount account, IMapper mapper, IUserRepository userRepository)
        {
            _accountDbService = account;
            _mapper = mapper;
            _userRepo = userRepository;
            _response = new ViewModels.Models.APIResponse();
        }

        [HttpGet]
        //[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> GetAccounts([FromQuery(Name = "filterOccupancy")] int? occupancy,
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



                IEnumerable<Account> AccountList = null;

                if (Role != null && Role == SD.MasterAdminRole || Role == SD.AdminRole || Role == "admin")
                {
                    AccountList = await _accountDbService.GetAllAsync(null, pageSize: pageSize,
                        pageNumber: pageNumber);
                    AccountList = AccountList.Where(t => t.Soc_No == _userRepo.GetUserInfo(UserName).Soc_Id);
                }
                else if (Role == SD.CustomerRole)
                {
                    ApplicationUser userInfo = _userRepo.GetUserInfo(UserName);
                    AccountList = await _accountDbService.GetAllAsync(t => t.Customer_ID == userInfo.Name && t.Soc_No == userInfo.Soc_Id);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    AccountList = AccountList.Where(u => u.AccountHolder_Name.ToLower().Contains(search));
                }
                Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<AccountModel>>(AccountList);
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

        //[Authorize]
        [HttpGet("{CustomerId:int}", Name = "GetAccountDetails")]
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
                var account = await _accountDbService.GetAsync(u => u.Id == CustomerId);
                if (account == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<AccountModel>(account);
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
        public async Task<ActionResult<ViewModels.Models.APIResponse>> CreateAccount([FromBody] List<AccountModel> accountModel)
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

                List<AccountModel> returnListOfAccount = new List<AccountModel>();

                //delete the records of society
                var deleteRecords = await _accountDbService.GetAllAsync(u => u.GL_CODE == accountModel[0].GL_CODE && u.Soc_No == accountModel[0].Soc_No);
                if (deleteRecords != null && deleteRecords.Count > 0)
                {
                    foreach (var eachdeletedrecord in deleteRecords)
                    {
                        await _accountDbService.RemoveAsync(eachdeletedrecord);
                    }
                }

                foreach (var eachaccount in accountModel)
                {

                    //if (await _accountDbService.GetAsync(u => u.AccountNo == eachaccount.AccountNo && u.GL_CODE == eachaccount.GL_CODE && u.Soc_No == u.Soc_No) != null)
                    //{
                    //    var model = await _accountDbService.GetAsync(u => u.AccountNo == eachaccount.AccountNo && u.GL_CODE == eachaccount.GL_CODE && u.Soc_No == u.Soc_No);
                    //    model.Balance = eachaccount.Balance;
                    //    await _accountDbService.UpdateAsync(model);
                    //    continue;
                    //    //ModelState.AddModelError("ErrorMessages", "Account no already Exists!");
                    //    //return BadRequest(ModelState);
                    //}

                    if (eachaccount == null)
                    {
                        return BadRequest(eachaccount);
                    }

                    Account account = _mapper.Map<Account>(eachaccount);

                    await _accountDbService.CreateAsync(account);
                    returnListOfAccount.Add(_mapper.Map<AccountModel>(account));
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteAccount")]
        // [Authorize(Roles = "admin")]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> DeleteAccount(int id)
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

                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _accountDbService.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _accountDbService.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
        //[Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ViewModels.Models.APIResponse>> UpdateAccount(int id, [FromBody] AccountModel updateDTO)
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

                if (updateDTO == null)
                {
                    return BadRequest();
                }

                Account model = _mapper.Map<Account>(updateDTO);

                await _accountDbService.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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

        [HttpPatch("{id:int}", Name = "UpdatePartialAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<AccountModel> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _accountDbService.GetAsync(u => u.Id == id, tracked: false);

            AccountModel villaDTO = _mapper.Map<AccountModel>(villa);


            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            Account model = _mapper.Map<Account>(villaDTO);

            await _accountDbService.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
