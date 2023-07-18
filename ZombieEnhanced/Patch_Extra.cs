using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using Harmony;
using Oculus.Newtonsoft.Json;
using UnityEngine;
using Application = System.Windows.Forms.Application;
using Object = UnityEngine.Object;

namespace ZombieEnhanced
{
    // [HarmonyPatch(typeof(ItemCountGUI))]
    // [HarmonyPatch("Open")]
    // [HarmonyPatch(new Type[]{typeof(string), typeof(int), typeof(int), typeof(Action<int>), typeof(ItemCountGUI), typeof(SmartSlider), typeof(DialogButtonsGUI), typeof(Action<int>)})]
    // internal class PatchGUI
    // {
    //     [HarmonyPrefix]
    //     public static bool ZUI(
    //         ItemCountGUI __instance, 
    //         ref string __item_id, 
    //         ref int __min, 
    //         ref int __max, 
    //         ref Action<int> __on_confirm, 
    //         ref ItemCountGUI ____price_calculate_delegate,
    //         ref SmartSlider ____slider,
    //         ref DialogButtonsGUI ____dialog_buttons,
    //         ref Action<int> ____on_confirm
    //     )
    //     {
    //         if (__item_id != "ZombieEnhanced")
    //         {
    //             return false;
    //         }
    //         else
    //         {
    //             if (Input.GetKey(KeyCode.J))
    //             {
    //                 Dictionary<string, string> _buffs = new Dictionary<string, string>();
    //                 _buffs.Add("buff_skull", "Difficult Corpses");
    //                 _buffs.Add("buff_rage", "Rage");
    //                 _buffs.Add("buff_rage", "Wow");
    //                 __instance.Open();
    //                 __instance.header_label.text = "test";
    //                 __instance.window.height = __instance.height_with_price;
    //                 ____slider.Open(1, 1, _buffs.Count, (System.Action<int>) (
    //                         n => { __instance.price.text = _buffs.ElementAt(n).Key; }), 
    //                     true, 
    //                     true, 
    //                     1);
    //                 // __instance.____on_confirm = ;
    //                 ____dialog_buttons.Set("ok",
    //                     () => {
    //                         BuffsLogics.AddBuff(_buffs.ElementAt(2).Key);
    //                     }, 
    //                     !BaseGUI.for_gamepad ? (string) null : "back", 
    //                     (GJCommons.VoidDelegate) (() => __instance.OnClosePressed()), 
    //                     (string) null, (GJCommons.VoidDelegate) null, 
    //                     GameKey.Select, 
    //                     GameKey.Back);
    //                 __instance.price.text = _buffs.ElementAt(____slider.value).Key;
    //                 return true;   
    //             }
    //
    //             return false;
    //         }
    //     }
    // }
    
    // [HarmonyPatch(typeof(OptionsMenuGUI))]
    // [HarmonyPatch("Open")]
    // class Init_Patcher
    // {
    //     MenuItemGUI zUi;
    //
    //     [HarmonyPostfix]
    //     public static void PatchPostfix()
    //     {
    //         Dictionary<string, string> _buffs = new Dictionary<string, string>();
    //         _buffs.Add("buff_skull", "Difficult Corpses");
    //         _buffs.Add("buff_rage", "Rage");
    //         _buffs.Add("buff_rage", "Wow");
    //
    //         MenuItemGUI zUi;
    //         if ((UnityEngine.Object) zUi != (UnityEngine.Object) null)
    //             zUi.SetupSlider();
    //     }
    // }

    // [HarmonyPatch(typeof(OptionsMenuGUI))]
    // [HarmonyPatch("Open")]
    // internal class Option_Patcher
    // {
    //     [HarmonyPostfix]
    //     public static void PatchPostfix(BaseMenuGUI __instance)
    //     {
    //         Dictionary<string, string> _buffs = new Dictionary<string, string>();
    //         _buffs.Add("null", "None");
    //         _buffs.Add("buff_skull", "Difficult Corpses");
    //         _buffs.Add("buff_rage", "Rage");
    //         _buffs.Add("buff_rage", "Wow");
    //
    //         string selectedBuff = null;
    //         var min = 1;
    //         var max = _buffs.Count;
    //         var goBuff = new GameObject("buff_slider");
    //         __instance.Open();
    //         MenuItemGUI zUi = goBuff.AddComponent<MenuItemGUI>();
    //         // zUi.Init(__instance);
    //         // if ((UnityEngine.Object) zUi != (UnityEngine.Object) null)
    //         zUi.SetupSlider(1, min, max, 
    //             (Action<int>) ((i) =>
    //             {
    //                 selectedBuff = _buffs.ElementAt(i).Key;
    //             }),
    //             1,2);
    //     }
    // }

