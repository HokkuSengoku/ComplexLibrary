namespace Social.Models
{
    using System;

    public struct Friend
    {
        public int FromUserId { get; set; }

        public DateTime SendDate { get; set; }

        public int Status { get; set; }

        public int ToUserId { get; set; }
    }
}
