﻿namespace Core.Schemas
{
    public class SystemPropertyDefinition : BasePropertyDefinition
    {
        public SystemPropertyDefinition() { }

        public SystemPropertyDefinition(string name, int offset)
        {
            Value = name;
            OffsetPos = offset;
        }

        public static SystemPropertyDefinition Create(string name, int offset = 0)
        {
            return new SystemPropertyDefinition(name, offset);
        }
    }
}
