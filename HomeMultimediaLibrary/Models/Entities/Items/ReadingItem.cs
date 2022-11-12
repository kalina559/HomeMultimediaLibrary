using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities.Items
{
    [Table("ReadingItems")]
    public class ReadingItem: Item
    {
        [Required(AllowEmptyStrings = false)]
        public int Pages { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ISBN { get; set; }

    }
}