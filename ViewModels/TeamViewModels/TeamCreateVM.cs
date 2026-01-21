namespace WebApplication_cctv_website_template.ViewModels.TeamViewModels
{
    public class TeamCreateVM
    {
      
        public string Name { get; set; } = string.Empty;
        public IFormFile Image { get; set; } 
        public int PositionId { get; set; } 
    }
}
