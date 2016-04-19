using System.Collections.Generic;

namespace MArchiveLibrary.Mvc.Core.JqGrid {
	public class Filter {
		public string groupOp { get; set; }
        public List<rules> rules { get; set; }
    }

    public class rules
    {
        public string field { get; set; }
        public string op { get; set; }
        public string data { get; set; }
    }
}