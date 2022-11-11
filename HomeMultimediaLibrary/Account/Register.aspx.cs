using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Pages;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeMultimediaLibrary.Account
{
    public partial class Register: BasePage
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text, Theme = "Light" };

            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                var grantRoleResult = manager.AddToRole(user.Id, "regular");

                if (grantRoleResult.Succeeded)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    ErrorMessage.Text = grantRoleResult.Errors.FirstOrDefault();
                }
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}