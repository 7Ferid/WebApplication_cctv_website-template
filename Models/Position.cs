namespace WebApplication_cctv_website_template.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name {  get; set; }=string.Empty;
        public ICollection<Team> Teams { get; set; }
    }
}
