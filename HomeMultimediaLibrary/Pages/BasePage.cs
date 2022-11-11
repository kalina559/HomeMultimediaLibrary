using HomeMultimediaLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Pages
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(User.Identity.GetUserId());

            if (user?.Theme != null)
            {
                Page.Theme = user.Theme;
            } else
            {
                Page.Theme = "Light";
            }
        }
    }
}