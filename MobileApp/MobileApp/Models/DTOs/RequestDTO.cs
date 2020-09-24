namespace MobileApp.Models.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int DungeonMasterId { get; set; }
        public bool PlayerAccepted { get; set; }
        public bool DungeonMasterAccepted { get; set; }
        public bool Active { get; set; }
        public DungeonMasterDTO DungeonMaster { get; set; }
        public PlayerDTO Player { get; set; }
    }
}
