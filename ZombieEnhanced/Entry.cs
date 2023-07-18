using System;
using System.IO;
using System.Reflection;
using Harmony;
using NGTools;

namespace ZombieEnhanced
{
  public static class Entry
  {
    public static IniConfig IniInstance = IniConfig.Instance;
    public static Options Config;
    public static void Patch()
    {
      LoadIniSettings();
      try
      {
        HarmonyInstance.Create("com.saveroo.graveyardkeeper.zombieenhanced.mod").PatchAll(Assembly.GetExecutingAssembly());
      }
      catch (Exception ex)
      {
        Entry.Log(ex.ToString());
      }
    }
    
    // public static KeyCode GetKeyCode(char character)
    // {
    //   KeyCode code;
    //   if (_keycodeCache.TryGetValue(character, out code)) return code;
    //   int alphaValue = character;
    //   code = (KeyCode)Enum.Parse(typeof(KeyCode), alphaValue.ToString());
    //   _keycodeCache.Add(character, code);
    //   return code;
    // }

    public static void Log(string v)
    {
      string dir = ".\\QMods\\ZombieEnhanced\\log.txt";
      if (!File.Exists(dir))
        dir = "log.txt";
      using (StreamWriter streamWriter = File.AppendText(dir))
        streamWriter.WriteLine(v);
    }
    
    public static void LogItemData(string v, string filename = "item")
    {
      string dir = $".\\QMods\\ZombieEnhanced\\{filename}.txt";
      if (!File.Exists(dir))
        dir = $"{filename}.txt";
      using (StreamWriter streamWriter = File.AppendText(dir))
        streamWriter.WriteLine(v);
    }
    public static void LoadIniSettings()
    {
      try
      {
        if (!File.Exists(IniInstance.Path))
        {
          Config = new Options();
          // IniInstance.WriteConfigToFile(Config);
          IniInstance.WriteJsonConfig(Config);
        }
        else
        {
          // Config = IniInstance.DeserializeConfig<Options>(IniInstance.Path);
          Config = IniInstance.ReadJsonConfig();
          // IniInstance.WriteConfigToFile(Config);
        }
      }
      catch (Exception e)
      {
        Entry.Log(e.ToString());
      }
    }

    public static void reloadSettings()
    {
        try
        {
            if (!File.Exists(IniInstance.Path))
            {
                Config = new Options();
                IniInstance.WriteJsonConfig(Config);
            }
            else
            {
                Config = IniInstance.DeserializeConfig<Options>(IniInstance.Path);
            }
        }
        catch (Exception e)
        {
            Entry.Log(e.ToString());
        }
    }
    }
}
