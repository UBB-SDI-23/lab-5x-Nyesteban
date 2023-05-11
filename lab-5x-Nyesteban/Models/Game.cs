using lab_5x_Nyesteban.Models;

namespace lab_1_Nyesteban.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string? GameName { get; set; }
        public string? GameDescription { get; set; }
        public int GameLength { get; set; }
        public int GameSize { get; set; }
        public decimal GameRating { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
    }
}
