﻿namespace FlagMovilPortable.Models
{
    using System;

    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserTypeId { get; set; }

        public string Picture { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public int FavoriteTeamId { get; set; }

        public int Points { get; set; }

        public UserType UserType { get; set; }

        public Team FavoriteTeam { get; set; }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public DateTime TokenExpires { get; set; }

        public string Password { get; set; }

        public bool IsRemembered { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public byte[] ImageArray { get; set; }

        public string FullPicture
        {
            get
            {
                if (string.IsNullOrEmpty(Picture))
                {
                    return "avatar_user.png";
                }

                return string.Format("http://flagfootballbackend.azurewebsites.net{0}", Picture.Substring(1));
            }
        }        

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}
