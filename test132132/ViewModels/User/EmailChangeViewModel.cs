using System;
namespace test132132.ViewModels.User
{
    public class EmailChangeViewModel : MyBaseViewModel
    {
        public Models.User CurrentUser { get; set; }
        public EmailChangeViewModel(Models.User user = null)
        {
            CurrentUser = App.GetCurrentUser();
        }
    }
}
