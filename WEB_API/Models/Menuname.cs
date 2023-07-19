using System.ComponentModel.DataAnnotations;

namespace WEB_API.Models
{
    public class AccessibilityMenu
    {
        [Key]
        public int Accessbility_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Status { get; set; }
    }
}
