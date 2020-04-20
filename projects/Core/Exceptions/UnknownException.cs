﻿using System;

namespace Core.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException(string message) : base(message)
        {
        }
    }
}
