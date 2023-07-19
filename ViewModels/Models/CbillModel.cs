using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class CbillModel
    {
        public int CbillId { get; set; }

        public String AccountName { get; set; }

        public string PancardNo { get; set; }

        public double? AadharCardNo { get; set; }

        public string ElectionCardNo { get; set; }

        public string SocietyName {get;set;}

        public string BranchName { get; set; }

        public string BranchCode { get; set; }

        public double? MobileNo { get; set; }

        public string GLNAME{ get; set; }

        public double Amount { get; set; }
       
    }
}
