﻿using System;

namespace Core.Helpers
{
    public static class Guards
    {
        public static void NotNull(object obj, string msg)
        {
            if (obj == null)
            {
                throw new ArgumentException(msg);
            }
        }

        public static void NotNullOrEmpty(string obj, string msg)
        {
            if (string.IsNullOrEmpty(obj))
            {
                throw new ArgumentException(msg);
            }
        }

        public static void GreaterThanZero(long id, string msg)
        {
            if (id <= 0)
            {
                throw new ArgumentException(msg);
            }
        }
    }
}
