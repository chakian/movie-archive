﻿using System;

namespace MArchiveLibrary.Exceptions
{
    public class BusinessException : ExceptionBase
    {
        public BusinessException()
            : base()
        {

        }

        public BusinessException(string errorMessage)
            : base(errorMessage)
        {

        }

        public BusinessException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {

        }
    }
}