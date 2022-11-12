using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Models.Entities;
using HomeMultimediaLibrary.Models.Entities.Items;
using HomeMultimediaLibrary.Pages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class AddItem : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<String> types = new List<String> { TYPE_BOOK, TYPE_MAGAZINE, TYPE_FILM, TYPE_ALBUM };
                itemTypeDropDown.DataSource = types;
                itemTypeDropDown.DataBind();
                ShowFieldsForItemType();
            }
        }
        protected void OnItemTypeChanged(object sender, EventArgs e)
        {
            ShowFieldsForItemType();
        }

        protected void AddItemClick(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(User.Identity.GetUserId());

            using (var context = new ApplicationDbContext())
            {
                switch (itemTypeDropDown.SelectedItem.Value)
                {
                    case TYPE_BOOK:
                        AddBookItem(context, user);
                        break;
                    case TYPE_MAGAZINE:
                        AddMagazineItem(context, user);
                        break;
                    case TYPE_FILM:
                        AddFilmItem(context, user);
                        break;
                    case TYPE_ALBUM:
                        AddAlbumItem(context, user);
                        break;
                    default:
                        throw new Exception("Unknown item type");
                }

                context.SaveChanges();
                ClearInputs(Page.Controls);
            }
        }

        private void ShowFieldsForItemType()
        {
            var type = itemTypeDropDown.SelectedItem.Value;
            if (IsReadingItemType(type))
            {
                pagesTextBox.Visible = true;
                pagesLabel.Visible = true;
                lengthMinutesTextBox.Visible = false;
                lengthMinutesLabel.Visible = false;
                ISBNTextBox.Visible = true;
                ISBNLabel.Visible = true;
            }
            else if (IsMultimediaItemType(type))
            {
                pagesTextBox.Visible = false;
                pagesLabel.Visible = false;
                lengthMinutesTextBox.Visible = true;
                lengthMinutesLabel.Visible = true;
                ISBNTextBox.Visible = false;
                ISBNLabel.Visible = false;
            }
            else
            {
                throw new Exception("Unknown item type");
            }
        }

        private bool IsReadingItemType(string type)
        {
            return type == TYPE_BOOK || type == TYPE_MAGAZINE;
        }

        private bool IsMultimediaItemType(string type)
        {
            return type == TYPE_FILM || type == TYPE_ALBUM;
        }

        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
        }

        private void AddBookItem(ApplicationDbContext context, ApplicationUser user)
        {
            BookItem item = new BookItem
            {
                Name = nameTextBox.Text,
                Author = authorTextBox.Text,
                Publisher = publisherTextBox.Text,
                Summary = summaryTextBox.Text,
                AddedByUserId = user.Id,
                ISBN = ISBNTextBox.Text,
                Pages = Convert.ToInt32(pagesTextBox.Text),
                Keywords = keywordsTextBox.Text
            };

            context.BookItems.Add(item);
        }

        private void AddMagazineItem(ApplicationDbContext context, ApplicationUser user)
        {
            MagazineItem item = new MagazineItem
            {
                Name = nameTextBox.Text,
                Author = authorTextBox.Text,
                Publisher = publisherTextBox.Text,
                Summary = summaryTextBox.Text,
                AddedByUserId = user.Id,
                ISBN = ISBNTextBox.Text,
                Pages = Convert.ToInt32(pagesTextBox.Text),
                Keywords = keywordsTextBox.Text
            };

            context.MagazineItems.Add(item);
        }

        private void AddFilmItem(ApplicationDbContext context, ApplicationUser user)
        {
            FilmItem item = new FilmItem
            {
                Name = nameTextBox.Text,
                Author = authorTextBox.Text,
                Publisher = publisherTextBox.Text,
                Summary = summaryTextBox.Text,
                AddedByUserId = user.Id,
                LengthMinutes = Convert.ToInt32(lengthMinutesTextBox.Text),
                Keywords = keywordsTextBox.Text
            };

            context.FilmItems.Add(item);
        }

        private void AddAlbumItem(ApplicationDbContext context, ApplicationUser user)
        {
            AlbumItem item = new AlbumItem
            {
                Name = nameTextBox.Text,
                Author = authorTextBox.Text,
                Publisher = publisherTextBox.Text,
                Summary = summaryTextBox.Text,
                AddedByUserId = user.Id,
                LengthMinutes = Convert.ToInt32(lengthMinutesTextBox.Text),
                Keywords = keywordsTextBox.Text
            };

            context.AlbumItems.Add(item);
        }
    }
}