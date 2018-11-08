using System;
using System.ComponentModel;

namespace test132132.Models
{
    public class User 
    {
        public User()
        {
            Name = "";
            Surname = "";
            Education = "";
            UserName = "guest";
            Email = "";
            Birth = new DateTime(1990, 12, 31);
            ProfileImagePath = "tab_about.png";
            Stats = new UserStatistics();
        }

        public string Name { get; set; }    
        public string Surname { get; set; } 
        public DateTime Birth { get; set; } 
        public string Education { get; set; } 
        public string UserName { get; set; }    
        public string Email { get; set; }     
        public string ProfileImagePath { get; set; }    //todo??
        public UserStatistics Stats { get; set; }


    }
}
