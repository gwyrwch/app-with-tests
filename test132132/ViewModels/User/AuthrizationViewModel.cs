using System;
namespace test132132.ViewModels.User
{
    public class AuthrizationViewModel
    {
        Models.User user;
        public AuthrizationViewModel()
        {
            user = App.GetCurrentUser();
        }
    }
}
