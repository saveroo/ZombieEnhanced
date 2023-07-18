// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Reflection;
// using Oculus.Newtonsoft.Json;
// using Oculus.Newtonsoft.Json.Serialization;
// using UnityEngine;
//
// namespace ZombieEnhanced
// {
//      public class Datamining
//   {
//     public static void HandleKeypress(object sender, KeyCode key)
//     {
//       foreach (KeyValuePair<System.Type, SmartResourceHelperPool> keyValuePair1 in SmartResourceHelper.me.GetField<Dictionary<System.Type, SmartResourceHelperPool>>("_pools"))
//       {
//         foreach (KeyValuePair<string, UnityEngine.Object> keyValuePair2 in keyValuePair1.Value.loaded)
//           Datamining.PrintAll((object) keyValuePair2.Value, keyValuePair2.Key, "resource_pools", keyValuePair1.Value.type.ToString());
//       }
//     }
//
//     public static void PrintAll(object value, string name, params string[] folders)
//     {
//       try
//       {
//         string str = Path.Combine(folders);
//         Directory.CreateDirectory(str);
//         string path = Path.Combine(str, name.Replace(':', '.') + ".json");
//         using (StreamWriter streamWriter = new StreamWriter(path))
//         {
//           using (JsonTextWriter jsonTextWriter = new JsonTextWriter((TextWriter) streamWriter))
//           {
//             JsonSerializer.CreateDefault(new JsonSerializerSettings()
//             {
//               Formatting = Formatting.Indented,
//               NullValueHandling = NullValueHandling.Ignore,
//               ObjectCreationHandling = ObjectCreationHandling.Replace,
//               DefaultValueHandling = DefaultValueHandling.Include,
//               TypeNameHandling = TypeNameHandling.None,
//               MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//               PreserveReferencesHandling = PreserveReferencesHandling.None,
//               ContractResolver = (IContractResolver) new Datamining.MyContractResolver()
//             }).Serialize((JsonWriter) jsonTextWriter, value);
//             jsonTextWriter.Close();
//           }
//           streamWriter.Close();
//         }
//       }
//       catch (Exception ex)
//       {
//         Entry.Log(ex.ToString());
//       }
//     }
//
//     public class MyContractResolver : DefaultContractResolver
//     {
//       protected override IList<JsonProperty> CreateProperties(
//         System.Type type,
//         MemberSerialization memberSerialization)
//       {
//         // ISSUE: reference to a compiler-generated method
//         List<JsonProperty> list = ((IEnumerable<FieldInfo>) 
//           type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
//           .Where<FieldInfo>((JsonContract.Func<FieldInfo, bool>) (s => s.IsPublic || Attribute.IsDefined((MemberInfo) s, typeof (SerializeField)))).Select<FieldInfo, JsonProperty>((Oculus.Newtonsoft.Json.Serialization.Func<FieldInfo, JsonProperty>) (f => this.\u003C\u003En__0((MemberInfo) f, memberSerialization))).ToList<JsonProperty>();
//         list.ForEach((System.Action<JsonProperty>) (p =>
//         {
//           p.Writable = true;
//           p.Readable = true;
//         }));
//         return (IList<JsonProperty>) list;
//       }
//     }
//   }
// }