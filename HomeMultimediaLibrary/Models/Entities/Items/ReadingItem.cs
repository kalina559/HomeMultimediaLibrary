using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities.Items
{
    [Table("ReadingItems")]
    public class ReadingItem: Item
    {
        public int Pages { get; set; }
        public string ISBN { get; set; }

    }
}