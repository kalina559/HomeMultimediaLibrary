using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Models.Entities;
using HomeMultimediaLibrary.Models.Entities.Items;
using HomeMultimediaLibrary.Pages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class ManageUsers : BasePage
    {
        private static IEnumerable<ApplicationUser> users;
        private static int currentPageStartRowIndex = 0;
        private static int pageSize = 5;

        protected override void OnPreRender(EventArgs e)
        {
            RebindTable();
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserRoles();
            RedirectIfUserNotInRole("admin", "~/Default");

            if (!IsPostBack)
            {
                LoadDatabaseTable();
            }
        }

        protected void OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var currentUser = users.ElementAt(e.Item.DataItemIndex);

            var roleTextbox = e.Item.FindControl("roleTextbox") as TextBox;

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (e.Item.DisplayIndex == UsersListView.EditIndex)
            {
                List<String> types = new List<String> { "admin", "regular" };

                var dropDown = e.Item.FindControl("editRoleDropDownList") as DropDownList;

                dropDown.DataSource = types;
                dropDown.DataBind();
            }
            else
            {
                if (manager.IsInRole(currentUser.Id, "admin"))
                {
                    roleTextbox.Text = "admin";
                }
                else if (manager.IsInRole(currentUser.Id, "regular"))
                {
                    roleTextbox.Text = "regular";
                }
            }
        }

        protected void OnItemEditing(object sender, ListViewEditEventArgs e)
        {
            UsersListView.EditIndex = e.NewEditIndex;
            UsersListView.DataSource = users;
            RebindTable();
        }

        protected void OnItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string userId = users.ElementAt(UsersListView.EditIndex + currentPageStartRowIndex).Id;

            string editItemName = (UsersListView.Items[e.ItemIndex].FindControl("editNameTextBox") as TextBox).Text;
            string newRole = (UsersListView.Items[e.ItemIndex].FindControl("editRoleDropDownList") as DropDownList).SelectedValue;

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);

            user.UserName = editItemName;

            switch (newRole)
            {
                case "admin":
                    manager.AddToRole(user.Id, "admin");
                    break;
                case "regular":
                    if (manager.IsInRole(userId, "admin"))
                    {
                        manager.RemoveFromRoles(user.Id, "admin");
                    }

                    if (!manager.IsInRole(userId, "regular"))
                    {
                        manager.AddToRole(user.Id, "regular");
                    }
                    break;
            }

            manager.Update(user);

            UsersListView.EditIndex = -1;
            LoadDatabaseTable();
            RebindTable();
        }

        protected void OnItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            UsersListView.EditIndex = -1;
            RebindTable();
        }

        protected void OnItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string userId = users.ElementAt(UsersListView.EditIndex + currentPageStartRowIndex).Id;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);

            manager.Delete(user);

            LoadDatabaseTable();
            RebindTable();
        }

        protected void OnPageChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            UsersListView.EditIndex = -1;
            currentPageStartRowIndex = e.StartRowIndex;
        }

        private void LoadDatabaseTable()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            users = manager.Users.ToList();
            UsersListView.DataSource = users;
            UsersListView.DataBind();
        }
        private void RebindTable()
        {
            UsersListView.DataSource = users;
            UsersListView.DataBind();
        }

        protected void OnSearchButtonClick(object sender, EventArgs e)
        {
            DataPager pager = UsersListView.FindControl("DataPager") as DataPager;
            if (pager != null)
            {
                pager.SetPageProperties(0, pager.PageSize, true);
            }

            LoadDatabaseTable();
            RebindTable();
        }
    }
}