using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Pages;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class Contact : BasePage
    {
        //private void Page_PreInit(object sender, EventArgs e)
        //{
        //    Page.Theme = "Dark";

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            var path = Server.MapPath("~/App_Themes");
            var list = Directory.GetDirectories(path)
                                .Select(folder => new DirectoryInfo(folder).Name)
                                .ToList();
            PreferredColor.DataSource = list;
            PreferredColor.DataBind();

            using (var context = new ApplicationDbContext())
            {
                var currentUserId = Context.User.Identity.GetUserId();
                var user = context.Users.Where(u => u.Id == currentUserId).Single();

                if (user?.Theme != null)
                {
                    PreferredColor.Items.FindByValue(user.Theme).Selected = true;
                }
            }

        }

        protected void SetPreferredColor_Click(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                var currentUserId = Context.User.Identity.GetUserId();
                var user = context.Users.Where(u => u.Id == currentUserId).SingleOrDefault();

                user.Theme = PreferredColor.SelectedItem.Value;
                context.SaveChanges();
            }
        }
    }
}