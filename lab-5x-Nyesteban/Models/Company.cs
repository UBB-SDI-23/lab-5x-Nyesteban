using lab_5x_Nyesteban.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace lab_1_Nyesteban.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyDescription { get; set; }
        public decimal CompanyRevenue { get; set; }
        public int CompanyEstablishmentYear { get; set; }
        public decimal CompanyRating { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
        public virtual ICollection<DevelopmentDetail>? DevelopmentDetails { get; set; }
    }
}
