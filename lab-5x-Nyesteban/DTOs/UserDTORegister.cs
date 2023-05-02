namespace lab_5x_Nyesteban.DTOs
{
    public class UserDTORegister
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? RealName { get; set; }
        public string? EMail { get; set; }
        public string? Website { get; set; }
    }
}
