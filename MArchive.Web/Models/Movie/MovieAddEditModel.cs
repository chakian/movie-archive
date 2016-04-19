using System.Collections.Generic;
using System.Linq;
using MArchive.BL;
using MArchive.Domain.Movie;
using System.Web.Mvc;

namespace MArchive.Web.Models.Movie
{
    public class MovieAddEditModel
    {
        public MovieDO Movie { get; set; }
        public MovieNameDO MovieName { get; set; }
        //public MovieUserArchiveDO MovieArchive { get; set; }

        public string NameLanguageID { get; set; }

        //public bool ArchiveExists { get; set; }
        //public string ArchiveResolution { get; set; }
        //public string ArchiveFileExtension { get; set; }
        //public string ArchiveID { get; set; }

        public string Submit { get; set; }

        public int MovieId
        {
            get
            {
                return Movie != null ? Movie.ID : 0;
            }
        }
        private IEnumerable<SelectListItem> _languages = null;
        public IEnumerable<SelectListItem> Languages
        {
            get
            {
                if (_languages == null)
                {
                    List<SelectListItem> languages = new List<SelectListItem>();
                    languages.Add(new SelectListItem() { Text = "Select language", Value = "-1", Selected = false });

                    var allLanguages = LanguageBL.GetAllDO().OrderBy(q => q.Name);

                    foreach (var item in allLanguages)
                    {
                        languages.Add(new SelectListItem()
                        {
                            Text = item.Name,
                            Value = item.ID.ToString(),
                            Selected = item.Name == "English" ? true : false
                        });
                    }
                    _languages = languages;
                }
                return _languages;
            }
        }
        //private IEnumerable<SelectListItem> _resolutionList = null;
        //public IEnumerable<SelectListItem> ResolutionList
        //{
        //    get
        //    {
        //        if (_resolutionList == null)
        //        {
        //            List<SelectListItem> resolution = new List<SelectListItem>();
        //            resolution.Add(new SelectListItem() { Text = "HD", Value = "HD", Selected = true });
        //            resolution.Add(new SelectListItem() { Text = "FULL HD", Value = "FULL HD", Selected = false });
        //            resolution.Add(new SelectListItem() { Text = "DIVX", Value = "DIVX", Selected = false });
        //            resolution.Add(new SelectListItem() { Text = "SHIT", Value = "SHIT", Selected = false });
        //            resolution.Add(new SelectListItem() { Text = "Youtube", Value = "Youtube", Selected = false });
        //            resolution.Add(new SelectListItem() { Text = "CD (avseq01)", Value = "CD", Selected = false });
        //            resolution.Add(new SelectListItem() { Text = "DVD", Value = "DVD", Selected = false });
        //            _resolutionList = resolution;
        //        }
        //        return _resolutionList;
        //    }
        //}
        //private IEnumerable<SelectListItem> _fileExtensionList = null;
        //public IEnumerable<SelectListItem> FileExtensionList
        //{
        //    get
        //    {
        //        if (_fileExtensionList == null)
        //        {
        //            List<SelectListItem> fileExtension = new List<SelectListItem>();
        //            fileExtension.Add(new SelectListItem() { Text = "mp4", Value = "mp4", Selected = true });
        //            fileExtension.Add(new SelectListItem() { Text = "mkv", Value = "mkv", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "avi", Value = "avi", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "dat", Value = "dat", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "dvd", Value = "dvd", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "flv", Value = "flv", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "m2ts", Value = "m2ts", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "m4v", Value = "m4v", Selected = false });
        //            fileExtension.Add(new SelectListItem() { Text = "mpg", Value = "mpg", Selected = false });
        //            _fileExtensionList = fileExtension;
        //        }
        //        return _fileExtensionList;
        //    }
        //}
        //private IEnumerable<SelectListItem> _archives = null;
        //public IEnumerable<SelectListItem> Archives
        //{
        //    get
        //    {
        //        if (_archives == null)
        //        {
        //            List<SelectListItem> archiveSelectList = new List<SelectListItem>();
        //            archiveSelectList.Add(new SelectListItem() { Text = "Select archive", Value = "-1", Selected = false });

        //            var allArchives = ArchiveBL.GetAllDOByUserID().OrderBy(q => q.Name);

        //            foreach (var item in allArchives)
        //            {
        //                archiveSelectList.Add(new SelectListItem()
        //                {
        //                    Text = item.Name,
        //                    Value = item.ID.ToString(),
        //                    Selected = item.Name == "CHAK_2000S" ? true : false
        //                });
        //            }
        //            _archives = archiveSelectList;
        //        }
        //        return _archives;
        //    }
        //}
    }
}