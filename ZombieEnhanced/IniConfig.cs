using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Oculus.Newtonsoft.Json;

namespace ZombieEnhanced
{
    public class IniConfig
    {
        public string Path;
        public string defaultSection = "[Config]";
        public string modFolder = "QMods/ZombieEnhanced/";
        // public string modFolder = "";
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;
        private static readonly Lazy<IniConfig> _instance = new Lazy<IniConfig>(() => new IniConfig());
        public static IniConfig Instance => _instance.Value;

        private IniConfig(string IniPath = null)
        {
            // string combine = IniPath ?? modFolder + EXE + ".cfg";
            // if (!Directory.Exists(modFolder))
            //     combine = IniPath ?? EXE + ".cfg";
            // Path = new FileInfo(combine).FullName;
            string combine = IniPath ?? $"{modFolder}{EXE}.cfg";
            if (!Directory.Exists(modFolder))
                combine = IniPath ?? EXE + ".cfg";
            Path = new FileInfo(combine).FullName;
            Entry.Log("Path: " + Path);
        }

        public void WriteJsonConfig(Options configInstance)
        {
            Utilities.WriteToJSONFile(configInstance, @"ZombieEnhanced", "cfg", "./");
        }

        public Options ReadJsonConfig()
        {
            using (StreamReader file = File.OpenText(Instance.Path))
            {
                Options opts = JsonConvert.DeserializeObject<Options>(file.ReadToEnd());
                return opts;
                // itemLists.ForEach(i => i.definition.q_plus = 7);
            }   
        }

        public void WriteConfigToFile(Options objConfig, bool useDot = false)
        {
            using (StreamWriter text = File.CreateText(Instance.Path))
            {
                foreach (PropertyInfo property in objConfig.GetType().GetProperties())
                {
                    // var dot = property.GetValue((object) objConfig).ToString().Replace(",", ".");
                    text.WriteLine($"{(object) property.Name}={property.GetValue((object) objConfig).ToString().Replace(',', '.')}");
                    // text.WriteLine($"{(object) property.Name}={property.GetValue(dot)}");
                }
            }
        }
        
        public T DeserializeConfig<T>(string fileName, bool display = false)
        {
            System.Type type = typeof (T);
            object instance = Activator.CreateInstance(type);
            foreach (string readLine in File.ReadLines(fileName))
            {
                string[] strArray = readLine.Split('=');
                if (strArray.Length == 2)
                {
                    PropertyInfo property = type.GetProperty(strArray[0].Trim());
                    var ct = Convert.ChangeType(!display ? (object) strArray[1] : (object) strArray[1].Replace(".", ","), property.PropertyType);
                    Console.WriteLine(ct);
                    if (property != (PropertyInfo) null)
                        property.SetValue(instance, ct);
                }
            }
            return (T) instance;
        }
    }
}