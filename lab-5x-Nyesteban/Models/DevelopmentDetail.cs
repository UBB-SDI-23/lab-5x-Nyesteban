using lab_5x_Nyesteban.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace lab_1_Nyesteban.Models
{
    public class DevelopmentDetail
    { 
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }
        public int AppId { get; set; }
        [JsonIgnore]
        public App? App { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public decimal DevelopmentCosts { get; set; }
        public int DevelopmentTimeInHours { get; set; }
    }
}
