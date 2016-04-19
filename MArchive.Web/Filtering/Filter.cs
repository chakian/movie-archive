using System.Collections.Generic;
using System.Linq;
using MArchive.BL;
using MArchive.Domain.Movie;
using com.cagdaskorkut.utility;

namespace MArchive.Web.Filtering
{
    public class FiltersForPage
    {
        public Filter Name { get; set; }
        public Filter Year { get; set; }
        public Filter ImdbRating { get; set; }
        public Filter IHaveWatched { get; set; }
        public Filter MyRating { get; set; }
        public string[] MovieType { get; set; }

        public FiltersForPage()
        {
            Name = new Filter(FilterNames.Name, FilterTypes.Contains);
            Year = new Filter(FilterNames.Year, FilterTypes.Equals);
            ImdbRating = new Filter(FilterNames.ImdbRating, FilterTypes.GreaterThanOrEqual);
            IHaveWatched = new Filter(FilterNames.HaveIWatched, FilterTypes.Equals);
            MyRating = new Filter(FilterNames.MyRating, FilterTypes.GreaterThanOrEqual);
            //MovieType = new Filter(FilterNames.MovieType, FilterTypes.Contains);
            MovieType = new string[0];
        }

        public FiltersForPage(string name, string year, string imdbRating, string iHaveWatched, string myRating, string movieTypes)
        {
            Name = new Filter(FilterNames.Name, FilterTypes.Contains, name);
            Year = new Filter(FilterNames.Year, FilterTypes.Equals, year);
            ImdbRating = new Filter(FilterNames.ImdbRating, FilterTypes.GreaterThanOrEqual, imdbRating);
            IHaveWatched = new Filter(FilterNames.HaveIWatched, FilterTypes.Equals, iHaveWatched);
            MyRating = new Filter(FilterNames.MyRating, FilterTypes.GreaterThanOrEqual, myRating);
            //MovieType = new Filter(FilterNames.MovieType, FilterTypes.Contains, movieTypes);
            MovieType = movieTypes.Split(',');
        }

        public List<MovieDO> DoFilter(List<MovieDO> movieList, int userID)
        {
            //filter names
            if (string.IsNullOrEmpty(Name.FilterValue) == false)
            {
                var tempList = MovieListBL.SearchSuperblyForNames(Name.FilterValue).Select(q => q.ID);
                movieList = movieList.Where(q => tempList.Contains(q.ID)).ToList();
            }

            //filter year
            if (string.IsNullOrEmpty(Year.FilterValue) == false)
            {
                int year = Parse.ToInt32(Year.FilterValue);
                if (year > 0)
                {
                    if (Year.FilterType == FilterTypes.Equals)
                        movieList = movieList.Where(q => q.Year.HasValue && q.Year.Value == year).ToList();
                    if (Year.FilterType == FilterTypes.GreaterThanOrEqual)
                        movieList = movieList.Where(q => q.Year.HasValue && q.Year.Value >= year).ToList();
                    if (Year.FilterType == FilterTypes.LessThanOrEqual)
                        movieList = movieList.Where(q => q.Year.HasValue && q.Year.Value <= year).ToList();
                }
            }

            //filter imdb rating
            if (string.IsNullOrEmpty(ImdbRating.FilterValue) == false)
            {
                double imdbRating = Parse.ToDouble(ImdbRating.FilterValue);
                if (imdbRating > 0)
                {
                    if (ImdbRating.FilterType == FilterTypes.Equals)
                        movieList = movieList.Where(q => q.ImdbRating.HasValue && q.ImdbRating.Value == imdbRating).ToList();
                    if (ImdbRating.FilterType == FilterTypes.GreaterThanOrEqual)
                        movieList = movieList.Where(q => q.ImdbRating.HasValue && q.ImdbRating.Value >= imdbRating).ToList();
                    if (ImdbRating.FilterType == FilterTypes.LessThanOrEqual)
                        movieList = movieList.Where(q => q.ImdbRating.HasValue && q.ImdbRating.Value <= imdbRating).ToList();
                }
            }

            //filter i have watched
            if (string.IsNullOrEmpty(IHaveWatched.FilterValue) == false)
            {
                string watchedStatus = IHaveWatched.FilterValue;

                if (watchedStatus != "All")
                {
                    List<MovieUserRatingDO> usersRatings = MovieUserRatingBL.GetAllDOByUserID(userID);
                    List<int> movieIds = usersRatings.Where(q => q.Watched == true).Select(q => q.MovieID).ToList();
                    if (watchedStatus == "No")
                    {
                        movieList.RemoveAll(q => movieIds.Contains(q.ID));
                    }
                    else if (watchedStatus == "Yes")
                    {
                        movieList = movieList.Where(q => movieIds.Contains(q.ID)).ToList();
                    }
                }
            }

            //filter my rating
            if (string.IsNullOrEmpty(MyRating.FilterValue) == false)
            {
                double myRating = Parse.ToDouble(MyRating.FilterValue);
                if (myRating > 0)
                {
                    List<int> movieIdsForUsersRatings = new List<int>();
                    if (MyRating.FilterType == FilterTypes.Equals)
                        movieIdsForUsersRatings = MovieUserRatingBL.GetAllDOByUserID(userID).Where(q => q.Rating == myRating).Select(q => q.MovieID).ToList();
                    if (MyRating.FilterType == FilterTypes.GreaterThanOrEqual)
                        movieIdsForUsersRatings = MovieUserRatingBL.GetAllDOByUserID(userID).Where(q => q.Rating >= myRating).Select(q => q.MovieID).ToList();
                    if (MyRating.FilterType == FilterTypes.LessThanOrEqual)
                        movieIdsForUsersRatings = MovieUserRatingBL.GetAllDOByUserID(userID).Where(q => q.Rating <= myRating).Select(q => q.MovieID).ToList();

                    movieList = movieList.Where(q => movieIdsForUsersRatings.Contains(q.ID)).ToList();
                }
            }

            //filter movie types
            if (MovieType.Length > 0)
            {
                List<int> typeIds = new List<int>();
                foreach (var item in MovieType)
                    typeIds.Add(Parse.ToInt32(item));

                var allMoviesAndTypes = MovieTypeBL.GetAllDO().Where(q => typeIds.Contains(q.TypeID));
                movieList = movieList.Join(allMoviesAndTypes, mList => mList.ID, allMovs => allMovs.MovieID, (mList, allMovs) => mList).ToList();
            }

            return movieList;
        }
    }
    public class Filter
    {
        public FilterNames FilterName { get; set; }
        public FilterTypes FilterType { get; set; }
        public string FilterValue { get; set; }

        public Filter(FilterNames name, FilterTypes type, string value = "")
        {
            FilterName = name;
            FilterType = type;
            FilterValue = value;
        }
    }

    public enum FilterNames
    {
        Name,
        Year,
        ImdbRating,
        HaveIWatched,
        MyRating,
        MovieType,
        //Directors,
        //Writers,
        //Actors,
        MovieResolution
    }
    public enum FilterTypes
    {
        Equals,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains
    }
    public enum ResolutionFilters
    {
        DIVX, HD, FULLHD
    }
}