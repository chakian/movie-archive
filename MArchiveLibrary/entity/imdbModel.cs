using System.Collections.Generic;

namespace MArchiveLibrary {
	public class ImdbModel {
		public string id;
		public int? year;
		public double? imdbRating;
		public string picturePath;
		public List<string> genres;
		public List<string> cast;
		public List<string> directors;
		public List<string> writers;
		public List<string> languages;
	}
}