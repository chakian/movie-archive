using MArchiveLibrary.JqGrid.Core;
using System;
using System.Linq;

namespace MArchiveLibrary.JqGrid.Model
{
	public class JqGridHelper<T> {
		public static IQueryable<T> Filter( JqGridRequest request, IQueryable<T> source ) {
			if( request.filters != null )
				request.filters.rules.RemoveAll( q => q.data == "-1" );

			source = JqGridExtensions.ListAddSearchQuery( source, request );
			source = JqGridExtensions.MakePagination( source, request );

			return source;
		}

		public static JqGridData Convert( JqGridRequest request, IQueryable<T> source, Func<T, JqGridRowItem> rowItems, int unfilteredRowCount ) {
			int pageCount;
			int totalRowCount;
			totalRowCount = unfilteredRowCount;
			pageCount = ( int )Math.Ceiling( ( decimal )totalRowCount / ( decimal )request.rows );
			JqGridData grid = new JqGridData {
				total = pageCount,
				page = request.page,
				records = totalRowCount,
				rows = (
							from t in source
							select new JqGridRowItem( ) {
								id = rowItems.Invoke( t ).id,
								cell = rowItems.Invoke( t ).cell
							}
					  ).ToArray( )
			};

			return grid;
		}

		public static JqGridData ToJqGridData( JqGridRequest request, IQueryable<T> source, Func<T, JqGridRowItem> rowItems ) {
			IQueryable<T> data = Filter( request, source );

			return Convert( request, data, rowItems, source.Count( ) );
		}
	}
}