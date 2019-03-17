using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hein.RulesEngine.Framework.Serialization
{
    public static class Serialize
    {
        public static string ToXml<T>(this T value)
        {
            try
            {
                if (value == null)
                {
                    return string.Empty;
                }

                string xml = string.Empty;
                using (var memoryStream = new MemoryStream())
                using (var lvWriter = XmlWriter.Create(memoryStream))
                {
                    var xs = new XmlSerializer(value.GetType());
                    xs.Serialize(lvWriter, value);
                    xml = new UTF8Encoding().GetString(memoryStream.GetBuffer());
                    xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    xml = xml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                    xml = xml.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                    xml = xml.Replace(" xsi:nil=\"true\"", "");
                    xml = xml.Replace("\0", "");
                }

                if (!string.IsNullOrEmpty(xml))
                {
                    byte[] encodedString = Encoding.UTF8.GetBytes(xml);
                    using (MemoryStream ms = new MemoryStream(encodedString))
                    {
                        ms.Flush();
                        ms.Position = 0;

                        using (var reader = new StreamReader(ms))
                        {
                            xml = reader.ReadToEnd();
                        }
                    }
                }

                return xml;

            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }

            return string.Empty;
        }

        public static string ToJson(this object value)
        {
            try
            {
                if (value == null)
                {
                    return string.Empty;
                }

                return JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                ThrowError(ex);
            }

            return string.Empty;
        }

        private static void ThrowError(Exception ex)
        {
            throw new SerializationException(
                string.Concat("Serialization Error: ", ex.Message), ex);
        }
    }
}
