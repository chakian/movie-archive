using System;
using System.Runtime.Serialization;

namespace MArchiveLibrary.Exceptions
{
    [Serializable]
    public class KnownException : ApplicationException
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public new string Message { get; set; }

        public KnownException()
        {
        }

        public KnownException(string message)
            : base(message)
        {
            Message = message;
        }

        public KnownException(string errorCode, string errorMessage)
            : base(errorMessage)
        {
            Code = errorCode;
            Message = errorMessage;
        }

        public KnownException(string message, Exception inner)
            : base(message, inner)
        {
            Message = message;
        }

        protected KnownException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
