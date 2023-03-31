namespace SocialDB.Domain;

public class Like
{
    public int LikeId { get; set; }
    public int UserId { get; set; }
    public int MessageId { get; set; }
}