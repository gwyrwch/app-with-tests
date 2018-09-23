using System;
namespace test132132.Common
{
    public static class MyInt
    {
        public static int? Parse(string s) {
            if (string.IsNullOrEmpty(s))
                return null;
            return int.Parse(s);
        }
    }
}
