using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Controller;

namespace GUI_v2.Model.UserSettings
{
    public class UserSettings
    {
        public NetworkSettings networkSettings;
        public ControlSettings ControlSettings;
        public int ControllerInterval = 50;
        public double Bat1MaxVoltage = 18.2;
        public double Bat2MaxVoltage = 13.4;
        public double Bat1MinVoltage = 0;
        public double Bat2MinVoltage = 0;
        public double Bat1Allert = 0;
        public double Bat2Allert = 0;
        //[XmlIgnore]
        public delegate void SettingsChangedCb();
        [XmlIgnore]
        public SettingsChangedCb SettingsChangedCallback;
        public UserSettings()
        {
            networkSettings = new NetworkSettings();
            ControlSettings = new ControlSettings();
        }

        private void BeforeSerialization()
        {
            ControlSettings.BeforeSerialization();
        }
        private void AfterSerialization()
        {
            ControlSettings.AfterSerialization();
        }
        public void Save(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                BeforeSerialization();
                XmlSerializer xmls = new XmlSerializer(typeof (UserSettings));
                xmls.Serialize(sw, this);
            }
        }

        public static UserSettings Read(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    try
                    {
                        XmlSerializer xmls = new XmlSerializer(typeof(UserSettings));

                        UserSettings ret=  xmls.Deserialize(sr) as UserSettings;
                        ret.AfterSerialization();
                        return ret;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    return null;
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}
