using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Oculus.Newtonsoft.Json;
using UnityEngine;
using Application = System.Windows.Forms.Application;

namespace ZombieEnhanced
{
    public class Utilities
    {        
        public static float executedTime = 1f;
        private static float timestamp = 0f;
        public static float waitTime = 4f;
        public static float timer = 0f;
        public static string deployedPath = ".\\QMods\\ZombieEnhanced\\";

        public static void SetItemDefinition(string itemNameContains, string propName, object value)
        {
            var g = GameBalance.me.GetDataOrNull<ItemDefinition>(itemNameContains);
            if (g != null)
            {
                var propertyInfo = g.GetType().GetProperty(propName);
                if (propertyInfo != null) propertyInfo.SetValue((object) g, 
                    Convert.ChangeType(value, propertyInfo.PropertyType));
                else Entry.Log($"[SetItemDefinition]: item={itemNameContains} prop={propName} null value={value}");
            }
        }
        public static void DelayerDelegateWithKey(string key, GJCommons.VoidDelegate act)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (Input.GetKey(key))
            {
                if (Time.time >= timestamp)
                {
                    act();
                    timestamp = Time.time + executedTime;
                }
            }
        }
        
        public static void DelayerDelegateWithFlag(ref bool flag, GJCommons.VoidDelegate act)
        {
            if (flag)
            {
                if (Time.time >= timestamp)
                {
                    act();
                    timestamp = Time.time + executedTime;
                    flag = false;
                }
            }
        }
        
        public static void DelayerDelegateWithFlag(bool flag, GJCommons.VoidDelegate act)
        {
            if (flag)
            {
                if (Time.time >= timestamp)
                {
                    act();
                    timestamp = Time.time + executedTime;
                    flag = false;
                }
            }
        }
        
        public static void DelayerDelegateWithFlag(bool flag, GJCommons.VoidDelegate act, float waitsec)
        {
            if (!flag) return;
            timer += Time.deltaTime;
            if (timer > waitsec)
            {
                act();
                timer = timer - waitsec;
            }
        }

        public static void DelayedExecute(string name, Action<ZDialogs> act, float waitsec)
        {
            var babushka = ZDialogs.me;
            if (babushka.IsTimeToExecute(name, waitsec))
            {
                act(babushka);
                babushka.Update(name, waitsec);
            }
        }

        public static T CloneObject<T>(T obj, Action<T> act = null)
        {
            var serialize = JsonConvert.SerializeObject(obj);
            var deserialize = JsonConvert.DeserializeObject<T>(serialize);
            act?.Invoke(deserialize);
            return deserialize;
        }

        public static void DelayerDelegateWithKey(string key, GJCommons.VoidDelegate act, string text = null)
        {
            DelayerDelegateWithKey(key, () =>
            {
                GUIElements.me.dialog.OpenYesNo(text, act);
            });
        }
        public static void WriteToJSONFile<T>(List<T> its, string filename)
        {
            var items = JsonConvert.SerializeObject(its, Formatting.Indented);
            File.WriteAllText($".\\QMods\\ZombieEnhanced\\{filename}.json", items);
            Entry.Log($"[ZombieEnhanced] name={nameof(its)} type={its.GetType()} WriteToJsonFile={filename}");
        } 
        public static void WriteToJSONFileDebug<T>(List<T> its, string filename)
        {
            if (Entry.Config.Debug_Enabled)
            {
                var dir = $".\\QMods\\ZombieEnhanced\\";
                var items = JsonConvert.SerializeObject(its, Formatting.Indented);
                var spl = filename.Split('/');
                if (spl.Length > 1)
                {
                    Directory.CreateDirectory(spl[0]);
                    if (filename.Contains(":")) 
                        filename = filename.Replace(":", "-");
                }
                File.WriteAllText(dir+filename+".json", items);
                // Entry.Log($"[ZombieEnhanced] name={nameof(its)} type={its.GetType()} WriteToJsonFile={filename}");
            }
        } 
        public static void WriteToJSONFileDebug<T>(T its, string filename)
        {
            if (Entry.Config.Debug_Enabled)
            {
                var dir = $".\\QMods\\ZombieEnhanced\\"; 
                var items = JsonConvert.SerializeObject(its, Formatting.Indented);
                var spl = filename.Split('/');
                if (spl.Length > 1)
                {
                    Directory.CreateDirectory(spl[0]);
                    if (filename.Contains(":")) 
                        filename = filename.Replace(":", "-");
                } 
                File.WriteAllText(dir+filename+".json", items);
                // Entry.Log($"[ZombieEnhanced] name={nameof(its)} type={its.GetType()} WriteToJsonFile={filename}");
            }
        } 
        public static void WriteToJSONFile<T>(HashSet<T> its, string filename)
        {
            var items = JsonConvert.SerializeObject(its, Formatting.Indented);
            File.WriteAllText($".\\QMods\\ZombieEnhanced\\{filename}.json", items);
        } 
        public static void WriteToJSONFile<T>(T its, string filename)
        {
            var items = JsonConvert.SerializeObject(its, Formatting.Indented);
            File.WriteAllText($".\\QMods\\ZombieEnhanced\\{filename}.json", items);
        } 
        
        public static void WriteToJSONFile<T>(T its, string filename, string path = null)
        {
            var items = JsonConvert.SerializeObject(its, Formatting.Indented);
            File.WriteAllText(path == null ? $"{deployedPath}{filename}.json" : $"{filename}.json", items);
        } 
        public static void WriteToJSONFile<T>(T its, string filename, string ext = "json", string path = null)
        {
            var items = JsonConvert.SerializeObject(its, Formatting.Indented);
            File.WriteAllText(path == null ? $"{deployedPath}{filename}.{ext}" : $"{filename}.{ext}", items);
        } 
        public static void WriteDictToFile(Dictionary<string, IList> dic)
        {
            foreach (var entry in dic)
            {
                Entry.Log($"[{entry.Key}, {entry.Value}]");
                foreach (var entry2 in entry.Value)
                {
                    Entry.Log($"-[{entry2}]");
                }
            }
        }

        public static void WriteListToFile(IList<Item> list)
        {
            foreach (var entry in list)
            {
                Entry.Log($"[{entry.money}, {entry}]");
            }
        }
    }
}