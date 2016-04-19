using System.Collections.Generic;
using System.Web.Mvc;
using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieUserRatingDO : MovieInfoDO {
		public int UserID { get; set; }

		public bool Watched { get; set; }

		public int Rating { get; set; }

		public string UserName { get; set; }
        
        //Non-DB properties
        public IEnumerable<SelectListItem> RatingList {
			get {
				string selectedValue = ( Watched && Rating > -1 ) ? Rating.ToString() : "-1";

				List<SelectListItem> listItems = new List<SelectListItem>( );
				listItems.Add( new SelectListItem( ) { Text = "Not Rated", Value = "-1" } );
				for( int i = 0; i <= 11; i++ ) {
					listItems.Add( new SelectListItem( ) {
						Text = i.ToString( ),
						Value = i.ToString( ),
						Selected = ( i.ToString( ) == selectedValue )
					} );
				}

				return listItems;
			}
		}
	}
}