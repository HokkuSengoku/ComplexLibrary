namespace Social.Models
{
    using System.Collections.Generic;

    public struct News
    {
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public List<int> Likes { get; set; }

        public string Text { get; set; }
    }
}
