using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities
{
    [Table("Items")]
    public class Item: EntityBase
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Issuer { get; set; }

        public virtual Image ImageId { get; set; }

        public string Summary { get; set; }

        public virtual ApplicationUser AddedById { get; set; }
    }
}