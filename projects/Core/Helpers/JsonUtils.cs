﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Helpers
{
    public static class JsonUtils
    {
        public static string ToJson(this object value, Formatting formatting = Formatting.Indented)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = contractResolver,
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(value, formatting, settings);
        }
    }
}
