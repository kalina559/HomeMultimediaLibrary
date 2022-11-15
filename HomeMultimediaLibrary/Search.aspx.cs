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
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class Search : BasePage
    {
        private static IEnumerable<Item> items;
        private static int currentPageStartRowIndex = 0;

        protected override void OnPreRender(EventArgs e)
        {
            RebindTable();
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RedirectIfUserNotInRole("regular", "~/Default");

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
            var currentItem = items.ElementAt(e.Item.DataItemIndex);

            // something wrong here
            var image = e.Item.FindControl("itemImage") as System.Web.UI.WebControls.Image;
            if (image != null)
            {
                image.ImageUrl = currentItem.Image?.Base64;
            }

            var lengthTextBox = e.Item.FindControl("lengthTextBox") as TextBox;
            var typeTextBox = e.Item.FindControl("typeTextBox") as TextBox;
            var ISBNTextBox = e.Item.FindControl("ISBNTextBox") as TextBox;


            if (currentItem is ReadingItem readingItem)
            {
                lengthTextBox.Text = readingItem.Pages.ToString();

                if (ItemListView.EditIndex != e.Item.DisplayIndex)
                {
                    lengthTextBox.Text += " pages";
                }

                ISBNTextBox.Text = readingItem.ISBN; 

                if (currentItem is BookItem)
                {
                    typeTextBox.Text = TYPE_BOOK;
                }
                else if (currentItem is MagazineItem)
                {
                    typeTextBox.Text = TYPE_MAGAZINE;
                }
            }
            else if (currentItem is MultimediaItem multimediaItem)
            {
                ISBNTextBox.Text = "Not applicable";
                lengthTextBox.Text = multimediaItem.LengthMinutes.ToString();

                if (ItemListView.EditIndex != e.Item.DisplayIndex)
                {
                    lengthTextBox.Text += " minutes";
                }


                if (currentItem is FilmItem)
                {
                    typeTextBox.Text = TYPE_FILM;
                }
                else if (currentItem is AlbumItem)
                {
                    typeTextBox.Text = TYPE_ALBUM;
                }
            }

        }

        protected void OnItemEditing(object sender, ListViewEditEventArgs e)
        {
            ItemListView.EditIndex = e.NewEditIndex;
            ItemListView.DataSource = items;
            RebindTable();
        }

        protected void OnItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            int itemId = items.ElementAt(ItemListView.EditIndex + currentPageStartRowIndex).Id;

            string editItemName = (ItemListView.Items[e.ItemIndex].FindControl("editNameTextBox") as TextBox).Text;
            string editAuthor = (ItemListView.Items[e.ItemIndex].FindControl("editAuthorTextBox") as TextBox).Text;
            string editPublisher = (ItemListView.Items[e.ItemIndex].FindControl("editPublisherTextBox") as TextBox).Text;
            string editSummary = (ItemListView.Items[e.ItemIndex].FindControl("editSummaryTextBox") as TextBox).Text;
            string editLength = (ItemListView.Items[e.ItemIndex].FindControl("lengthTextBox") as TextBox).Text;
            string editISBN = (ItemListView.Items[e.ItemIndex].FindControl("ISBNTextBox") as TextBox).Text;

            using (var context = new ApplicationDbContext())
            {
                var item = context.Items.Where(i => i.Id == itemId).Single();
                item.Name = editItemName;
                item.Author = editAuthor;
                item.Publisher = editPublisher;
                item.Summary = editSummary;

                if (item is ReadingItem readingItem)
                {
                    readingItem.Pages = Convert.ToInt32(editLength);
                    readingItem.ISBN = editISBN;
                }
                else if (item is MultimediaItem multimediaItem)
                {
                    multimediaItem.LengthMinutes = Convert.ToInt32(editLength);
                }
                context.SaveChanges();

                items = context.Items
                    .OrderByDescending(it => it.Id)
                    .Take(50)
                    .Include(it => it.Image)
                    .ToList();
            }

            ItemListView.EditIndex = -1;
            RebindTable();
        }

        protected void OnItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ItemListView.EditIndex = -1;
            RebindTable();
        }

        protected void OnItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            int itemId = items.ElementAt(e.ItemIndex + currentPageStartRowIndex).Id;

            using (var context = new ApplicationDbContext())
            {
                var item = context.Items.Where(i => i.Id == itemId).Single();

                context.Items.Remove(item);
                context.SaveChanges();
            }

            LoadDatabaseTable();
            RebindTable();
        }

        protected void OnPageChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ItemListView.EditIndex = -1;
            currentPageStartRowIndex = e.StartRowIndex;
        }

        private void LoadDatabaseTable(int? itemsToTake = null, IEnumerable<string> names = null, IEnumerable<string> authors = null)
        {
            using (var context = new ApplicationDbContext())
            {
                if (itemsToTake.HasValue)
                {
                    items = context.Items
                        .Include(item => item.Image)
                        .OrderByDescending(it => it.Id).Take((int)itemsToTake);
                }
                else
                {
                    items = context.Items
                        .Include(item => item.Image)
                        .OrderByDescending(it => it.Id);
                }

                items = ApplyFilters(items);

                items = items
                    .Where(item => authors != null ? authors.Contains(item.Author) : true)
                    .ToList();

                ItemListView.DataSource = items;
                ItemListView.DataBind();
            }
        }
        private IEnumerable<Item> ApplyFilters(IEnumerable<Item> items)
        {
            var filtered = FilterByType(items);
            filtered = FilterByName(filtered);
            filtered = FilterByAuthor(filtered);
            filtered = FilterByPublisher(filtered);
            filtered = FilterByKeyword(filtered);

            return filtered;
        }

        private IEnumerable<Item> FilterByType(IEnumerable<Item> items)
        {
            if (itemTypeDropDown.SelectedItem == null)
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

        private IEnumerable<Item> FilterByName(IEnumerable<Item> items)
        {
            if (nameTextBox.Text != null && nameTextBox.Text != "")
            {
                var nameArray = ParseStringToArray(nameTextBox.Text);
                return items.Where(item => nameArray.Where(name => item.Name.ToLower().Contains(name.ToLower())).Any());
            }
            else
            {
                return items;
            }
        }

        private IEnumerable<Item> FilterByAuthor(IEnumerable<Item> items)
        {
            if (authorTextBox.Text != null && authorTextBox.Text != "")
            {
                var authorArray = ParseStringToArray(authorTextBox.Text);
                return items.Where(item => authorArray.Where(author => item.Author.ToLower().Contains(author.ToLower())).Any());
            }
            else
            {
                return items;
            }
        }

        private IEnumerable<Item> FilterByKeyword(IEnumerable<Item> items)
        {
            if (keywordsTextBox.Text != null && keywordsTextBox.Text != "")
            {
                var keywordArray = ParseStringToArray(keywordsTextBox.Text);
                return items.Where(item => keywordArray
                .Any(keyword => item.GetKeywords.Any(k => k.Contains(keyword))));
            }
            else
            {
                return items;
            }
        }

        private IEnumerable<Item> FilterByPublisher(IEnumerable<Item> items)
        {
            if (publisherTextBox.Text != null && publisherTextBox.Text != "")
            {
                var publisherArray = ParseStringToArray(publisherTextBox.Text);
                return items.Where(item => publisherArray.Where(publisher => item.Publisher.ToLower().Contains(publisher.ToLower())).Any());
            }
            else
            {
                return items;
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

            LoadDatabaseTable();
            RebindTable();
        }

        protected void OnSetFiltersClick(object sender, EventArgs e)
        {
            if (setFiltersButton.Text == "Set filters")
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

        private IEnumerable<string> ParseStringToArray(string str)
        {
            IEnumerable<string> array = str.Split(',');

            // removing leading and trailing whitespace
            array = array.Select(s => s.Trim());

            return array;
        }
    }
}