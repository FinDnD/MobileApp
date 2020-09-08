using System.Collections.Generic;

namespace MobileApp.Models.DTOs
{
    public class DungeonMasterDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDesc { get; set; }
        public string ExperienceLevel { get; set; }
        public string PersonalBio { get; set; }
        public string ImageUrl { get; set; }
        public PartyDTO Party { get; set; }
        public List<RequestDTO> ActiveRequests { get; set; }
    }
}