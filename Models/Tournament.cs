public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string LogoUrl { get; set; }

    // Navigation properties
    public ICollection<TeamTournamentHistory> TeamTournamentHistories { get; set; }
}
