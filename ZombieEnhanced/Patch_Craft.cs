using System;
using System.Linq;
using Harmony;
using UnityEngine;
using Random = System.Random;

namespace ZombieEnhanced
{
    public static class Purist
    {
        public static int RandomBetween(int a, int b)
        {
            var random = new Random();
            return random.Next(a, b);
        }

        public static void ModifyCraftObject(ref CraftComponent __instance, string objIdContains, string craftIdContains, Action<CraftDefinition> act)
        {
            if (__instance.wgo.obj_id.Contains(objIdContains))
            {
                GameBalance.me.GetCraftsForObject(__instance.wgo.obj_id)
                    .ForEach(craft =>
                    {
                        // if (craft.id.Contains("grow_desk_planting"))
                        if (craft.id.Contains(craftIdContains))
                            act(craft);
                    });
            }
        }

        public static void ReduceSeedsNeedToZombieCraft(ref CraftComponent __instance)
        {
            ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting",
                craft => { craft.needs.ForEach(n => n.value = Entry.Config.Craft_ZombieFarm_Garden_Needs_Value); });
        }

        public static void AddCropWasteToZombieCraft(ref CraftComponent __instance)
        {
            ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting",
                craft =>
                {
                    if (!craft.output.Exists(o => o.id == "crop_waste"))
                    {
                        var cropWaste = new Item("crop_waste")
                        {
                            min_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Min.ToString()),
                            max_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Max.ToString()),
                            self_chance = {default_value = Entry.Config.Craft_ZombieFarm_ProduceWaste_Chance},
                        };
                        craft.output.Add(cropWaste);
                    }
                });
        }

        public static void AddItemToCraft(ref CraftComponent instance, string objIdContain, string craftIdContain,Item item)
        {
            ModifyCraftObject(ref instance, objIdContain, craftIdContain,
                craft =>
                {
                    if (!craft.output.Exists(o => o.id == item.id))
                    {
                        craft.output.Add(item);
                    }
                });
        }
        public static void SetNeedsToCraft(ref CraftComponent instance, string objIdContain, string craftIdContain, int value)
        {
            ModifyCraftObject(ref instance, objIdContain, craftIdContain,
                craft =>
                {
                    craft.needs.ForEach(n => n.value = value);
                });
        }

        public static void CraftModifyNeeds(string craftPlaceContains, int value, bool allNeeds = false)
        {
            try
            {
                var listCraft = GameBalance.me.craft_data;
                listCraft.ForEach(craft =>
                {
                    if (craft == null) return;
                    if (craft.needs == null || craft.needs.Count == 0) return;
                    if (allNeeds)
                        if (craft.id.Contains(craftPlaceContains))
                            craft.needs.ForEach(needs =>
                            {
                                if (needs.id.Contains("seeds"))
                                    needs.value = value;
                            });
                });
            }
            catch (Exception e)
            {
                Debug.Log(
                    $"[ZombieEnhanced] Couldn't modify output: {craftPlaceContains} vl:{value} an:{allNeeds} e={e}");
            }
        }

        public static void CraftAddOutput(string craftPlaceContains, Item item)
        {
            try
            {
                var listCraft = GameBalance.me.craft_data;
                listCraft.ForEach(craft =>
                {
                    if (craft == null) return;
                    if (craft.output == null || craft.output.Count == 0) return;
                    if (craft.id.Contains(craftPlaceContains))
                    {
                        if (!craft.output.Exists(o => o.id == item.id))
                        {
                            craft.output.Add(item);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Entry.Log($"[ZombieEnhanced] Couldn't modify output: {craftPlaceContains} {e}");
            }
        }

        [HarmonyPatch(typeof(CraftComponent))]
        [HarmonyPatch("FillCraftsList")]
        internal class FillCraftsList_Patcher
        {
            [HarmonyPrefix]
            public static void FillCraftsList_Prefix(CraftComponent __instance)
            {
                if (Entry.Config.Craft_ZombieFarm_SeedsNeed_Enabled)
                {
                    SetNeedsToCraft(ref __instance, 
                        "zombie_garden_desk", 
                        "grow_desk_planting", Entry.Config.Craft_ZombieFarm_Garden_Needs_Value);
                    SetNeedsToCraft(ref __instance, 
                        "zombie_vineyard_desk", 
                        "grow_vineyard_planting", Entry.Config.Craft_ZombieFarm_Vineyard_Needs_Value);
                }

                if (Entry.Config.Craft_ZombieFarm_ProduceWaste_Enabled)
                {
                    var cropWaste = new Item("crop_waste")
                    {
                        min_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Min.ToString()),
                        max_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Max.ToString()),
                        self_chance = {default_value = Entry.Config.Craft_ZombieFarm_ProduceWaste_Chance},
                    };
                    AddItemToCraft(ref __instance, 
                        "zombie_vineyard_desk", 
                        "vineyard_planting", 
                        cropWaste);
                    AddItemToCraft(ref __instance, 
                        "zombie_garden_desk", 
                        "grow_desk_planting",
                        cropWaste);
                }
            }
        }

        [HarmonyPatch(typeof(CraftComponent))]
        [HarmonyPatch("DoAction")]
        public class PatchDoAction
        {
            [HarmonyPostfix]
            static void PatchDoAction_Postfix(CraftComponent __instance)
            {
                if (__instance.HasLinkedWorker())
                {
                    if(Entry.Config.Debug_Enabled) 
                        Entry.Log($"PatchDoAction {__instance.HasLinkedWorker()} {__instance.wgo.progress} id={__instance.wgo.linked_worker.unique_id}");
                    if(__instance.wgo.progress < 2f)
                        HelperWorker.ZombieSaid(__instance.wgo.linked_worker.unique_id.ToString(),
                            HelperWorker.ZCraftStartProgressDialogue, 
                            __instance.wgo.linked_worker, 
                            __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, 
                            Entry.Config.Zombie_Dialogue_WaitSec);   
                    if((__instance.wgo.progress >= 0.3f && __instance.wgo.progress <= 0.4f) ||
                       (__instance.wgo.progress >= 0.60f && __instance.wgo.progress <= 0.80f)) 
                        HelperWorker.ZombieSaid(__instance.wgo.linked_worker.unique_id.ToString(),
                            HelperWorker.ZCraftProgressDialogue, 
                            __instance.wgo.linked_worker, 
                            __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, 
                            Entry.Config.Zombie_Dialogue_WaitSec);   
                }
            }
        }

        [HarmonyPatch(typeof(CraftComponent))]
        [HarmonyPatch("IsBodyPartExtractionCraft")]
        public class cccc
        {
            [HarmonyPrefix]
            public static void prosdasdcescsesn(Item ____current_item)
            {
                if (____current_item.id.Contains("heart:"))
                    ____current_item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;
                if (____current_item.id.Contains("brain:"))
                    ____current_item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;
                if (____current_item.id.Contains("intestine:"))
                    ____current_item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;
            }
            
            [HarmonyPostfix]
            public static void procescsesn(ref bool __result)
            {
                if (__result)
                {
                    Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                    Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                    Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                }
            }
        }

        [HarmonyPatch(typeof(CraftComponent))]
        [HarmonyPatch("ProcessFinishedCraft")]
        public class Craft_Patcher
        {
            [HarmonyPrefix]
            static void ProcessFinishedCraft_Prefix()
            {
                Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
            }
            
            [HarmonyPostfix]
            static void ProcessFinishedCraft_Postfix(CraftComponent __instance)
            {
                // Feature: [Organs_Overhaul]
                Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
                
                // Feature: [Zombie_Dialogue]
                if(__instance.HasLinkedWorker())
                    HelperWorker.ZombieSaid(
                        __instance.wgo.linked_worker.unique_id.ToString(),
                        HelperWorker.ZFinishCraftDialogue, 
                        __instance.wgo.linked_worker, 
                        __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, 
                        Entry.Config.Zombie_Dialogue_WaitSec);
            }
        }

        // [HarmonyPatch(typeof(CraftComponent))]
        // [HarmonyPatch("CraftAsPlayer")]
        // internal class CraftCraftAsPlayer
        // {
        //     [HarmonyPrefix]
        //     public static void PatchCraftAsPlayer_Prefix()
        //     {
        //         Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        //         Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        //         Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        //     }
        //     [HarmonyPostfix]
        //     public static void PatchCraftAsPlayer_Postfix()
        //     {
        //         Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        //         Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        //         Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        //     }
        //     
        // }
    }
}