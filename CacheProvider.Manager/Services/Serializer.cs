using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace CacheProvider.Services.Services
{
    public static class Serializer
    {
        public static T FromJson<T>(string jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString, typeof(T), new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
        }

        public static string ToJson<T>(T thisObject)
        {
            return JsonConvert.SerializeObject(thisObject, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });

        }

        public static bool ValidateJsonFormat(string jsonString)
        {
            try
            {
                FromJson<object>(jsonString);

                return true;

            }
            catch (Exception e)
            {
                //logging

                return false;
            }

        }
    }
}
