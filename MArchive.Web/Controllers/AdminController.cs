using System.Linq;
using System.Web;
using System.Web.Mvc;
using MArchive.BL;
using MArchive.Domain.Movie;
using MArchive.Web.Models.Movie;
using MArchive.Web.Mvc.BaseControllers;
using MArchiveLibrary.Attributes;
using MArchiveLibrary.Helpers;

namespace MArchive.Web.Controllers {
	[Role( RoleEnum.SuperDuperUser )]
	public class AdminController : MArchiveBaseController
    {
		public ActionResult Index( ) {
			return View( );
		}

        public ActionResult AddMovie()
        {
            MovieAddEditModel model = new MovieAddEditModel();
            model.Movie = new MovieDO();
            model.MovieName = new MovieNameDO();
            //model.MovieArchive = new MovieArchiveDO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMovie(MovieAddEditModel model)
        {
            model.MovieName.LanguageID = ParseHelper.ToInt32(model.NameLanguageID);
            model.MovieName.IsDefault = true;

            //model.MovieArchive.ArchiveID = Parse.ToInt32(model.ArchiveID);
            //model.MovieArchive.Resolution = model.ArchiveResolution;
            //model.MovieArchive.FileExtension = model.ArchiveFileExtension;
            //model.MovieArchive.UserID = model.

            //insert movie first and get inserted id
            int movieId = MovieBL.Save(model.Movie, UserID);
            //save the name
            model.MovieName.MovieID = movieId;
            MovieNameBL.Save(model.MovieName, UserID);
            //if (model.ArchiveExists == true)
            //{
            //    //save the archive
            //    model.MovieArchive.MovieID = movieId;
            //    MovieArchiveBL.Save(model.MovieArchive, UserID);
            //}

            switch (model.Submit)
            {
                case "SubmitAndNavigateToMovie":
                    return RedirectToAction("DetailView", "Movie", new { id = movieId });
                case "SubmitAndClearForm":
                default:
                    return RedirectToAction("AddMovie");
            }
        }

        public ActionResult EditMovie(int id)
        {
            MovieAddEditModel model = new MovieAddEditModel();
            model.Movie = MovieBL.GetMovie(id);
            model.MovieName = MovieNameBL.GetAllDOByMovieID(id).SingleOrDefault(q => q.IsDefault == true);
            //model.MovieArchive = MovieArchiveBL.GetAllDOByMovieID(id).SingleOrDefault();

            model.Languages.ToList().ForEach(q => q.Selected = false);
            model.Languages.Single(q => q.Value == model.MovieName.LanguageID.ToString()).Selected = true;

            //if (model.MovieArchive != null)
            //{
            //    model.ArchiveExists = true;

            //    model.ResolutionList.ToList().ForEach(q => q.Selected = false);
            //    model.ResolutionList.Single(q => q.Value == model.MovieArchive.Resolution).Selected = true;

            //    model.FileExtensionList.ToList().ForEach(q => q.Selected = false);
            //    model.FileExtensionList.Single(q => q.Value == model.MovieArchive.FileExtension).Selected = true;

            //    model.Archives.ToList().ForEach(q => q.Selected = false);
            //    model.Archives.Single(q => q.Value == model.MovieArchive.ArchiveID.ToString()).Selected = true;
            //}

            return View(model);
        }

        [HttpPost]
        public ActionResult EditMovie(MovieAddEditModel model)
        {
            int movieId = model.MovieId;

            MovieDO _movie = MovieBL.GetMovie(movieId);
            MovieNameDO _movieName = MovieNameBL.GetAllDOByMovieID(movieId).SingleOrDefault(q => q.IsDefault == true);
            //MovieArchiveDO _movieArchive = MovieArchiveBL.GetAllDOByMovieID(movieId).SingleOrDefault();
            //if (_movieArchive == null) _movieArchive = new MovieArchiveDO() { MovieID = movieId };

            _movieName.LanguageID = ParseHelper.ToInt32(model.NameLanguageID);
            _movieName.Name = model.MovieName.Name;

            //_movieArchive.ArchiveID = Parse.ToInt32(model.ArchiveID);
            //_movieArchive.Resolution = model.ArchiveResolution;
            //_movieArchive.FileExtension = model.ArchiveFileExtension;
            //_movieArchive.Path = model.MovieArchive.Path;

            //if imdbid is changed, we need to reset imdb things first.
            string previousImdbId = _movie.ImdbID;
            if (string.IsNullOrEmpty(previousImdbId) == false && model.Movie.ImdbID != previousImdbId)
            {
                MovieBL.ResetImdbInformation(model.MovieId);
                _movie = MovieBL.GetMovie(movieId);
                _movie.ImdbID = model.Movie.ImdbID;
            }

            //save movie
            MovieBL.Save(_movie, UserID);
            //save the name
            MovieNameBL.Save(_movieName, UserID);
            //if (model.ArchiveExists == true)
            //{
            //    //save the archive
            //    MovieArchiveBL.Save(_movieArchive, UserID);
            //}

            switch (model.Submit)
            {
                case "SubmitAndNavigateToMovie":
                    return RedirectToAction("DetailView", "Movie", new { id = movieId });
                case "SubmitAndClearForm":
                default:
                    return RedirectToAction("AddMovie");
            }
        }

		public ActionResult ExcelImport( ) {
			return View( );
		}

        public ActionResult ExcelImportForArchive()
        {
            return View();
        }

		[HttpPost]
		public ActionResult UploadExcel( HttpPostedFileBase MovieFile ) {
            //TODO: Burası bir şekilde çalışmıyor.
            ///Microsoft Excel cannot access the file 'C:\Development\marchive\MArchive.Web\App_Data\Uploads\MovieData2.xlsx'. 
            ///There are several possible reasons: 
            /// The file name or path does not exist. 
            /// The file is being used by another program.
            /// The workbook you are trying to save has the same name as a currently open workbook.

            //if (MovieFile == null || MovieFile.ContentLength == 0)
            //{
            //    TempData["ErrorMessage"] = "Please select a valid file to upload";
            //    return RedirectToAction("ExcelImport");
            //}
            //string extension = Path.GetExtension(MovieFile.FileName);
            //if (extension != ".xls" && extension != ".xlsx")
            //{
            //    TempData["ErrorMessage"] = "Please upload an excel file with 'xls' or 'xlsx' extension";
            //    return RedirectToAction("ExcelImport");
            //}

            //string path = Server.MapPath("~/App_Data/Uploads/");
            //Directory.CreateDirectory(path);
            //path = Path.Combine(path, "MovieData.xlsx");
            //MovieFile.SaveAs(path);

            ////string path = Server.MapPath("~/App_Data/Uploads/");
            ////path = Path.Combine(path, "MovieData3.xlsx");

            //Excel.Application excel = new Excel.Application();
            //Excel.Workbook wb = excel.Workbooks.Open(path);

            //try
            //{
            //    // Get worksheet names
            //    //foreach( Excel.Worksheet sh in wb.Worksheets )
            //    //	Debug.WriteLine( sh.Name );

            //    //Get headers
            //    if (wb.Sheets["Movies"].Cells[1, "A"].Value2 != "Original Name"
            //        && wb.Sheets["Movies"].Cells[1, "B"].Value2 != "Org. Name Lang. ID"
            //        && wb.Sheets["Movies"].Cells[1, "C"].Value2 != "English Name"
            //        && wb.Sheets["Movies"].Cells[1, "D"].Value2 != "Turkish Name"
            //        && wb.Sheets["Movies"].Cells[1, "E"].Value2 != "IMDb ID"
            //        && wb.Sheets["Movies"].Cells[1, "F"].Value2 != "Archive ID"
            //        && wb.Sheets["Movies"].Cells[1, "G"].Value2 != "Resolution"
            //        && wb.Sheets["Movies"].Cells[1, "H"].Value2 != "File Extension"
            //        && wb.Sheets["Movies"].Cells[1, "I"].Value2 != "Path")
            //    {
            //        TempData["ErrorMessage"] = "Uploaded file corrupted. Please get a valid sample file.";
            //        return RedirectToAction("ExcelImport");
            //    }

            //    //TODO: Burayı transaction içinde yaparsak daha güzel olur
            //    //List<MovieDO> movies = new List<MovieDO>( );
            //    //List<MovieNameDO> names = new List<MovieNameDO>( );
            //    //List<MovieArchiveDO> archives = new List<MovieArchiveDO>( );
            //    MovieDO _movie;
            //    MovieNameDO _name;
            //    //MovieArchiveDO _archive;

            //    int i = 2;
            //    while (string.IsNullOrEmpty(wb.Sheets["Movies"].Cells[i, "A"].Value2) == false)
            //    {
            //        // MOVIE
            //        _movie = new MovieDO();
            //        string _imdbId = wb.Sheets["Movies"].Cells[i, "E"].Value2;
            //        if (string.IsNullOrEmpty(_imdbId) == false)
            //        {
            //            _imdbId = _imdbId.Replace("\\", "").Replace("/", "");
            //            _movie.ImdbID = _imdbId;
            //        }
            //        else
            //            _movie.ImdbID = null;
            //        _movie.Year = null;
            //        _movie.ImdbParsed = false;
            //        int _movieId = MovieBL.Save(_movie, UserID);

            //        // NAME
            //        int _tr = 56;
            //        int _en = 1;
            //        _name = new MovieNameDO();
            //        _name.MovieID = _movieId;
            //        _name.LanguageID = GetInt(wb.Sheets["Movies"].Cells[i, "B"].Value2);
            //        _name.Name = wb.Sheets["Movies"].Cells[i, "A"].Value2;
            //        _name.IsDefault = true;
            //        MovieNameBL.Save(_name, UserID);
            //        if (string.IsNullOrEmpty(wb.Sheets["Movies"].Cells[i, "C"].Value2) == false)
            //        { //english name
            //            _name = new MovieNameDO();
            //            _name.MovieID = _movieId;
            //            _name.LanguageID = _en;
            //            _name.Name = wb.Sheets["Movies"].Cells[i, "C"].Value2;
            //            _name.IsDefault = false;
            //            MovieNameBL.Save(_name, UserID);
            //        }
            //        if (string.IsNullOrEmpty(wb.Sheets["Movies"].Cells[i, "D"].Value2) == false)
            //        { //turkish name
            //            _name = new MovieNameDO();
            //            _name.MovieID = _movieId;
            //            _name.LanguageID = _tr;
            //            _name.Name = wb.Sheets["Movies"].Cells[i, "D"].Value2;
            //            _name.IsDefault = false;
            //            MovieNameBL.Save(_name, UserID);
            //        }

            //        //// ARCHIVE
            //        //_archive = new MovieArchiveDO();
            //        //_archive.MovieID = _movieId;
            //        //_archive.ArchiveID = GetInt(wb.Sheets["Movies"].Cells[i, "F"].Value2);
            //        //_archive.Resolution = wb.Sheets["Movies"].Cells[i, "G"].Value2;
            //        //_archive.FileExtension = wb.Sheets["Movies"].Cells[i, "H"].Value2;
            //        //_archive.Path = wb.Sheets["Movies"].Cells[i, "I"].Value2;
            //        //MovieArchiveBL.Save(_archive, UserID);

            //        i++;
            //    }

            //    TempData["InfoMessage"] = "Success";
            //}
            //finally
            //{
            //    wb.Close();
            //    excel.Quit();
            //}

			return RedirectToAction( "Index" );
		}

        [HttpPost]
        public ActionResult UploadExcelForArchive(HttpPostedFileBase MovieFile)
        {
            //if (MovieFile == null || MovieFile.ContentLength == 0)
            //{
            //    TempData["ErrorMessage"] = "Please select a valid file to upload";
            //    return RedirectToAction("ExcelImport");
            //}
            //string extension = Path.GetExtension(MovieFile.FileName);
            //if (extension != ".xls" && extension != ".xlsx")
            //{
            //    TempData["ErrorMessage"] = "Please upload an excel file with 'xls' or 'xlsx' extension";
            //    return RedirectToAction("ExcelImport");
            //}

            //string path = Server.MapPath("~/App_Data/Uploads/");
            //Directory.CreateDirectory(path);
            //path = Path.Combine(path, MovieFile.FileName);
            //MovieFile.SaveAs(path);

            //Excel.Application excel = new Excel.Application();
            //Excel.Workbook wb = excel.Workbooks.Open(path);

            //try
            //{
            //    // Get worksheet names
            //    //foreach( Excel.Worksheet sh in wb.Worksheets )
            //    //	Debug.WriteLine( sh.Name );

            //    //Get headers
            //    if (wb.Sheets["Movies"].Cells[1, "A"].Value2 != "Movie ID"
            //        //&& wb.Sheets["Movies"].Cells[1, "B"].Value2 != "Org. Name Lang. ID"
            //        && wb.Sheets["Movies"].Cells[1, "C"].Value2 != "Archive ID"
            //        && wb.Sheets["Movies"].Cells[1, "D"].Value2 != "Resolution"
            //        && wb.Sheets["Movies"].Cells[1, "E"].Value2 != "File Extension"
            //        && wb.Sheets["Movies"].Cells[1, "F"].Value2 != "Path"
            //        )
            //    {
            //        TempData["ErrorMessage"] = "Uploaded file corrupted. Please get a valid sample file.";
            //        return RedirectToAction("ExcelImport");
            //    }

            //    //MovieArchiveDO _archive;

            //    int i = 2;
            //    while (string.IsNullOrEmpty(wb.Sheets["Movies"].Cells[i, "B"].Value2) == false)
            //    {
            //        //// ARCHIVE
            //        //_archive = new MovieArchiveDO();
            //        //_archive.MovieID = GetInt(wb.Sheets["Movies"].Cells[i, "A"].Value2);
            //        //_archive.ArchiveID = GetInt(wb.Sheets["Movies"].Cells[i, "C"].Value2);
            //        //_archive.Resolution = wb.Sheets["Movies"].Cells[i, "D"].Value2;
            //        //_archive.FileExtension = wb.Sheets["Movies"].Cells[i, "E"].Value2;
            //        //_archive.Path = wb.Sheets["Movies"].Cells[i, "F"].Value2;
            //        //MovieArchiveBL.Save(_archive, UserID);

            //        i++;
            //    }

            //    TempData["InfoMessage"] = "Success";
            //}
            //finally
            //{
            //    wb.Close();
            //    excel.Quit();
            //}

            return RedirectToAction("Index");
        }

		private int GetInt( object input ) {
			string val = input.ToString( ).Replace( ".", "" ).Replace( ",", "" );
			int _temp;
			if( int.TryParse( val, out _temp ) == false )
				_temp = 0;
			return _temp;
		}

        public ActionResult Test()
        {
            return View();
        }
	}
}