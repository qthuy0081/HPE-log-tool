using HPE_Log_Tool.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace HPE_Log_Tool.Models
{
    public class ConfigModel
    {
        private static readonly string configFile = "config.xml";
        private static readonly byte[] iv = new byte[16];

        #region Database

        [DataMember(EmitDefaultValue = false)]
        public string DatabaseServer { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DatabaseName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DatabaseUser { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DatabasePassword { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DatabaseTimeout { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CommandPassword { get; set; }
        #endregion Database

        #region Security

        [DataMember(EmitDefaultValue = false)]
        public string SystemValue { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ConfigPassword { get; set; }
        #endregion Security
        #region Image Path
        [DataMember(EmitDefaultValue = false)]
        public string ImagePath { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ExcelPath { get; set; }
        public string SaveImagePath { get; set; }
        #endregion
     // cần thì t
        #region Method

        public static ConfigModel LoadConfig()
        {
            ConfigModel _mConfig = new ConfigModel();
            try
            {
                if (File.Exists(configFile))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFile);
                    string xml = xmlDoc.InnerXml;
                    _mConfig =  Utility.Deserialize<ConfigModel>(xml);
                    DescryptionConfig(_mConfig);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error($"{MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
            return _mConfig;
        }

        private static void DescryptionConfig(ConfigModel config)
        {
            try
            {
                if (!string.IsNullOrEmpty(config.SystemValue))
                {
                    byte[] pass = Encoding.UTF8.GetBytes(config.SystemValue);
                    if (config.DatabasePassword != null) 
                    {
                        config.DatabasePassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.DatabasePassword), pass, iv));
                    }
                    if(config.CommandPassword != null)
                    {
                        config.CommandPassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.CommandPassword), pass, iv));
                    }
                    if(config.ConfigPassword != null)
                    {
                        config.ConfigPassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.ConfigPassword), pass, iv));
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error($"{MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }

        private static void EncryptionConfig(ConfigModel config)
        {
            try
            {
                if (!string.IsNullOrEmpty(config.SystemValue))
                {
                    byte[] pass = Encoding.UTF8.GetBytes(config.SystemValue);
                    byte[] encrypted = null;
                    if (config.DatabasePassword != null) //encrypt pass
                    {
                        encrypted = Utility.Encrypt(Encoding.UTF8.GetBytes(config.DatabasePassword), pass, iv);
                        config.DatabasePassword = Utility.ByteAsString(encrypted, false);
                    }

                    if(config.CommandPassword != null)
                    {
                        encrypted = Utility.Encrypt(Encoding.UTF8.GetBytes(config.CommandPassword), pass, iv);
                        config.CommandPassword = Utility.ByteAsString(encrypted, false);

                    }
                    if (config.ConfigPassword != null)
                    {
                        encrypted = Utility.Encrypt(Encoding.UTF8.GetBytes(config.ConfigPassword), pass, iv);
                        config.ConfigPassword = Utility.ByteAsString(encrypted, false);

                    }

                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error($"{MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }

        public static bool SaveConfig(ConfigModel pConfigModel)
        {
            try
            {
                if (string.IsNullOrEmpty(pConfigModel.SystemValue))
                {
                    pConfigModel.SystemValue = "UTE_QT_2020_11_8";
                }
                EncryptionConfig(pConfigModel);
                string xml = Utility.Serialize<ConfigModel>(pConfigModel);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlDoc.Save(configFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}

