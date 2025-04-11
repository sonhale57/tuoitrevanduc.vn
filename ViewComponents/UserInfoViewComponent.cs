using Microsoft.AspNetCore.Mvc;
using tuoitrevanduc.Helper;

namespace tuoitrevanduc.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly CheckUser _checkUser;
        public UserInfoViewComponent(CheckUser checkUser)
        {
            _checkUser = checkUser;
        }
        public IViewComponentResult Invoke()
        {
            string username = _checkUser.GetUsername();
            return View("Default", username);
        }
    }
}
