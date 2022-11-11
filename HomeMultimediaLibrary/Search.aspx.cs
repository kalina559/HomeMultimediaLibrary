using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class Search: BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                ItemListView.DataSource = context.Items.OrderByDescending(it => it.Id).Take(50).ToList();
                ItemListView.DataBind();
            }
        }
    }
}