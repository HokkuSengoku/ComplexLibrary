namespace Social.Models
{
    using System;

    public struct User
    {
        public DateTime DateOfBirth { get; set; }

        public int Gender { get; set; }

        public DateTime LastVisit { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }

        public int UserId { get; set; }
    }
}
