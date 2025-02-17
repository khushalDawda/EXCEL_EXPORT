﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_APP.Repository.Interface;

namespace WEB_APP.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositService _depositService;
        private readonly IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;


        public DepositController(IDepositService villaService, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _depositService = villaService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<IActionResult> Import()
        {
            APIResponse aPIResponse = new APIResponse();
            DataTable dtTable = new DataTable();
            List<DepositModel> rowList = new List<DepositModel>();
            IFormFile file = Request.Form.Files[0];
            string folderName = "UploadExcel";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table table-bordered'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        dtTable.Columns.Add(new DataColumn(cell.ToString(), typeof(string)));
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        DepositModel accountModel = new DepositModel();
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (j == 11)
                                {
                                    var item = row.GetCell(11).DateCellValue.TimeOfDay;
                                    sb.Append("<td>" + item + "</td>");
                                }
                                else
                                    sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                            }
                        }
                        try
                        {
                            accountModel.AccountNo = string.IsNullOrEmpty(row.GetCell(1).ToString()) ? 0 : Convert.ToDouble(row.GetCell(1).ToString());
                            accountModel.GL_CODE = string.IsNullOrEmpty(row.GetCell(2).ToString()) ? 0 : Convert.ToInt32(row.GetCell(2).ToString());
                            accountModel.GL_NAME = string.IsNullOrEmpty(row.GetCell(3).ToString()) ? null : Convert.ToString(row.GetCell(3).ToString());
                            accountModel.Report_Date = string.IsNullOrEmpty(row.GetCell(10).ToString()) ? DateTime.Now : Convert.ToDateTime(row.GetCell(10).ToString());
                            accountModel.Time = string.IsNullOrEmpty(row.GetCell(11).ToString()) ? DateTime.Now.ToString("t") : row.GetCell(11).DateCellValue.TimeOfDay.ToString();
                            try
                            {

                                // accountModel.Source = row.GetCell(5) == null ? "" : string.IsNullOrEmpty(row.GetCell(5).ToString()) ? null : Convert.ToString(row.GetCell(5).ToString());
                            }
                            catch (Exception ex)
                            {
                                // accountModel.Source = null;
                            }
                            //accountModel.CHEQUE_NO = row.GetCell(6) == null ? "" : string.IsNullOrEmpty(row.GetCell(6).ToString()) ? null : Convert.ToString(row.GetCell(6).ToString());
                            // accountModel.PARTICULAR = row.GetCell(7) == null ? "" : string.IsNullOrEmpty(row.GetCell(7).ToString()) ? null : Convert.ToString(row.GetCell(7).ToString());
                            accountModel.AccountHolder_Name = row.GetCell(4) == null ? "" : string.IsNullOrEmpty(row.GetCell(4).ToString()) ? null : Convert.ToString(row.GetCell(4).ToString());
                            accountModel.Balance = string.IsNullOrEmpty(row.GetCell(5).ToString()) ? 0 : Convert.ToDouble(row.GetCell(5).ToString());
                            accountModel.Mobile_No = string.IsNullOrEmpty(row.GetCell(6).ToString()) ? "" : Convert.ToString(row.GetCell(6).ToString());
                            accountModel.Customer_ID = string.IsNullOrEmpty(row.GetCell(7).ToString()) ? "" : Convert.ToString(row.GetCell(7).ToString());
                            accountModel.Society_Name = string.IsNullOrEmpty(row.GetCell(8).ToString()) ? null : Convert.ToString(row.GetCell(8).ToString());
                            accountModel.Branch_Name = string.IsNullOrEmpty(row.GetCell(9).ToString()) ? null : Convert.ToString(row.GetCell(9).ToString());
                            accountModel.Aadhar_No = string.IsNullOrEmpty(row.GetCell(12).ToString()) ? "" : Convert.ToString(row.GetCell(12).ToString());
                            accountModel.Soc_No = string.IsNullOrEmpty(row.GetCell(13).ToString()) ? "" : Convert.ToString(row.GetCell(13).ToString());

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        rowList.Add(accountModel);
                        sb.AppendLine("</tr>");
                    }




                    sb.Append("</table>");
                    aPIResponse.Result = rowList;
                    aPIResponse.StatusCode = HttpStatusCode.OK;
                    aPIResponse.ErrorMessages.Add(sb.ToString());
                }
            }
            return Ok(aPIResponse);
        }

        public async Task<IActionResult> Index()
        {
            List<DepositModel> list = new List<DepositModel>();

            var response = await _depositService.GetAllAsync<APIResponse>(HttpContext.User.Claims.LastOrDefault().Value);//await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DepositModel>>(Convert.ToString(response.Result));
                if (list != null && list.Count > 0)
                {
                    double total = 0;
                    foreach (var eachloanAccount in list)
                    {
                        total += eachloanAccount.Balance;
                    }
                    list.LastOrDefault().SumOfCustomerBal = total.ToString();
                }
                ViewData["lastupdatedon"] = list != null && list.Count > 0 ? Convert.ToDateTime(list.LastOrDefault().Report_Date).ToString("dd/MMM/yyyy") + " " + list.LastOrDefault().Time : "";
            }
            return View(list);
        }

        public async Task<APIResponse> Create(string listOfAccounts)
        {
            List<DepositModel> AccountList = JsonConvert.DeserializeObject<List<DepositModel>>(listOfAccounts);

            var response = await _depositService.CreateAsync<APIResponse>(AccountList, HttpContext.User.Claims.LastOrDefault().Value);//await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                //list = JsonConvert.DeserializeObject<List<AccountModel>>(Convert.ToString(response.Result));
            }
            return response;
        }

    }
}
