using System;
using System.Collections.Generic;

namespace MArchiveLibrary {
	public class imdbModel {
		public String id;
		public Int32? year;
		public Double? imdbRating;
		public String picturePath;
		public List<String> genres;
		public List<String> cast;
		public List<String> directors;
		public List<String> writers;
		public List<String> languages;
	}
}