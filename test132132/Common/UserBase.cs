﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace test132132.Common
{
    public static class UserBase
    {
        const string allUsersPath = "./allUsers.txt";

        public static bool Exists(string username)
        {
            return File.Exists(
                string.Join("",
                     "./",
                     username,
                     ".json"
                )
            );
        }

        static List<Tuple<string, string> > AllUserNames() // name + hash
        {
            if (!File.Exists(allUsersPath))
            {
                return new List<Tuple<string, string>>();
            }
            string[] allUsers = File.ReadAllLines(allUsersPath);
            return allUsers.ToList().Select(
                (arg) =>
                {
                    var pair = arg.Split(' ');
                    return new Tuple<string, string>(pair[0], pair[1]);
                }
            ).ToList();
        }

        static void WriteUser(Models.User user)
        {
            string fileName = string.Join("",
                "./",
                user.UserName,
                 ".json"
            );

            File.WriteAllText(
                fileName,
                JsonConvert.SerializeObject(user)
            );
        }

        public static void NewUser(Models.User newUser, string password)
        {
            App.CurrentUser = newUser;
            WriteUser(newUser);

            var allUsers = AllUserNames();
            allUsers.Add(new Tuple<string, string>(newUser.UserName, CreateMD5(password)));

            string fileContent = "";
            foreach (var userInfo in allUsers)
            {
                string text = string.Join(" ", userInfo.Item1, userInfo.Item2);
                fileContent += text + '\n';
            }
            File.WriteAllText(allUsersPath, fileContent);
        }

        public static void UpdateUser(Models.User user)
        {
            WriteUser(user);
        }

        public static bool GoodPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 5)
                return false;
            return true;
        }

        public static bool TryToLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            var thisUser = new Tuple<string, string>(username, CreateMD5(password));
            if (AllUserNames().Contains(thisUser))
            {
                string fileName = string.Join("",
                     "./",
                     username,
                     ".json"
                );

                string plainText = File.ReadAllText(fileName);
                App.CurrentUser = JsonConvert.DeserializeObject<Models.User>(plainText);

                return true;
            } else
                return false;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static void CreateDanik()
        {
            Models.User danik = new Models.User
            {
                Name = "Danik",
                Surname = "Melnichenka",
                UserName = "melnick",
                Birth = new DateTime(1999, 12, 24),
                Education = "BSU",
                Email = "melnick@gmail.com",
                ProfileImagePath = "Melnick.jpg"
            };

            if (Exists(danik.UserName))
                return;

            NewUser(danik, "12345");
        }
    }
}
