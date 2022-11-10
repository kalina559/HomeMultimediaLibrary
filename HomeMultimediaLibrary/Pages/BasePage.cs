using HomeMultimediaLibrary.Models;
using Microsoft.AspNet.Identity;
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
            using (var context = new ApplicationDbContext())
            {
                var currentUserId = Context.User.Identity.GetUserId();
                var user = context.Users.Where(u => u.Id == currentUserId).SingleOrDefault();

                if (user?.Theme != null)
                {
                    Page.Theme = user.Theme;
                }
            }
        }
    }
}