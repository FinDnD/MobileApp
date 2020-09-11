using System.Collections.Generic;

namespace MobileApp.Models.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ImageUrl { get; set; }
        public string CharacterName { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public string ExperienceLevel { get; set; }
        public int GoodAlignment { get; set; }
        public int LawAlignment { get; set; }
        public int? PartyId { get; set; }
        public PartyDTO Party { get; set; }
        public List<RequestDTO> ActiveRequests { get; set; }
    }
}