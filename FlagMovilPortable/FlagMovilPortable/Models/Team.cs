namespace FlagMovilPortable.Models
{
    using System.Collections.Generic;

    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public string Initials { get; set; }

        public int LeagueId { get; set; }

        public List<User> Fans { get; set; }

        public string FullLogo
        {
            get
            {
                if (string.IsNullOrEmpty(Logo))
                {
                    return "avatar_shield.png";
                }

                return string.Format("http://flagfootballbackend.azurewebsites.net{0}", Logo.Substring(1));
            }
        }

        public override int GetHashCode()
        {
            return TeamId;
        }
    }
}