    // [Serializable]
    // public class ItemSpawner
    // {
    //     [SerializeField] public string Id { get; set; }
    //     [SerializeField] public string Icon { get; set; }
    //     [SerializeField] public string Name { get; set; }
    //     [SerializeField] public string Description { get; set; }
    //
    //     public ItemSpawner(string id, string icon, string name, string description)
    //     {
    //         Id = id;
    //         Icon = icon;
    //         Name = name;
    //         Description = description;
    //     }
    // }
    
    [HarmonyPatch(typeof (WorldGameObject))]
    [HarmonyPatch("CustomUpdate")]
    // [HarmonyPatch(typeof (MovementComponent))]
    // [HarmonyPatch("UpdateMovement")]
    internal class Extra_Patcher
    {
        public static float executedTime = 1f;
        private static float timestamp = 0f;
        private static List<string> addedBuff;
        private static int justOnce = 0;
        private static bool haveRun = false;
        private static bool reload = false;

        // private static List<ItemSpawner> _items;

        static string GetMethodName<T>(Expression<Action<T>> action)
        {
            MethodCallExpression methodCall = action.Body as MethodCallExpression;
            if (methodCall == null)
            {
                throw new ArgumentException("Only method calls are supported");
            }
            return methodCall.Method.Name;
        }

        private static void Prayer_Reset()
        {
            // Prayer Reset
            if (Input.GetKeyDown(Entry.Config.Prayer_Key))
            {
                var prayerParam = MainGame.me.player.GetParam("prayed_this_week");
                if (prayerParam > 0)
                {
                    GUIElements.me.dialog.OpenYesNo(
                        "Reset Sermon ?",
                        () =>
                        {
                            MainGame.me.player.SetParam("prayed_this_week", 0f);
                        }
                    );
                }
                else
                {
                    GUIElements.me.dialog.OpenOK("Sermon Already Resetted");
                }
            }
        }

        private static void Buffs_Activator()
        {
            Utilities.DelayerDelegateWithKey(Entry.Config.Buffs_Activator_Key, () =>
            {
                GUIElements.me.dialog.OpenYesNo($"Add {Entry.Config.Buffs_Activator_Title} Buff ?", 
                    () =>
                    {
                        BuffsLogics.AddBuff(Entry.Config.Buffs_Activator_Selected);
                        if (!addedBuff.Exists(k => k == Entry.Config.Buffs_Activator_Selected))
                            addedBuff.Add(Entry.Config.Buffs_Activator_Selected);
                    });
            });
        }
        
        private static void Buffs_Activator_remover()
        {
            Utilities.DelayerDelegateWithKey(
                Entry.Config.Buffs_Activator_Remover_Key, () =>
                {
                    GUIElements.me.dialog.OpenYesNo($"Remove {addedBuff.Count} Buff ?",
                        () =>
                        {
                            if (addedBuff.Count > 0)
                                addedBuff.ForEach(BuffsLogics.RemoveBuff);
                            addedBuff = new List<string>();
                        });
                    timestamp = Time.time + timestamp;
                });
        }

        private static void Item_Spawner_Package_Body()
        {
            Utilities.DelayerDelegateWithKey(
                Entry.Config.Item_Spawner_Package_Key,
                () =>
                {
                    List<Item> itemLists = new List<Item>();
                    // Item newHeart = Utilities.CloneObject(new Item("heart:heart_-2_3"));
                    Item newHeart = new Item("heart:heart_-2_3");
                    Item newIntestine = new Item("intestine:intestine_-2_3");
                    Item newBrain = new Item("brain:brain_-2_3");
                    itemLists.Add(new Item(newHeart));
                    itemLists.Add(new Item(newIntestine));
                    itemLists.Add(new Item(newBrain));
                    GUIElements.me.dialog.OpenYesNo($"Drop (rskull){newHeart.definition.q_minus} | (skull){newHeart.definition.q_plus} Organs Package ?", () =>
                    {
                        MainGame.me.player.DropItems(itemLists);
                    }, null);
                });
            // Utilities.DelayerDelegateWithKey(
            //     Entry.Config.Item_Spawner_Package_Key,
            //     () =>
            //     {
            //         using (StreamReader file = File.OpenText(@"heart_10.json"))
            //         {
            //             Item heartItem = JsonConvert.DeserializeObject<Item>(file.ReadToEnd());
            //             List<Item> itemLists = new List<Item>();
            //             itemLists.Add(new Item("brain:brain_-2_3"));
            //             itemLists.Add(new Item("heart:heart_-2_3"));
            //             itemLists.Add(new Item("intestine:intestine_-2_3"));
            //             itemLists.Add(new Item("heart:heart_3_3"));
            //             itemLists.Add(heartItem);
            //             // itemLists.ForEach(i => i.definition.q_plus = 7);
            //             MainGame.me.player.DropItems(itemLists);
            //         }
            //         
            //     }, $"Drop (rskull)2 (skull)10 Organs Package ?");
        }

