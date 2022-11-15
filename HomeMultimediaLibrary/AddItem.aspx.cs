using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Models.Entities;
using HomeMultimediaLibrary.Models.Entities.Items;
using HomeMultimediaLibrary.Pages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
            GetUserRoles();
            RedirectIfUserNotInRole("admin", "~/Default");

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

            imagePreview.ImageUrl = "";
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
                Keywords = keywordsTextBox.Text,
                Image = imagePreview.ImageUrl != null ? new Models.Entities.Image { Base64 = imagePreview.ImageUrl } : null
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
                Keywords = keywordsTextBox.Text,
                Image = imagePreview.ImageUrl != null ? new Models.Entities.Image { Base64 = imagePreview.ImageUrl } : null
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
                Keywords = keywordsTextBox.Text,
                Image = imagePreview.ImageUrl != null ? new Models.Entities.Image { Base64 = imagePreview.ImageUrl } : null
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
                Keywords = keywordsTextBox.Text,
                Image = imagePreview.ImageUrl != null ? new Models.Entities.Image { Base64 = imagePreview.ImageUrl } : null
            };

            context.AlbumItems.Add(item);
        }

        protected void OnImagePreviewClick(object sender, EventArgs e)
        {
            if (imageFileUpload.FileBytes != null && imageFileUpload.FileBytes.Count() > 0)
            {
                byte[] byteArray;
                using (MemoryStream ms = new MemoryStream(imageFileUpload.FileBytes))
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    var resizedImage = resizeImage(image, 400);

                    ImageConverter _imageConverter = new ImageConverter();
                    byte[] xByte = (byte[])_imageConverter.ConvertTo(resizedImage, typeof(byte[]));
                    byteArray = xByte;

                    ms.Close();
                }

                imagePreview.ImageUrl = "data:image;base64," + Convert.ToBase64String(byteArray);
            }
        }

        public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, int newHeight)
        {
            double aspectRatio = (double)imgToResize.Width / imgToResize.Height;
            int newWidth = (int)Math.Ceiling(newHeight * aspectRatio);
            return (System.Drawing.Image)(new Bitmap(imgToResize, new Size(newWidth, newHeight)));
        }
    }
}