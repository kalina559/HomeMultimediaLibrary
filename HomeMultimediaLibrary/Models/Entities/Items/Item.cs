using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities
{
    [Table("Items")]
    public class Item : EntityBase
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Author { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Publisher { get; set; }

        public virtual Image ImageId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Summary { get; set; }

        public string Keywords { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId
        {
            get; set;
        }

        [NotMapped]
        public IEnumerable<string> GetKeywords
        {
            get => Keywords != null ? Keywords.Split(',') : Enumerable.Empty<string>();
        }
    }
}