        private static void Item_Spawner_Logic()
        {
            Utilities.DelayerDelegateWithKey(Entry.Config.Item_Spawner_Key, () =>
            {
                var item = GameBalance.me.items_data.FirstOrDefault(i => i.id == Entry.Config.Item_Spawner_Id);
                GUIElements.me.dialog.OpenYesNo($"({item.icon}) Spawn {Entry.Config.Item_Spawner_Qty} of [{item.GetItemName()}] ?\n\n{item.GetItemDescription()}", (() =>
                { 
                    // MainGame.me.player.AddToInventory(Entry.Config.Item_Spawner_Id, Entry.Config.Item_Spawner_Qty);
                    if(Entry.Config.Debug_Enabled)
                        Utilities.WriteToJSONFile(new Item(Entry.Config.Item_Spawner_Id), $"debug\\{Entry.Config.Item_Spawner_Id}");

                    for (int i = 0; i < Entry.Config.Item_Spawner_Qty; i++)
                    { 
                        MainGame.me.player.DropItem(new Item(Entry.Config.Item_Spawner_Id));
                    }
                    // foreach (string item_id in strArray)
                    // {
                    //     Item obj = new Item(item_id, 1);
                    //     if (obj?.definition != null)
                    //         bodyFromInventory.AddItem(obj, true);
                    // }
                }));
            });
        }

        private static void RunJustOnce()
        {
            if (justOnce == 0)
            {
                ZeGUI.Loader();
                
                if (Entry.Config.Drops_StoreThenCleaned_OnReload)
                { 
                    StoreToPossibleInventory();
                }
                
                if (Entry.Config.Item_Organs_Overhaul_Enabled)
                    GameBalance.me.items_data.ForEach(i =>
                    {
                        try
                        {
                            if (i.id.StartsWith("brain:brain")
                                || i.id.StartsWith("intestine:intestine")
                                || i.id.StartsWith("heart:heart"))
                            {
                                // if (Entry.Config.Debug_Enabled)
                                // {
                                //     // CraftDefinition brain = GameBalance.me.GetDataOrNull<CraftDefinition>("ex:mf_preparation_2:brain");
                                //     // CraftDefinition intestine = GameBalance.me.GetDataOrNull<CraftDefinition>("ex:mf_preparation_2:intestine");
                                //     // CraftDefinition heart = GameBalance.me.GetDataOrNull<CraftDefinition>("ex:mf_preparation_2:heart");
                                //     // Entry.Log($"[Organs Overhaul]: {i.q_plus}");
                                //     // Utilities.WriteToJSONFileDebug(i, $"bugs/{i.id}");
                                //     // Utilities.WriteToJSONFileDebug(brain, $"prep/{brain.id}");
                                //     // Utilities.WriteToJSONFileDebug(intestine, $"prep/{intestine.id}");
                                //     // Utilities.WriteToJSONFileDebug(heart, $"prep/{heart.id}");
                                // }UpdateWorkersWithReloadFlag
                                var r = i.id.Substring(i.id.Length - 1, 1);
                                i.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;
                                i.q_plus = (Int32.Parse(r)) + Entry.Config.Item_Organs_Additional_Value;
                            }
                        }
                        catch (Exception e)
                        {
                            Entry.Log($"[Organs Overhaul Error]: id={i.id} e={e}");
                        }
                    });

                if (Entry.Config.Item_Embalm_Overhaul_Enabled)
                    GameBalance.me.items_data.ForEach(i =>
                    {
                        var list = new List<string>();
                        var embalm = i.id.Substring(i.id.Length - 1, 1);
                        // list.Add("embalm_0_1");
                        // list.Add("embalm_1_1");
                        // list.Add("embalm_2_0");
                        // list.Add("embalm_-1_1");
                        // list.Add("embalm_-1_-1");
                        list.Add("embalm_-2_2");
                        if (i.id.Equals("embalm_-2_2")) i.q_plus = Int32.Parse(embalm) + Entry.Config.Item_Embalm_Additional_Value;
                    });
                HelperWorker.UpdateWorkersWithReloadFlag(!haveRun);

                if(Entry.Config.Debug_Enabled)
                    MainGame.me.player.Say("ZE Overhaul Initiated");
                justOnce = 1;
                haveRun = true;
            }
        }

