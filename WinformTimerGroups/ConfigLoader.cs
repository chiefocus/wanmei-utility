using System.IO;
using System.Xml.Serialization;

namespace WinformTimerGroups
{
    public static class ConfigLoader
    {
        public static Config Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("配置文件不存在", path);

            XmlSerializer xs = new XmlSerializer(typeof(Config));
            using (var fs = File.OpenRead(path))
            {
                return (Config)xs.Deserialize(fs);
            }
        }
    }
}
