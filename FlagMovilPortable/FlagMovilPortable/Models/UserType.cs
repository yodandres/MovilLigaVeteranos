﻿namespace FlagMovilPortable.Models
{
    using System.Collections.Generic;

    public class UserType
    {
        public int UserTypeId { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }

        public override int GetHashCode()
        {
            return UserTypeId;
        }
    }    
}