        private static void Item_Spawner_Generator(string _key)
        {
            Utilities.DelayerDelegateWithKey(_key, () =>
            {
                GameBalance.me.items_data.ForEach(k =>
                {
                    // _items.Add(new ItemSpawner(k.id, k.icon, k.GetItemName(), k.GetItemDescription()));
                    var txt = $"new ItemSpawner(\"{k.id}\", \"{k.icon}\", \"{k.GetItemName()}\", @\"{k.GetItemDescription()}\"),";
                    Entry.LogItemData(txt, "item");
                });
            });
        }

        private static void InGameReload()
        {
            Utilities.DelayerDelegateWithKey(Entry.Config.InGame_ReloadConfig_Key, () =>
            {
                Entry.LoadIniSettings();
                MainGame.me.player.Say("ZE: Config Reloaded");
                // var keys = Entry.Config.GetType().GetProperties();
                // Dictionary<string, string> str = new Dictionary<string, string>();
                // for (int i = 0; i < keys.Length; i++)
                // {
                //     if(keys[i].Name.ToLower().Contains("key"))
                //         str.Add(keys[i].Name, keys.GetValue(i).ToString());
                // }
                // MainGame.me.player.Say(string.Join("\n",str.Select(s => s.Key + "=" + s.Value).ToArray()));
                if (Entry.Config.InGame_ReloadConfig_Rerun)
                {
                    justOnce = 0;
                }
                reload = true;
            });
        }

