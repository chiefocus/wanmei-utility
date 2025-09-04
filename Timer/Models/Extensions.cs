using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TimerUtility.Models
{
    public static class XElementExtensions
    {
        public static T Deserialize<T>(this XElement element)
        {
            using (var reader = element.CreateReader())
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
            }
        }

        public static string SerializeToString<T>(this T obj)
        {
            var emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add("", "");

            var serializer = new XmlSerializer(typeof(T));

            using (var writer = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings
            {
                OmitXmlDeclaration = true
            }))
            {
                serializer.Serialize(xmlWriter, obj, emptyNamespaces);
                return writer.ToString();
            }
        }
    }
}
