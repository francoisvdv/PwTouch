using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace PwTouchLib
{
    public abstract class Settings
    {
        protected class SettingsValue
        {
            public string Value { get; set; }

            public SettingsValue(string value)
            {
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }

        public bool IsLoaded { get; private set; }

        protected Settings()
        {
        }

        public abstract string FilePath { get; }

        protected Dictionary<string, SettingsValue> dictionary = new Dictionary<string, SettingsValue>();
        protected void SetValue(string key, string value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, new SettingsValue(value));
            else
                dictionary[key] = new SettingsValue(value);
        }
        protected string GetValue(string key)
        {
            if (!dictionary.ContainsKey(key))
                return "";
            else
            {
                if (dictionary[key].Value == null)
                    return "";
                else
                    return dictionary[key].Value;
            }
        }
        protected string GetValue(string key, string defaultValue)
        {
            if (!dictionary.ContainsKey(key))
                return defaultValue;
            else
            {
                if (dictionary[key].Value == null)
                    return "";
                else
                    return dictionary[key].Value;
            }
        }

        public bool Load()
        {
            XmlDocument xDoc = new XmlDocument();

            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xDoc.Load(fs);
                }
            }
            catch { return false; }

            if (xDoc.DocumentElement == null || xDoc.DocumentElement.Name != "PwTouch")
                return false;

            foreach (XmlElement xValue in xDoc.DocumentElement)
            {
                if (xValue.Name != "value")
                    continue;

                if (!dictionary.ContainsKey(xValue.Attributes["key"].Value))
                    dictionary.Add(xValue.Attributes["key"].Value, new SettingsValue(xValue.InnerText));
            }

            IsLoaded = true;

            return true;
        }

        public void Save()
        {
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xDeclaration = xDoc.CreateXmlDeclaration("1.0", null, null);
            xDoc.AppendChild(xDeclaration);

            // root element
            XmlElement xPwTouch = xDoc.CreateElement("PwTouch");
            xDoc.AppendChild(xPwTouch);

            foreach (KeyValuePair<string, SettingsValue> kvp in dictionary)
            {
                XmlElement xValue = xDoc.CreateElement("value");

                //Key
                {
                    XmlAttribute xKey = xDoc.CreateAttribute("key");
                    xKey.Value = kvp.Key;
                    xValue.Attributes.Append(xKey);
                }

                xValue.InnerText = kvp.Value.Value;

                xPwTouch.AppendChild(xValue);
            }

            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    xDoc.Save(fs);
                }
            }
            catch (Exception exc) { Console.WriteLine(exc.Message); }
        }
    }

    public class AppSettings : Settings
    {
        private string filePath;
        public override string FilePath
        {
            get { return filePath; }
        }

        public AppSettings(string filePath)
            : base()
        {
            this.filePath = filePath;
        }

        public int CameraID
        {
            get
            {
                int i;
                int.TryParse(GetValue("CameraID", "0"), out i);
                return i;
            }
            set { SetValue("CameraID", value.ToString()); }
        }
    }
}