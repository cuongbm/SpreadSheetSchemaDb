﻿using System;
using System.Threading;

namespace Core.Helpers
{
    public class IdGenerator
    {
        public static long GetUniqueTicks(long lastTick = 0)
        {
            var ticks = new DateTime(1900, 1, 1).Ticks;
            var uNumber = DateTime.Now.Ticks - ticks;
            if (uNumber == lastTick)
            {
                Thread.Sleep(1);
                uNumber = GetUniqueTicks(lastTick);
            }
            return uNumber;
        }
    }
}
