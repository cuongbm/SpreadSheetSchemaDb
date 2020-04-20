using System;

namespace Core.Helpers
{
    public static class ResourceHelper
    {
        public static string GetText(Enum e) 
        {
            return Resources.ResourceManager.GetString($"Enum.{e.GetType().Name}.{e.ToString()}");
        }
    }
}