        private static void StoreToPossibleInventory()
        {
            List<string> str = Entry.Config.Drops_AutoStore_Items_Item.ToList();
            string[] places = Entry.Config.Drops_AutoStore_Items_Places;
            DropResGameObject[] componentsInChildren = MainGame.me.world_root.GetComponentsInChildren<DropResGameObject>(false);
            WorldGameObject[] wgo = MainGame.me.world_root.GetComponentsInChildren<WorldGameObject>(false);
            DropResGameObject dropResGameObject1 = (DropResGameObject) null;
            List<DropResGameObject> dropResGameObjects = new List<DropResGameObject>();
            List<Item> drops = new List<Item>();
            List<Item> dropsToPut = new List<Item>();
            List<Item> cantPut = new List<Item>();
            List<WorldGameObject> inventories = WorldMap.GetWorldGameObjectsByObjId(places);
            List<Item> cantInsert = null;
            Vector2 vector2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            // Vector2 worldPoint = MainGame.me.world_cam.ScreenToWorldPoint(vector2);

            if (componentsInChildren.Length > 0)
            {
                // // Filtering
                // foreach (var dropResGameObject in componentsInChildren)
                // {
                //     // Entry.Log($"[DROPS] name={dropResGameObject.name} item_id={dropResGameObject.res.id}");
                //     if (str.Contains(dropResGameObject.res.id))
                //     {
                //         
                //         // Sort Drops
                //         // dropResGameObject1 = dropResGameObject;
                //         dropResGameObjects.Add(dropResGameObject);
                //         drops.Add(dropResGameObject.res);
                //         // Put to Inventory
                //     }
                // }
                //
                // foreach (var inventory in inventories)
                // {
                //     var drop = (List<Item>) dropResGameObjects.Select(s => s.res);
                //     inventory.PutToAllPossibleInventories(drop, out cantInsert);
                //     foreach (var d in cantInsert)
                //     {
                //         ((List<Item>) dropResGameObjects.Select(ds => ds.res)).Remove(d);
                //     }
                //
                //     foreach (var itemToput in dropResGameObjects)
                //     {
                //         WorldMap.OnDropItemRemoved(itemToput.res);
                //         itemToput.is_collected = true;
                //     }
                // }

                // foreach (var drop in componentsInChildren)
                // {
                //     if(str.Contains(drop.res.id)) drops.Add(drop.res);
                // }
                // foreach (var inventory in inventories)
                // {
                //     if(str.Contains(drop.res.id)) drops.Add(drop.res);
                //
                //     inventory.PutToAllPossibleInventories(drops, out cantInsert);
                // }
                // MainGame.me.player.Say($"Can't put {drops.Count} stone/log/ore");

                foreach (var dropResGameObject2 in componentsInChildren)
                {
                    if (str.Contains(dropResGameObject2.res.id))
                    {
                        foreach (var inventory in inventories)
                        {
                            drops.Add(dropResGameObject2.res);
                            inventory.PutToAllPossibleInventories(drops, out cantInsert);
                        }
                        // Entry.Log($"GGGG: {dropResGameObject2.gameObject.name}");
                        if ((UnityEngine.Object) dropResGameObject2 == (UnityEngine.Object) null ||
                            dropResGameObject2.res == null || dropResGameObject2.res.definition == null)
                        {
                            dropResGameObject1 = dropResGameObject2;
                            dropResGameObject1.is_collected = true;
                        }
                            WorldMap.OnDropItemRemoved(dropResGameObject2.res);
                            dropResGameObject2.is_collected = true;
                            // Object.Destroy(dropResGameObject);
                    }
                    // Utilities.WriteToJSONFileDebug(dropResGameObject.res, $"dropitems/{dropResGameObject.res.id}");
                    // Entry.Log($"id={dropResGameObject.res.id} type={dropResGameObject.res.definition.type}");
                    // Entry.Log($"id={dropResGameObject.res.id} prodType={dropResGameObject.res.definition.product_types.Join()}");
                    // Entry.Log($"id={dropResGameObject.res.id} prodType={dropResGameObject.res.definition.product_types.Join()}");
                    // if ((!dropResGameObject.res.is_worker
                    //     || !dropResGameObject.res.is_bag
                    //     || !dropResGameObject.res.is_unique
                    //     || !dropResGameObject.res.is_multiquality))
                    // {
                    //     dropResGameObject.DestroyComponent(2f);
                    //     dropResGameObject.DestroyGO(2f);
                    //     // WorldMap.OnDropItemRemoved(dropResGameObject.res);
                    // }
                }
                MainGame.me.player.Say($"Handled {componentsInChildren.Length} Log/Stone");
            }
            // foreach (var componentsInChild in MainGame.me.world_root.GetComponentsInChildren<Item>())
            // {
            //     componentsInChild.RemoveItem(componentsInChild);
            //     WorldMap.OnDropItemRemoved(componentsInChild);
            // }
            // foreach (var drop in drops)
            // {
            //
            //     WorldMap.OnDropItemRemoved(drop);
            // }
        }
        
