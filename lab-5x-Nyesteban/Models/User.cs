using lab_1_Nyesteban.Models;

namespace lab_5x_Nyesteban.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string RealName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int ShowCount { get; set; }
        public virtual ICollection<Company>? Companies { get; set; }
        public virtual ICollection<App>? Apps { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
        public virtual ICollection<DevelopmentDetail>? DevelopmentDetails { get; set; }
    }
}
