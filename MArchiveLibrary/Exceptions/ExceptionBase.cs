using System;
using System.Runtime.Serialization;

namespace MArchiveLibrary.Exceptions
{
    [Serializable]
    public class ExceptionBase : Exception
    {
        public ExceptionBase()
            : base()
        {

        }

        public ExceptionBase(string errorMessage)
            : base(errorMessage)
        {

        }

        public ExceptionBase(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {

        }
    }

    public enum FaultCodes
    {
        BusinessException,
        TechnicalException,
        AuthorizationException
    }

    [DataContract(Namespace = "http://services.cagdaskorkut.com/error")]
    public class ErrorInfo
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string EndpointName { get; set; }
    }
}
