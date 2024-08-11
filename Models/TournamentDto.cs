namespace EsportPortal.Models
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string LogoUrl { get; set; }
    }
}
