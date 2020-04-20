﻿using System.Collections.Generic;

namespace Core.Models.Converters
{
    public interface IModelConverter
    {
        T Parse<T>(IList<object> row);

        IList<T> ParseAll<T>(IList<IList<object>> values);
    }
}