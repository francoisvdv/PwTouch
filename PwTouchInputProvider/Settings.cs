using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace PwTouchInputProvider
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

            dictionary.Clear();

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
            catch (Exception exc) { Log.Write(exc.Message); }
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

        public int Camera
        {
            get
            {
                int i;
                int.TryParse(GetValue("Camera", "0"), out i);
                return i;
            }
            set { SetValue("Camera", value.ToString()); }
        }

        public int CameraMode
        {
            get
            {
                int i;
                int.TryParse(GetValue("CameraMode", "0"), out i);
                return i;
            }
            set { SetValue("CameraMode", value.ToString()); }
        }

        public int SkipFrames
        {
            get
            {
                int i;
                int.TryParse(GetValue("SkipFrames", "0"), out i);
                return i;
            }
            set { SetValue("SkipFrames", value.ToString()); }
        }

        public string DetectorName
        {
            get
            {
                return GetValue("DetectorName", "[default]");
            }
            set { SetValue("DetectorName", value.ToString()); }
        }

        public List<CalibrationPoint> CalibrationPoints
        {
            get
            {
                List<CalibrationPoint> r = new List<CalibrationPoint>();
                int count;
                int.TryParse(GetValue("CalibrationPoint_Count", "0"), out count);

                for (int i = 0; i < count; i++)
                {
                    int screenX, screenY, webcamX, webcamY;
                    if (!int.TryParse(GetValue(String.Format("CalibrationPoint_{0}_ScreenX", i), "nothing"), out screenX) ||
                        !int.TryParse(GetValue(String.Format("CalibrationPoint_{0}_ScreenY", i), "nothing"), out screenY) ||
                        !int.TryParse(GetValue(String.Format("CalibrationPoint_{0}_WebcamX", i), "nothing"), out webcamX) ||
                        !int.TryParse(GetValue(String.Format("CalibrationPoint_{0}_WebcamY", i), "nothing"), out webcamY))
                        continue;

                    r.Add(new CalibrationPoint(screenX, screenY, webcamX, webcamY));
                }

                return r;
            }
            set
            {
                SetValue("CalibrationPoint_Count", value.Count.ToString());

                for (int i = 0; i < value.Count; i++)
                {
                    SetValue(String.Format("CalibrationPoint_{0}_ScreenX", i), value[i].ScreenX.ToString());
                    SetValue(String.Format("CalibrationPoint_{0}_ScreenY", i), value[i].ScreenY.ToString());
                    SetValue(String.Format("CalibrationPoint_{0}_WebcamX", i), value[i].WebcamX.ToString());
                    SetValue(String.Format("CalibrationPoint_{0}_WebcamY", i), value[i].WebcamY.ToString());
                }
            }
        }
    }
}