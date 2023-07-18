using System;
using System.Collections.Generic;
using System.Linq;
using Harmony;
using UnityEngine;

namespace ZombieEnhanced
{
    [HarmonyPatch(typeof(DropResGameObject))]
    [HarmonyPatch("Drop",
        new Type[]
        {
            typeof(Vector3), typeof(Item), typeof(Transform), typeof(Direction), typeof(float), typeof(int),
            typeof(bool), typeof(bool)
        })]
    public class PDropReal
    {
        [HarmonyPrefix]
        public static void pids(
            Vector3 pos,
            ref Item item,
            Transform parent,
            Direction direction = Direction.None,
            float force_factor = 1f,
            int selected_curve = -1,
            bool check_walls = true,
            bool force_stacked_drop = false)
        {
            if (Entry.Config.Drops_AutoStore_Enabled)
            {
                string[] items = Entry.Config.Drops_AutoStore_Items_Item;
                string[] places = Entry.Config.Drops_AutoStore_Items_Places;
                List<Item> drops = new List<Item>();
                List<Item> cantInsert = new List<Item>();
                drops.Add(item);
                bool canInsert = false;
                if (items.Contains(item.id))
                {
                    foreach (var place in places)
                    {
                        try
                        {
                            var map = WorldMap.GetWorldGameObjectByObjId(place);
                            if (map.CanInsertItem(item))
                            {
                                map.TryPutToInventory(drops, out cantInsert);
                                canInsert = true;
                            }
                        }
                        catch (Exception e)
                        {
                            
                        }
                    }

                    if (canInsert)
                    {
                        item = null;
                    }
                }   
            }
        }
    }
}