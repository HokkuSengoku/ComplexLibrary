namespace SocialDB.Domain;
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    public int AuthorId { get; set; }

    public int MessageId { get; set; }

    public DateTime SendDate { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    [System.ComponentModel.DataAnnotations.MaxLengthAttribute(int.MaxValue)]
    public string Text { get; set; }
    
    public virtual User User { get; set; }
}