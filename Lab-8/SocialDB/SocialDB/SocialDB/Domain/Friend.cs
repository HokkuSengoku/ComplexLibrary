namespace SocialDB.Domain;

public class Friend
{
    public int FriendId { get; set; }
    
    public int FromUserId { get; set; }

    public DateTime SendDate { get; set; }

    public int Status { get; set; }

    public int ToUserId { get; set; }
    
    public virtual User User { get; set; }
}