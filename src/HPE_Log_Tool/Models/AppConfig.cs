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
    [Serializable]
    public class AppConfig
    {
        public ConfigModel CompareDB;
        public ConfigModel InsertDB;
        private static string key_pass = "This_is_a_key__.";
        private static readonly string configFile = "config.xml";
        private static readonly byte[] iv = new byte[16];
        [DataMember(EmitDefaultValue = false)]
        public string AuthenPassword { get; set; }
        public static AppConfig LoadConfig()
        {
            AppConfig _mConfig = new AppConfig();
            _mConfig.CompareDB = new ConfigModel();
            _mConfig.InsertDB = new ConfigModel();
            try
            {
                if (File.Exists(configFile))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFile);
                    string xml = xmlDoc.InnerXml;
                    _mConfig = Utility.Deserialize<AppConfig>(xml);
                    DescryptDbConfig(_mConfig.CompareDB);
                    DescryptDbConfig(_mConfig.InsertDB);
                    DecryptAuthenPass(_mConfig);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error($"{MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
            return _mConfig;
        }

        private static void DescryptDbConfig(ConfigModel config)
        {
            try
            {
                
                    byte[] pass = Encoding.UTF8.GetBytes(key_pass);
                    if (config.DatabasePassword != null)
                    {
                        config.DatabasePassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.DatabasePassword), pass, iv));
                    }
                    if (config.CommandPassword != null)
                    {
                        config.CommandPassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.CommandPassword), pass, iv));
                    }
                    if (config.ConfigPassword != null)
                    {
                        config.ConfigPassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.ConfigPassword), pass, iv));
                    }
                
            }
            catch (Exception ex)
            {
                //LogHelper.Error($"{MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        private static void EncryptDbConfig(ConfigModel config)
        {
            try
            {

                    byte[] pass = Encoding.UTF8.GetBytes(key_pass);
                    byte[] encrypted = null;
                    if (config.DatabasePassword != null) //encrypt pass
                    {
                        encrypted = Utility.Encrypt(Encoding.UTF8.GetBytes(config.DatabasePassword), pass, iv);
                        config.DatabasePassword = Utility.ByteAsString(encrypted, false);
                    }
                    if (config.CommandPassword != null)
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
            catch (Exception ex)
            {
                //LogHelper.Error($"{MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        private static void EncryptAuthenPass (AppConfig config)
        {
            try
            {
                byte[] pass = Encoding.UTF8.GetBytes(key_pass);
                byte[] encrypted = null;
                if(config.AuthenPassword != null)
                {
                    encrypted = Utility.Encrypt(Encoding.UTF8.GetBytes(config.AuthenPassword), pass, iv);
                    config.AuthenPassword = Utility.ByteAsString(encrypted, false);
                }
            } 
            catch(Exception ex)
            {

            }
        }
        private static void DecryptAuthenPass (AppConfig config)
        {
            try
            {
                byte[] pass = Encoding.UTF8.GetBytes(key_pass);
                if (config.AuthenPassword != null)
                {
                    config.AuthenPassword = Encoding.UTF8.GetString(Utility.Decrypt(Utility.GetBytes(config.AuthenPassword), pass, iv));
                }

            } 
            catch (Exception ex)
            {

            }
        }
        public static bool SaveConfig(AppConfig pConfigModel)
        {
            try
            {
                AppConfig cloneConfig = Utility.DeepClone<AppConfig>(pConfigModel);
                EncryptDbConfig(cloneConfig.CompareDB);
                EncryptDbConfig(cloneConfig.InsertDB);
                EncryptAuthenPass(cloneConfig);
                string xml = Utility.Serialize<AppConfig>(cloneConfig);
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
    }
}
