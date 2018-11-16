using System;
using System.IO;

namespace test132132.Common
{
    public static class UserBase
    {
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
    }
}
