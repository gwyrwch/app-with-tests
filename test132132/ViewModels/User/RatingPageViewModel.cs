using System;
using System.Collections.Generic;
using System.Linq;

namespace test132132.ViewModels.User
{
    public class RatingPageViewModel
    {
        public RatingPageViewModel()
        {
        }

        public List<Tuple<string,int>> SortedUsers()
        {
            return Common.UserBase.GetSortedUsersWithPoints();
        }
    }
}
