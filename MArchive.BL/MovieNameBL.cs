using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using com.cagdaskorkut.utility.CacheUtil;
using com.cagdaskorkut.utility.ExtendedDataContext;
using com.cagdaskorkut.utility.ObjectMapping;
using com.cagdaskorkut.utility.Repository;
using MArchive.DataContext;
using MArchive.Domain.Movie;

namespace MArchive.BL {
    public class MovieNameBL : BLBase {
        private const string CacheAreaKey = "MArchive_MovieName";

		public static string GetOriginalNameOfMovie( int movieID ) {
			return GetAllDOByMovieID( movieID ).Single( q => q.IsDefault ).Name;
		}

		public static List<MovieNameDO> SearchDOByLikelyName( string name ) {
			List<MovieNameDO> list = new List<MovieNameDO>( );

			//first of all get exact matches
			list.AddRange( GetAllAsDO( ).Where( q => q.Name.ToLowerInvariant( ) == name.ToLowerInvariant( ) ) );

			//next, find matches that contain all of the words in the given keyword
			List<MovieNameDO> _tempSearchResults = new List<MovieNameDO>( );
			string[] _tempKeywordParts = name.Split( ' ' );
			if( _tempKeywordParts.Length > 1 ) {
				_tempSearchResults = GetAllAsDO( ).Where( q => q.Name.ToLowerInvariant( ).Contains( _tempKeywordParts[0].ToLowerInvariant( ) ) ).ToList( );
				for( int i = 1; i < _tempKeywordParts.Length; i++ ) {
					_tempSearchResults = _tempSearchResults.Where( q => q.Name.ToLowerInvariant( ).Contains( _tempKeywordParts[i].ToLowerInvariant( ) ) ).ToList( );
				}
			} else {
				_tempSearchResults = GetAllAsDO( ).Where( q => q.Name.ToLowerInvariant( ).Contains( _tempKeywordParts[0].ToLowerInvariant( ) ) ).ToList( );
			}

			foreach( var item in _tempSearchResults ) {
				if( list.Any( q => q.MovieID == item.MovieID ) ) { continue; } else { list.Add( item ); }
			}

			return list;
		}

		public static List<MovieNameDO> GetAllDOByMovieID( int movieID ) {
			List<MovieNameDO> names = new List<MovieNameDO>( );
			foreach( var item in GetAllByMovieID( movieID ).OrderByDescending( n => n.IsDefault ).ThenBy( n => n.Name ) ) {
				MovieNameDO newItem = Mapper.Map<vMovieName, MovieNameDO>( item );
				newItem.LanguageName = item.LanguageName;
				names.Add( newItem );
			}
			return names;
		}

		private static List<vMovieName> GetAllByMovieID( int movieID ) {
			return GetAll( ).Where( q => q.MovieID == movieID ).ToList( );
		}

        public static List<MovieNameDO> GetAllAsDO() {
            string functionName = "GetAllAsDO";
            if (ReadCache<List<MovieNameDO>>(CacheAreaKey, functionName) != null) { return ReadCache<List<MovieNameDO>>(CacheAreaKey, functionName); }

			var list = GetAll( );
			var movieList = Mapper.Map<List<vMovieName>, List<MovieNameDO>>( list );

            WriteCache(CacheAreaKey, functionName, movieList);

            return movieList;
		}

        public static List<vMovieName> GetAll() {
            string functionName = "GetAll";
            if (ReadCache<List<vMovieName>>(CacheAreaKey, functionName) != null) { return ReadCache<List<vMovieName>>(CacheAreaKey, functionName); }

            Repository<vMovieName> rep = new Repository<vMovieName>(MArchiveDataContextProvider.Instance);
            var list = rep.GetAll().ToList();

            WriteCache(CacheAreaKey, functionName, list);

            return list;
		}

		public static void Save( MovieNameDO dataObj, int userID ) {
			Repository<MOV_M_Name> rep = new Repository<MOV_M_Name>( MArchiveDataContextProvider.Instance );

			MOV_M_Name objectToAdd = null;
			if( dataObj.ID == 0 ) {
				objectToAdd = new MOV_M_Name( );
				ObjectMapper.MapObjects( dataObj, objectToAdd, AuditInfo.Fields );
				rep.InsertOnSubmit( objectToAdd );
			} else {
				objectToAdd = rep.GetAll( ).Single( x => x.ID == dataObj.ID );
				ObjectMapper.MapObjects( dataObj, objectToAdd, AuditInfo.Fields );
			}
            rep.DCP.CommitChanges(userID);

            InvalidateCache(CacheAreaKey);

			ObjectMapper.MapObjects( objectToAdd, dataObj );
		}
	}
}