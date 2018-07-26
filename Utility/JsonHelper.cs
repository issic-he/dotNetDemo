using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// json序列化帮助类
    /// </summary>
    public class JsonHelper
    {
        private static JsonHelper _jsonHelper = null;
        private static readonly object _lock = new object();

        public static JsonHelper Instance
        {
            get//双重校验实现单例
            {
                if(_jsonHelper==null)
                {
                    lock (_lock)
                    {
                        if(_jsonHelper==null)
                        {
                            _jsonHelper = new JsonHelper();
                        }
                    }
                }
                return _jsonHelper;
            }
        }
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        public string SerializeByConverter(object obj, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(obj, converters);
        }

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public T DeserializeByConverter<T>(string input, params JsonConverter[] converter)
        {
            return JsonConvert.DeserializeObject<T>(input, converter);
        }

        public T DeserializeBySetting<T>(string input, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(input, settings);
        }

        private object NullToEmpty(object obj)
        {
            return null;
        }

    }
}
