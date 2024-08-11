using System;
using System.Collections.Generic;

namespace EsportPortalAPI.Models
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Surname { get; set; }
        public int? TeamId { get; set; }
        public TeamDto Team { get; set; }
        public string Role { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public string FavouriteMap { get; set; }
        public string PhotoUrl { get; set; }
        public List<PlayerHistoryDto> PlayerHistories { get; set; } 
    }
}


public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

