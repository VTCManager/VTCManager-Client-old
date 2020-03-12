using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace VTCManager_1._0._0
{
    class SettingsManager
    {
        private string settingsDirectory;
        private string cacheFile;
        private string m_sCacheFileName;
        private string settingsFile;
        private string m_sConfigFileName;
        private CacheDataObject m_oConfigJob;
        public static string userFolder;
        private SettingsDataObject m_oConfig;
        private Utilities utils = new Utilities();

        public SettingsManager()
        {
            this.settingsDirectory = Path.Combine(userFolder, ".vtcmanager");
            this.settingsFile = "settings.xml";
            this.m_sConfigFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".xml";
            this.m_oConfig = new SettingsDataObject();


            this.cacheFile = "cache.xml";
            this.m_sCacheFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".xml";
            this.m_oConfigJob = new CacheDataObject();
        }
        static SettingsManager()
        {
            userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        public void CreateConfig()
        {
            if (!Directory.Exists(this.settingsDirectory))
            {
                Directory.CreateDirectory(this.settingsDirectory);
            }
            if (!File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
            {
                File.Create(Path.Combine(this.settingsDirectory, this.settingsFile)).Dispose();
                string[] contents = new string[] { "<SettingsDataObject></SettingsDataObject>" };
                File.AppendAllLines(Path.Combine(this.settingsDirectory, this.settingsFile), contents);
            }
        }

        public void LoadConfig()
        {
            if (File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
            {
                StreamReader textReader = File.OpenText(Path.Combine(this.settingsDirectory, this.settingsFile));
                object obj2 = new XmlSerializer(this.m_oConfig.GetType()).Deserialize(textReader);
                this.m_oConfig = (SettingsDataObject)obj2;
                textReader.Close();
            }
        }

        public void SaveConfig()
        {
            StreamWriter writer = File.CreateText(Path.Combine(this.settingsDirectory, this.settingsFile));
            Type type = this.m_oConfig.GetType();
            if (type.IsSerializable)
            {
                new XmlSerializer(type).Serialize((TextWriter)writer, this.m_oConfig);
                writer.Close();
            }
        }

        public void DeleteConfig()
        {
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
                {
                    // If file found, delete it    
                    File.Delete(Path.Combine(this.settingsDirectory, this.settingsFile));
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }
        public void CreateCache()
        {
            if (!Directory.Exists(this.settingsDirectory))
            {
                Directory.CreateDirectory(this.settingsDirectory);
            }
            if (!File.Exists(Path.Combine(this.settingsDirectory, this.cacheFile)))
            {
                File.Create(Path.Combine(this.settingsDirectory, this.cacheFile)).Dispose();
                string[] contents = new string[] { "<CacheDataObject></CacheDataObject>" };
                File.AppendAllLines(Path.Combine(this.settingsDirectory, this.cacheFile), contents);
            }
        }
        public void SaveJobID()
        {
            StreamWriter writer = File.CreateText(Path.Combine(this.settingsDirectory, this.cacheFile));
            Type type = this.m_oConfigJob.GetType();
            if (type.IsSerializable)
            {
                new XmlSerializer(type).Serialize((TextWriter)writer, this.m_oConfigJob);
                writer.Close();
            }
        }
        public void LoadJobID()
        {

            utils.Reg_Lesen("TruckersMP_Autorun", "jobID");

            /*
            if (File.Exists(Path.Combine(this.settingsDirectory, this.settingsFile)))
            { 
                try
                {

                    StreamReader textReader = File.OpenText(Path.Combine(this.settingsDirectory, this.cacheFile));
                    object obj2 = new XmlSerializer(this.m_oConfigJob.GetType()).Deserialize(textReader);
                    this.m_oConfigJob = (CacheDataObject)obj2;
                    textReader.Close();
                }
                catch { }
            }
            */
           
        }

        // Properties
        public SettingsDataObject Config
        {
            get =>
                this.m_oConfig;
            set =>
                this.m_oConfig = value;
        }

        public CacheDataObject Cache
        {
            get =>
                this.m_oConfigJob;
            set =>
                this.m_oConfigJob = value;
        }

    }
}

