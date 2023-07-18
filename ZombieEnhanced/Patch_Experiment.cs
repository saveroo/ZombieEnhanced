using System;
using Harmony;

namespace ZombieEnhanced
{
    [HarmonyPatch(typeof(AutopsyGUI))]
    [HarmonyPatch("Open", new Type[]{typeof(WorldGameObject)})]
    public class Patch_Periment
    {
        [HarmonyPrefix]
        public static void PatchOpen_Prefix(ref WorldGameObject craft_obj)
        {
            var body = craft_obj.GetBodyFromInventory(true); 
            Utilities.WriteToJSONFileDebug(body.inventory, $"prep/open_body_{body.id}");
            foreach (var organ in body.inventory)
            {
                Entry.Log($"(Organs in Body): {organ.id}");
                Utilities.WriteToJSONFileDebug(organ, $"prep/bodinv_{organ.id}");
                if (organ.id.Contains("brain"))
                    organ.definition.stack_count = 1;
                if (organ.id.Contains("intestine"))
                    organ.definition.stack_count = 1;
                if (organ.id.Contains("heart"))
                    organ.definition.stack_count = 1;
            }
        }
    }
    
    [HarmonyPatch(typeof(AutopsyGUI))]
    [HarmonyPatch("Hide", new Type[]{typeof(bool)})]
    public class Patch_Hide
    {
        [HarmonyPostfix]
        public static void PatchHide_Postfix(ref bool play_hide_sound)
        {
            Entry.Log("[AutopsyGUI Hide]");
            Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
            Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
            Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        }
    }

    [HarmonyPatch(typeof(AutopsyGUI))]
    [HarmonyPatch("ClearItemsGrid")]
    public class Patch_ClearItemsGrid
    {
        [HarmonyPrefix]
        public static void PatchClearItemsGrid_Prefix()
        {
            Entry.Log("[ClearItemsGrid Prefix]");
            Utilities.SetItemDefinition("heart:heart", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
            Utilities.SetItemDefinition("brain:brain", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
            Utilities.SetItemDefinition("intestine:intestine", "stack_count", Entry.Config.Item_Organs_Overhaul_StackCount);
        }
    }

    // [HarmonyPatch(typeof(AutopsyGUI))]
    // [HarmonyPatch("RemoveBodyPartFromBody", new Type[]{typeof(Item), typeof(Item)})]
    // public class Patch_Experiment
    // {
    //     [HarmonyPrefix]
    //     public static void PatchRemoveBodyPartFromBody_Prefix(ref Item body, ref Item item)
    //     {
    //         Entry.Log($"[RemoveBodyPartFromBody(): body={body.id} item={item.id}");
    //         Utilities.WriteToJSONFileDebug(body, $"prep/body_{body.id}");
    //         Utilities.WriteToJSONFileDebug(item, $"prep/item_{item.id}");
    //     }
    //
    //     [HarmonyPostfix]
    //     public static void PatchRemoveBodyPartFromBody_Postfix(ref Item body, ref Item item)
    //     {
    //         // if (item.id.Contains("brains:") || item.id.Contains("intestine:") || item.id.Contains("heart"))
    //         // {
    //         //     item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;
    //         // }
    //         if (item.id.Contains("brain"))
    //             item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;;
    //         if (item.id.Contains("intestine"))
    //             item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;;
    //         if (item.id.Contains("heart"))
    //             item.definition.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;;
    //     }
    // }
}