namespace EsportPortal.Models
{
    public class TeamTournamentHistory
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int TournamentId { get; set; }
        public Team Team { get; set; }
        public Tournament Tournament { get; set; }
    }
}
