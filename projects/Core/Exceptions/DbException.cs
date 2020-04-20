﻿using System;

namespace Core.Exceptions
{
    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {
        }

        public DbException(string message, Exception e) : base(message, e)
        {
        }
    }
}
