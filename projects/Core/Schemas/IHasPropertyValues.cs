﻿using System.Collections.Generic;

namespace Core.Schemas
{
    public interface IHasPropertyValues
    {
        Dictionary<string, object> PropertyValues { get; }
    }
}
