using System.Collections.Generic;
using MArchive.Domain.Movie;
using MArchive.Web.Filtering;
using MArchive.Domain.Lookup;

namespace MArchive.Web.Models.Browse {
	public class BrowseViewModel {
		public List<MovieDO> MovieList { get; set; }

		public int CurrentPage { get; set; }

        //Sorting
        //public string SortBy { get; set; }
        //public bool SortAscending { get; set; }

        //Filtering
        public FiltersForPage Filters { get; set; }
        public List<TypeDO> MovieTypes { get; set; }

        //public string MovieTypesForPage { get; set; }
	}
}