using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities.Items
{
    [Table("MagazineItems")]
    public class MagazineItem: ReadingItem
    {
    }
}