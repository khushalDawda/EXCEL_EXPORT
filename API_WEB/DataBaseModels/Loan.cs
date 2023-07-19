﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_WEB.DataBaseModels
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public double AccountNo { get; set; }
        public int? GL_CODE { get; set; }
        public string GL_NAME { get; set; }
        public string AccountHolder_Name { get; set; }
        public string Customer_ID { get; set; }
        public string Branch_Name { get; set; }
        public double Principal { get; set; }
        public double Interest { get; set; }
        public double Penalty { get; set; }
        public double Other { get; set; }
        public double Total { get; set; }
        public double Balance { get; set; }
        public string Mobile_No { get; set; }
        public DateTime? Entry_Date { get; set; }
        public DateTime? Tdate { get; set; }
        public string SOURCE { get; set; }
        public string CHEQUE_NO { get; set; }
        public string PARTICULAR { get; set; }
        public string Extra_1 { get; set; }
        public string Extra_2 { get; set; }
        public string Extra_3 { get; set; }
        public string Extra_4 { get; set; }

    }
}
