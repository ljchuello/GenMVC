using System;
using System.IO;
using Newtonsoft.Json;

namespace GenMVC
{
    public class Fichero
    {
        public static string ObjectToJson(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o, Formatting.Indented);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}