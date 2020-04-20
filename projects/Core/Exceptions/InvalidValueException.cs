﻿using System;

namespace Core.Exceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(string message) : base(message)
        {
        }

        public InvalidValueException(string messageFormat, string arg) : base(string.Format(messageFormat, arg))
        {
        }

        public InvalidValueException(string messageFormat, string arg1, string arg2) : base(string.Format(messageFormat, arg1, arg2))
        {
        }

        public InvalidValueException(string messageFormat, string arg1, string arg2, string arg3) : base(string.Format(messageFormat, arg1, arg2, arg3))
        {
        }
    }
}
