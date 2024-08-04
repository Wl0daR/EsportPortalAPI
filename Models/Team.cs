using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }

    [JsonIgnore]
    public ICollection<Player> Players { get; set; }

    [JsonIgnore]
    public ICollection<PlayerHistory> PlayerHistories { get; set; }

    [JsonIgnore]
    public ICollection<TeamTournamentHistory> TeamTournamentHistories { get; set; }
}
