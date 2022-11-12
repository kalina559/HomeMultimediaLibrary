using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Models.Entities;
using HomeMultimediaLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class Search : BasePage
    {
        private static IEnumerable<Item> items;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetDatabaseTable();
        }

        protected void OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }

        protected void OnItemEditing(object sender, ListViewEditEventArgs e)
        {
            ItemListView.EditIndex = e.NewEditIndex;
            ItemListView.DataSource = items;
            rebindTable();
        }

        protected void OnItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string editItemId = (ItemListView.Items[e.ItemIndex].FindControl("editIdText") as TextBox).Text;
            int itemId = Convert.ToInt32(editItemId);

            string editItemName = (ItemListView.Items[e.ItemIndex].FindControl("editNameText") as TextBox).Text;
            string editAuthor = (ItemListView.Items[e.ItemIndex].FindControl("editAuthorText") as TextBox).Text;


            using (var context = new ApplicationDbContext())
            {
                var item = context.Items.Where(i => i.Id == itemId).Single();
                item.Name = editItemName;
                item.Author = editAuthor;
                context.SaveChanges();

                items = context.Items.OrderByDescending(it => it.Id).Take(50).ToList();
            }

            ItemListView.EditIndex = -1;
            rebindTable();
        }

        protected void OnItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ItemListView.EditIndex = -1;
            rebindTable();
        }

        private void GetDatabaseTable()
        {
            if (!IsPostBack)
            {
                using (var context = new ApplicationDbContext())
                {
                    items = context.Items.OrderByDescending(it => it.Id).Take(50).ToList();
                    ItemListView.DataSource = items;
                    ItemListView.DataBind();
                }
            }
        }

        private void rebindTable()
        {
            ItemListView.DataSource = items;
            ItemListView.DataBind();
        }
    }
}