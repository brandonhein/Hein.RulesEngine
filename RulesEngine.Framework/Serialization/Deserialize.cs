using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hein.RulesEngine.Framework.Serialization
{
    public static class Deserialize
    {
        public static T XmlToObject<T>(string xml)
        {
            try
            {
                //load up string to validate before deserialize
                XDocument.Parse(xml).ToString();
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var validateXml = doc.OuterXml;
                //was getting a weird serialization error for this...
                validateXml = validateXml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(validateXml));
                XmlSerializer serializer = new XmlSerializer(typeof(T), "");

                return (T)serializer.Deserialize(ms);
            }
            catch (Exception ex)
            {
                ThrowError(ex, xml);
            }

            return default(T);
        }

        public static T JsonToObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                ThrowError(ex, json);
            }

            return default(T);
        }

        private static void ThrowError(Exception ex, string deserailizingString)
        {
            throw new SerializationException(
                string.Concat("Deserialization Error: ", ex.Message, " Trying to Deserailize: ", deserailizingString), ex);
        }
    }
}
