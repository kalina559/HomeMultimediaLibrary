﻿using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Models.Entities;
using HomeMultimediaLibrary.Models.Entities.Items;
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

        protected override void OnPreRender(EventArgs e)
        {
            RebindTable();
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // initially just take 50 newest items
                LoadDatabaseTable(50);
                List<String> types = new List<String> { TYPE_ALL, TYPE_BOOK, TYPE_MAGAZINE, TYPE_FILM, TYPE_ALBUM };
                itemTypeDropDown.DataSource = types;
                itemTypeDropDown.DataBind();
            }
        }

        protected void OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }

        protected void OnItemEditing(object sender, ListViewEditEventArgs e)
        {
            ItemListView.EditIndex = e.NewEditIndex;
            ItemListView.DataSource = items;
            RebindTable();
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
            RebindTable();
        }

        protected void OnItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ItemListView.EditIndex = -1;
            RebindTable();
        }

        private void LoadDatabaseTable(int? itemsToTake, IEnumerable<string> names = null, IEnumerable<string> authors = null)
        {
            using (var context = new ApplicationDbContext())
            {
                if (itemsToTake.HasValue)
                {
                    items = context.Items.OrderByDescending(it => it.Id).Take((int)itemsToTake);
                } else
                {
                    items = context.Items.OrderByDescending(it => it.Id);
                }

                items = FilterByType(items);

                items = items
                    .Where(item => names != null ? names.Contains(item.Name) : true)
                    .Where(item => authors != null ? authors.Contains(item.Author) : true)
                    .ToList();

                ItemListView.DataSource = items;
                ItemListView.DataBind();
            }
        }

        private IEnumerable<Item> FilterByType(IEnumerable<Item> items)
        {
            if(itemTypeDropDown.SelectedItem == null)
            {
                return items;
            }
            switch (itemTypeDropDown.SelectedItem.Value)
            {
                case TYPE_ALL:
                    return items;
                case TYPE_BOOK:
                    return items.OfType<BookItem>();
                case TYPE_MAGAZINE:
                    return items.OfType<MagazineItem>();
                case TYPE_FILM:
                    return items.OfType<FilmItem>();
                case TYPE_ALBUM:
                    return items.OfType<AlbumItem>();
                default:
                    throw new Exception("Unknown item type");
            }
        }

        private void RebindTable()
        {
            ItemListView.DataSource = items;
            ItemListView.DataBind();
        }

        protected void OnSearchButtonClick(object sender, EventArgs e)
        {
            DataPager pager = ItemListView.FindControl("DataPager") as DataPager;
            if (pager != null)
            {
                pager.SetPageProperties(0, pager.PageSize, true);
            }

            LoadDatabaseTable(null);
            RebindTable();
        }

        protected void OnSetFiltersClick(object sender, EventArgs e)
        {
            if(setFiltersButton.Text == "Set filters")
            {
                filtersLayout.Visible = true;
                setFiltersButton.Text = "Close filters";

            }
            else if (setFiltersButton.Text == "Close filters")
            {
                filtersLayout.Visible = false;
                setFiltersButton.Text = "Set filters";
            }
        }
    }
}