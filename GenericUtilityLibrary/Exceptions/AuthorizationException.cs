using System;

namespace com.cagdaskorkut.utility.Exceptions {
	public class AuthorizationException : ExceptionBase {
		public AuthorizationException( )
			: base( ) {

		}

		public AuthorizationException( string errorMessage )
			: base( errorMessage ) {

		}

		public AuthorizationException( string errorMessage, Exception innerException )
			: base( errorMessage, innerException ) {

		}
	}
}