namespace WebApplication_cctv_website_template.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public Position Position{ get; set; } 
    }
}
