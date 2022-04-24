using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace BattleEngine.Temporary
{
    public static class JsonSerializationExtensions
    {
        private static readonly SnakeCaseNamingStrategy _snakeCaseNamingStrategy = new SnakeCaseNamingStrategy();
        private static readonly CamelCaseNamingStrategy _camelCaseNamingStrategy = new CamelCaseNamingStrategy();
        private static readonly KebabCaseNamingStrategy _kebabCaseNamingStrategy = new KebabCaseNamingStrategy();
        
        public static JsonSerializerSettings JsonSerializerSettings() =>
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = true , NamingStrategy = new KebabCaseNamingStrategy() }},
                NullValueHandling = NullValueHandling.Ignore
            };

        private static readonly JsonSerializerSettings _snakeCaseSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = _snakeCaseNamingStrategy
            }
        };

        public static string ToSnakeCase<T>(this T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(paramName: nameof(instance));
            }

            return JsonConvert.SerializeObject(instance, _snakeCaseSettings);
        }

        public static string ToSnakeCase(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(paramName: nameof(s));
            }

            return _snakeCaseNamingStrategy.GetPropertyName(s, false);
        }
        public static string ToCamelCase(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(paramName: nameof(s));
            }

            return _camelCaseNamingStrategy.GetPropertyName(s, false);
        }
        public static string ToKebabCase(this string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(paramName: nameof(s));
            }

            return _kebabCaseNamingStrategy.GetPropertyName(s, false);
        }
    }
}