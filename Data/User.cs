

namespace AD_CW1_20048632_Amrit_Adhikari_C2.Data
{
    internal class User 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }    // username
        public string PasswordHash { get; set; }    // password
        public Role Role { get; set; }
        public bool HasInitialPassword { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;   // date created
        public Guid CreatedBy { get; set; }
    }
}
