namespace MArchive.Domain {
	public class MArchiveJsonResult {
		public bool isSuccess { get; set; }

        public object data { get; set; }

        public MArchiveJsonResult()
        {
        }

		public MArchiveJsonResult( bool _isSuccess, object _data )
        {
            isSuccess = _isSuccess;
            data = _data;
        }
	}
}