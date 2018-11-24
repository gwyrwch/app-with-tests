using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace test132132.Models
{
    public class User
    {
        static readonly Regex nameRegex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");
        static readonly Regex nickRegex = new Regex(@"^[a-zA-Z0-9_]+$");
        static readonly Regex emailRegex = new Regex(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
        );

        public User()
        {
            name = "aklsd";
            Surname = "Melnichenka";
            Education = "BSU";
            UserName = "guest";
            Email = "bla@mail.ru";
            Birth = new DateTime(1990, 12, 31);
            ProfileImagePath = "tab_about.png";
            Stats = new UserStatistics();
        }

        //public User(
        //        string name,
        //        string surname,
        //        DateTime birth,
        //        string education,
        //        string userName,
        //        string email,
        //        string profileImagePath,
        //        UserStatistics stats = null
        //) {
        //    Name = name;
        //    Surname = surname;
        //    Birth = birth;
        //    Education = education;
        //    UserName = userName;
        //    Email = email;
        //    ProfileImagePath = profileImagePath;

        //    //todo stats
        //}

        string name;
        public string Name 
        {
            get => name;
            set
            {
                if (!nameRegex.IsMatch(name))
                    throw new Exception("Invalid Name");

                name = value;
            }
        }

        string surname;
        public string Surname {
            get => surname;
            set
            {
                if (!nameRegex.IsMatch(name))
                    throw new Exception("Invalid Surname");
                surname = value;
            }
        }

        DateTime birth;
        public DateTime Birth {
            get => birth;
            set {
                //DateTime.Now - DateTime.
                DateTime today = DateTime.Today;
                DateTime fiveYearsAgo = today.AddYears(-5);
                if (value > fiveYearsAgo)
                    throw new Exception("You are too young to use this app");
                birth = value;
            }
        } 
        public string Education { get; set; }

        string username;
        public string UserName {
            get => username;
            set {
                if (!nickRegex.IsMatch(value))
                    throw new Exception("Invalid username");
                username = value;
            }
        }

        string email;
        public string Email {
            get => email;
            set {
                if (!emailRegex.IsMatch(value))
                    throw new Exception("Invalid email");
                email = value;
            }
        }     
        public string ProfileImagePath { get; set; }    //todo??
        public UserStatistics Stats { get; set; }
    }
}
