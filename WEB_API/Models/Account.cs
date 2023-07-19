using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public double AccountNo { get; set; }
        public int? GL_CODE { get; set; }
        public string GL_NAME { get; set; }
        public string AccountHolder_Name { get; set; }
        public double Balance { get; set; }
        public string Mobile_No { get; set; }
        public string Customer_ID { get; set; }
        public string Society_Name { get; set; }
        public string Branch_Name { get; set; }
        public DateTime? Report_Date { get; set; }
        public string Time { get; set; }

        public string Aadhar_No { get; set; }
        public string Soc_No { get; set; }

    }
}
