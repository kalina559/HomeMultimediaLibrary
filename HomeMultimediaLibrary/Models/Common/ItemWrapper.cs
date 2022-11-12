using HomeMultimediaLibrary.Models.Entities;
using HomeMultimediaLibrary.Models.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Models.Common
{    
    public class ItemWrapper
    {
        protected const string TYPE_BOOK = "Book";
        protected const string TYPE_MAGAZINE = "Magazine";
        protected const string TYPE_FILM = "Film";
        protected const string TYPE_ALBUM = "Album";

        public int Id;
        public string Type;
        public string Name;
        public string Author;
        public int Pages;
        public string ISBN;
        public int LengthMinutes;
        public string TableOfContents;
        public string Summary;
        public string Keywords;
        public string ImageBase64;
        public string Publisher;

        public ItemWrapper()
        {
        }

        public ItemWrapper(Item item)
        {

            ItemWrapper wrapper = new ItemWrapper
            {
                Id = item.Id,
                Name = item.Name,
                Author = item.Author,
                Summary = item.Summary,
                Keywords = item.Keywords,
                ImageBase64 = item.Image?.Base64,
                Publisher = item.Publisher
            };

            if(item is ReadingItem readingItem)
            {
                wrapper.Pages = readingItem.Pages;
                wrapper.ISBN = readingItem.ISBN;

                if(item is BookItem bookItem)
                {
                    wrapper.Type = TYPE_BOOK;
                } else if(item is MagazineItem magazineItem)
                {
                    wrapper.Type = TYPE_MAGAZINE;
                }
            } else if (item is MultimediaItem multimediaItem)
            {
                wrapper.LengthMinutes = multimediaItem.LengthMinutes;

                if (item is FilmItem filmItem)
                {
                    wrapper.Type = TYPE_FILM;
                }
                else if (item is AlbumItem albumItem)
                {
                    wrapper.Type = TYPE_ALBUM;
                }
            }
        }

        public IEnumerable<string> GetKeywords
        {
            get => Keywords != null ? Keywords.Split(',') : Enumerable.Empty<string>();
        }
    }
}