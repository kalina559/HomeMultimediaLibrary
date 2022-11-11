using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Entities
{
    public class EntityBase
    {
		protected EntityBase()
		{
			DateCreated = DateTime.UtcNow;
		}

		[Key]
		public int Id { get; set; }

		public DateTime DateCreated { get; set; }
	}
}