using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace test132132.Common
{
    public static class Mappers
    {
        public static List<string> AvaliableTimeModes() {
            List<string> modeNames = Enum.GetNames(typeof(Models.TimeMode)).ToList();
            return modeNames.Select(s => {
                s = char.ToUpper(s[0]) + s.Substring(1);
                return string.Join(
                    " ",
                    Regex.Split(s, @"(?<!^)(?=[A-Z])")
                );
            }).ToList();
        }
    }
}
