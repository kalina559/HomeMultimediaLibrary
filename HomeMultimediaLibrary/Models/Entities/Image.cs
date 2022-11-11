using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities
{
    [Table("Images")]
    public class Image: EntityBase
    {
        public string Base64 { get; set; }
    }
}