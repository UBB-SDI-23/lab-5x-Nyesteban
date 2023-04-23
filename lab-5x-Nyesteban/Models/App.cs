namespace lab_1_Nyesteban.Models
{
    public class App
    {
        public int ID { get; set; }
        public string? AppName { get; set; }

        public string? AppVersion { get; set; }

        public string? AppDescription { get; set; }

        public int AppSize { get; set; }

        public decimal AppPrice { get; set; }

        public int AppRating { get; set; }

        public virtual ICollection<DevelopmentDetail>? DevelopmentDetails { get; set; }
    }
}
