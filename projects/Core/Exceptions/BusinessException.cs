﻿using System;

namespace Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string messageFormat, string arg) : base(string.Format(messageFormat, arg))
        {
        }

        public BusinessException(string messageFormat, string arg1, string arg2) : base(string.Format(messageFormat, arg1, arg2))
        {
        }

        public BusinessException(string messageFormat, string arg1, string arg2, string arg3) : base(string.Format(messageFormat, arg1, arg2, arg3))
        {
        }

        public BusinessException()
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