        [HarmonyPostfix]
        public static void PatchPostfix(WorldGameObject __instance, ref Item ____data)
        {
            // In-Game Reload
            InGameReload();
            
            if (Entry.Config.Prayer_Reset_Enabled) 
                Prayer_Reset();
            
            if (Entry.Config.Buffs_Activator_Enabled)
                Buffs_Activator();

            Buffs_Activator_remover();
            
            // Item Spawner
            Item_Spawner_Logic();
            Item_Spawner_Package_Body();
            RunJustOnce();
            HelperWorker.UpdateWorkersWithReloadFlag(ref reload);



            // Item_Spawner_Generator("g");

            // Debug, serialize Game Resource to .json
            // SerializeThis(Entry.Config.Debug_Log_PropName);

            // if (__instance.obj_id.StartsWith("zombie_garden_desk"))
            // {
            //     MainGame.me.GetListOfWorldObjects().ForEach(k =>
            //     {
            //         Entry.Log($"WGO:{k.obj_id}");
            //     });
            //     var obj = GameBalance.me.GetCraftsForObject("zombie_garden_desk_2");
            //     Utilities.WriteToJSONFile(obj, "Getcraftsforobject");
            //     obj.ForEach(k =>
            //     {
            //         k.output.Add(new Item("crop_waste"));
            //         k.needs.ForEach(kk => kk.value = 1);
            //         k.item_output.Add(new Item("crop_waste"));
            //         Utilities.WriteToJSONFile(k, "craftdef");
            //         Utilities.WriteToJSONFile(new Item("crop_waste"), "crop_waste");
            //     });
            //     Utilities.WriteToJSONFile(GameBalance.me.GetItemCraftsIn("zombie_garden_desk_2"), "getitemcraftsin");
            //     Utilities.WriteToJSONFile(GameBalance.me.GetCraftsForObject(__instance.obj_id), "getcraftsforobject");
            // }


            // Prayer 0
            // if (false)
            // {

            // if (Time.time >= timestamp)
            // {
            //     GameBalance.me.items_data.ForEach(k =>
            //     {
            //         _items.Add(new ItemSpawner(k.id, k.icon, k.GetItemName(), k.GetItemDescription()));
            //         // var txt = $"new ItemSpawner(\"{k.id}\", \"{k.icon}\", \"{k.GetItemName()}\", @\"{k.GetItemDescription()}\"),";
            //         // Entry.LogItemData(txt, "item");
            //     });
            //     WriteToJSONFile(_items);
            //     timestamp = Time.time + executedTime;
            // }
            // GameBalance.me.bodies_data.ForEach((k) =>
            // {
            //     GUIElements.me.dialog.OpenDialog($"link:{k.linked_item_id}\n\n{k.tier}\n\n{k.id}"
            //     , "OKK", null);
            // });


            // Item Fetcher
            // GameBalance.me.items_data.ForEach((k) =>
            // {
            //     Entry.Log("====ITEM");
            //     Entry.Log($"Id: {k.id}");
            //     Entry.Log($"Id: {k.GetItemDescription()}");
            //     Entry.Log($"Name: {k.GetItemName()}");
            //     Entry.Log($"Desc: {k.GetItemDescription()}");
            //     Entry.Log($"Eff: {k.efficiency}");
            //     Entry.Log($"prodTier: {k.product_tier}");
            // });

            // GUIElements.me.dialog.OpenOK("Boxtest", 
            //     null, 
            //     "TestBox");
            // Dictionary<string, string> _buffs = new Dictionary<string, string>();
            // _buffs.Add("buff_skull", "Difficult Corpses");
            // _buffs.Add("buff_rage", "Rage");
            // _buffs.Add("buff_rage", "Wow");

            // GUIElements.me.item_count.Open("garlic", 1, _buffs.Count,
            //     i => {
            //         BuffsLogics.AddBuff(_buffs.ElementAt(i).Key);
            //     });


            // GJTimer.AddTimer(1f, (GJTimer.VoidDelegate) (() =>
            // {
            //     // BuffsLogics.AddBuff("buff_skull");
            // }));

            // GameBalance.me.buffs_data.ForEach((k) =>
            // {
            //     Entry.Log($"{k.id}: {k.GetLocalizedName()}: {k.se_start.default_value}");
            //     Entry.Log($"{k.id}: {k.GetLocalizedName()}: {k.se_finish.default_value}");
            // });
            // GameBalance.me.auras_data.ForEach(k =>
            // {
            //     Entry.Log($"{k.id}, {k.radius}, {k.time_tick}, {k.res.progress}");
            // });
            // MainGame.me.player_char.player.DoSpawnPrayBuff();
            // MainGame.me.player_component.DoSpawnPrayBuff();

            // WriteListToFile(MainGame.me.player.data.GetAllRatBuffs());
            // GameBalance.me.GetData<BuffDefinition>(Player.buff_id);
            // Entry.Log(playerBuff.buff_id);
            // WriteDictToFile(GameBalance.me.GetAllDataListsAndGoogleTabs());
            // Entry.Log("======");
            // Entry.Log("Pray ID: " + __instance.pray_craft.id);
            // Entry.Log("Pray Buff: " + __instance.pray_craft.buff);
            // Entry.Log("Pray Type: " + __instance.pray_craft.craft_type);
            // Entry.Log("Pray CustomName: " + __instance.pray_craft.custom_name);
            // Entry.Log("Pray Linked Buff: " + __instance.pray_craft.linked_buffs);
            // Entry.Log("Pray Linked Perks: " + __instance.pray_craft.linked_perks);
            // Entry.Log("Pray dur_par: " + __instance.pray_craft.dur_parameter);
            // Entry.Log("Pray dur_needs: " + __instance.pray_craft.dur_needs_item);
            // Entry.Log("Pray description: " + __instance.pray_craft.GetDescription());
            // Entry.Log("Pray description: " + __instance.pray_craft);
            // Entry.Log("Pray description: " + __instance.pray_craft);
            // if (__instance.pray_craft != null)
            // {
            //     Stats.DesignEvent("Pray:" + __instance.pray_craft.id);
            // }
            // }
        }
    }
}