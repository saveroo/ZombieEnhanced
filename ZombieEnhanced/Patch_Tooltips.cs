using System.Collections.Generic;
using System.Linq;
using Harmony;

namespace ZombieEnhanced
{
    [HarmonyPatch(typeof(ItemDefinition))]
    [HarmonyPatch("GetTooltipData")]
    public class Tooltip_Patcher
    {
        [HarmonyPostfix]
        static void GetTooltipData_Postfix(ItemDefinition __instance, Item item, bool full_detail, List<BubbleWidgetData> __result)
        {
            if (Entry.Config.Tooltip_Enabled)
            {
                if (__instance.id.Contains("heart:heart") || __instance.id.Contains("intestine:intestine") ||
                    __instance.id.Contains("brain:brain") || __instance.id.Contains("heart:brain"))
                {
                    __result[0] = (BubbleWidgetData) new BubbleWidgetTextData($"{__instance.GetItemName()}\n[(rskull){__instance.q_minus}|(skull){__instance.q_plus}][{__instance.stack_count}]",
                        UITextStyles.TextStyle.HintTitle, NGUIText.Alignment.Center, -1);
                }   
            }
        }
    }
}