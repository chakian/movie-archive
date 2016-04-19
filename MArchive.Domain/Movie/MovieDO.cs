using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using MArchive.Domain.Base;
using System.Collections.Generic;

namespace MArchive.Domain.Movie
{
    public class MovieDO : AuditableDO
    {
        public int? Year { get; set; }

        [StringLength(30)]
        public string ImdbID { get; set; }

        public double? ImdbRating { get; set; }

        [StringLength(500)]
        public string ImdbPoster { get; set; }

        public bool ImdbParsed { get; set; }

        public DateTime? ImdbLastParseDate { get; set; }

		//Non-DB properties
		public string ImdbPosterPath {
			get {
				return string.IsNullOrEmpty ( ImdbPoster ) ? "" : ImdbPoster;
			}
		}

		public string ImdbPageUrl {
			get {
				return string.IsNullOrEmpty( ImdbID ) ? "" : "http://www.imdb.com/title/" + ImdbID + "/";
			}
		}

		public string OriginalName { get; set; }

        public MovieDO()
        {
            ImdbRating = null;
            ImdbPoster = null;
            ImdbParsed = false;
            ImdbLastParseDate = null;
        }
    }
}