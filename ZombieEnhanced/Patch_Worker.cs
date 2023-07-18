using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using HarmonyLib;
using UnityEngine;

namespace ZombieEnhanced
{

    public class ZDialog
    {
        public string DialogueName;
        public string DialogChoosen;
        public float DialogChances;
        public bool DialogSuccess;
        public float lastExecutedTime;
        public float currentTime;
        public float nextExecutedTime;
    }
    
    public class ZDialogs
    {
        public static List<ZDialog> Dialogues = new List<ZDialog>();
        private static readonly Lazy<ZDialogs> _instance = new Lazy<ZDialogs>(() => new ZDialogs());
        public static ZDialogs me => _instance.Value;

        public void Create(string name, float next)
        {
            var dialog = new ZDialog();
            var d = Dialogues.Exists(
                s => string.Equals(s.DialogueName, name, StringComparison.CurrentCultureIgnoreCase));
            if (d) return;
            dialog.DialogueName = name;
            dialog.currentTime = Time.time;
            dialog.nextExecutedTime = Time.time + next;
            Dialogues.Add(dialog);
            Init(name, next);
        }
        public bool Init(string name, float next)
        {
            ZDialog dialog;
            var d = Dialogues.Exists(
                s => string.Equals(s.DialogueName, name, StringComparison.CurrentCultureIgnoreCase));
            if (!d)
            {
                Create(name, next);
                // Init(name, next);
                return d;
            }
            dialog = GetDialog(name);
            dialog.currentTime = Time.time;
            return d;
        }
        public ZDialog GetDialog(string name)
        {
            var d = Dialogues.Single(s => string.Equals(s.DialogueName, name, StringComparison.CurrentCultureIgnoreCase));
            if (d != null)
                return d;
            return (ZDialog) null;
        }
        public void Update(string name, float next)
        {
            GetDialog(name).lastExecutedTime = Time.time;
            GetDialog(name).nextExecutedTime = Time.time + next;
        }

        public bool IsTimeToExecute(string name, float next)
        {
            if (Init(name, next))
            {
                Utilities.WriteToJSONFileDebug(Dialogues, $"debug/initPhase");
                var d = GetDialog(name);
                d.currentTime = Time.time;
                if (d.currentTime >= d.nextExecutedTime)
                {
                    if (d.nextExecutedTime > d.lastExecutedTime)
                    { 
                        d.lastExecutedTime = Time.time;
                        d.nextExecutedTime = d.currentTime + next;
                        Utilities.WriteToJSONFileDebug(Dialogues, $"debug/executedPhase");
                        return true;
                    }
                }
            }
            Utilities.WriteToJSONFileDebug(Dialogues, $"debug/initPhaseFailed");
            return false;
        }
    }

    public class HelperWorker
    {
        public static List<Worker> SavedWorkers;
        public static List<string> ZPorterStartDialogue = new List<string>()
        {
            "Go.. Go...!!",
            "Zombo Boooore!",
            "When.. Milord!?",
            "argghhhh again!?..",
            "eeet.. breens..",
            "yyoooyoo....",
            "Bbbbbbaabaaba am gooin...",
            ""
        };
        public static List<string> ZPorterEndDialogue = new List<string>()
        {
            "Fyuhh.. a..m zired!!",
            "aaa vring vomething voss!",
            "Brreens breens.. am heeer..",
            "Grhhhhhr.. whyyy??",
            "Zee yaa...",
            "Zere is morr..",
            ""
        };
        public static List<string> ZFinishCraftDialogue = new List<string>()
        {
            "Zork.. zork... Groo..",
            "Gorrdd Gorrdd...",
            "Iz dooon.. voss..",
            "Waaaaaa...",
            "Umm.."
        };
        public static List<string> ZCraftProgressDialogue = new List<string>()
        {
            "Haffway...",
            "Groo.. yee groo..",
            "Zombee zombee.. Sombe!",
            "In your heaaad..",
            "Arrmooz voss...",
            "Umm.."
        };
        public static List<string> ZCraftStartProgressDialogue = new List<string>()
        {
            "I Kravt fo you~",
            "Gimme yer head!!",
            "Zo good kraf, zo must krave..",
            "Kraf kraf kraf..",
            "Umm.."
        };
        public static List<string> ZPorterUpdateDialogue = new List<string>()
        {
            "Runnn",
        };

        public static void UpdateWorkersWithReloadFlag(ref bool flag)
        {
            Utilities.DelayerDelegateWithFlag(ref flag,() =>
            {
                var savedWorkers = MainGame.me.save.workers;
                foreach (var worker in HelperWorker.SavedWorkers)
                {
                    // Entry.Log($"Y Key, id={worker.worker_unique_id} wgoEff={worker.worker_wgo.GetParam("working_k")} dataParamEff={worker.worker_wgo.data.GetParam("working_k")}");
                    // Entry.Log($"Y Key, id={worker.worker_unique_id} wgoSpeed={worker.worker_wgo.GetParam("speed")} dataParamSpeed={worker.worker_wgo.data.GetParam("speed")}");
                    SetWorkerEfficiency(savedWorkers.GetWorker(worker.worker_unique_id));
                    SetWorkerSpeed(savedWorkers.GetWorker(worker.worker_unique_id));
                    // worker.worker_wgo.Say($"Id={worker.worker_unique_id}\nSpeed={Entry.Config.Zombie_MovementSpeed}\nEff={worker.worker_wgo.GetParam("working_k")}%");
                    worker.worker_wgo.Say($"Ahhaa...");
                }
                // Utilities.WriteToJSONFile(SavedWorkers, "debug/savedWorkers");
            });
        }
        
        public static void UpdateWorkersWithReloadFlag(bool flag)
        {
            Utilities.DelayerDelegateWithFlag(flag,() =>
            {
                var savedWorkers = MainGame.me.save.workers;
                foreach (var worker in HelperWorker.SavedWorkers)
                {
                    // Entry.Log($"Y Key, id={worker.worker_unique_id} wgoEff={worker.worker_wgo.GetParam("working_k")} dataParamEff={worker.worker_wgo.data.GetParam("working_k")}");
                    // Entry.Log($"Y Key, id={worker.worker_unique_id} wgoSpeed={worker.worker_wgo.GetParam("speed")} dataParamSpeed={worker.worker_wgo.data.GetParam("speed")}");
                    SetWorkerEfficiency(savedWorkers.GetWorker(worker.worker_unique_id));
                    SetWorkerSpeed(savedWorkers.GetWorker(worker.worker_unique_id));
                    // worker.worker_wgo.Say($"Id={worker.worker_unique_id}\nSpeed={Entry.Config.Zombie_MovementSpeed}\nEff={worker.worker_wgo.GetParam("working_k")}%");
                    worker.worker_wgo.Say($"Ahhaa...");
                }
            });
        }

        
        public static IEnumerable<WaitForSeconds> WaitBeforeSaid()
        {
            var timeExecute = Time.time + new System.Random().Next(0, 5);
            yield return new WaitForSeconds(timeExecute);;
        }
        public static void ZombieSaid(object str, long zombieId)
        {
            var savedWorkers = MainGame.me.save.workers;
            var workerId = SavedWorkers.FirstOrDefault(w => w.worker_unique_id == zombieId);
            savedWorkers.GetWorker(workerId.worker_unique_id).worker_wgo.Say($"{str}");;
        }
        public static void ZombieSaid(List<string> dialogue, WorldGameObject worker, bool flag)
        {
            if (flag)
            {
                var num = new System.Random().Next(100);
                var chances = (num * 100) / 100; // 20, 30
                if (chances < Entry.Config.Zombie_Dialogue_Chances)
                { 
                    var timeExecute = Time.time + new System.Random().Next(0, 3);
                    new WaitForSeconds(timeExecute);
                    var random = new System.Random().Next(0, dialogue.Count);
                    worker.Say($"{dialogue[random]}", 
                        null, 
                        null, 
                        SpeechBubbleGUI.SpeechBubbleType.Talk, 
                        (SmartSpeechEngine.VoiceID) Entry.Config.Zombie_Dialogue_Voice);
                    
                }
            }
        }
        public static void ZombieSaid(List<string> dialogue, WorldGameObject worker, bool flag, float percentage, float waitsec = 1f)
        {
            if (flag)
            {
                Utilities.DelayerDelegateWithFlag(flag, () =>
                {
                    var num = new System.Random().Next(100);
                    var chances = (num * 100) / 100; // 20, 30
                    if (chances < percentage)
                    { 
                        var random = new System.Random();
                        var index = random.Next(dialogue.Count);
                        worker.Say($"{dialogue[index]}", 
                            null, 
                            null, 
                            SpeechBubbleGUI.SpeechBubbleType.Talk, 
                            (SmartSpeechEngine.VoiceID) Entry.Config.Zombie_Dialogue_Voice);
                    }
                }, waitsec);
            }
        }
        
        public static void ZombieSaid(string name, List<string> dialogue, WorldGameObject worker, bool flag, float percentage, float waitsec = 1f)
        {
            if (!Entry.Config.Zombie_Dialogue_Enabled) return;
            if (flag)
            {
                // Worker mWorker = MainGame.me.save.workers.GetWorker(worker.worker_unique_id);;
                // Entry.Log($"ZombieSaid .wgo.uniq: {mWorker.worker_wgo.unique_id}");
                // Entry.Log($"ZombieSaid .wgo.worker_uniq: {mWorker.worker_wgo.worker_unique_id}");
                // Entry.Log($"ZombieSaid .worker_uniq: {mWorker.worker_unique_id}");
                // if (mWorker == null) return;
                // if (worker.linked_worker.unique_id <= 0L) return;
                // var realName = worker.worker_unique_id.ToString() + "_" + name;
                var realName = name;
                Utilities.DelayedExecute(realName, (zd) =>
                {
                    var num = new System.Random().Next(100);
                    var chances = (num * 100) / 100; // 20, 30
                    if (chances < percentage)
                    { 
                        var random = new System.Random();
                        var index = random.Next(dialogue.Count);
                        zd.GetDialog(realName).DialogChances = chances;
                        zd.GetDialog(realName).DialogChoosen = dialogue[index];
                        worker.Say($"{dialogue[index]}",
                            null, 
                            null,
                            SpeechBubbleGUI.SpeechBubbleType.Talk, 
                            (SmartSpeechEngine.VoiceID) Entry.Config.Zombie_Dialogue_Voice);
                    } 
                    zd.GetDialog(realName).DialogSuccess = chances < percentage;
                }, waitsec);
            }
        }

        public static void SetWorkerSpeed(Worker _worker)
        {
            try
            {
                var paramData = _worker.worker_wgo.data;
                paramData?.SetParam("speed", Entry.Config.Zombie_MovementSpeed);
            }
            catch (Exception e)
            {
                Entry.Log($"[SetWorkerSpeed] id={_worker.worker_unique_id} error {e}");
            }
        }

        public static void SetWorkerEfficiency(Worker _worker)
        {
            try
            {
                var paramData = _worker.worker_wgo.data;
                int positive;
                _worker.worker_wgo.data.GetBodySkulls(out int _, out positive, out int _, true);
                var patchedBaseValue = (float) Entry.Config.Zombie_BaseEfficiency;
                var patchedMaxValue = (patchedBaseValue * 100) / (float) Entry.Config.Zombie_MaxEfficiency;
                var patchedExtraValue = positive + (float)Entry.Config.Zombie_ExtraEfficiency;
                paramData?.SetParam("working_k", patchedExtraValue / patchedMaxValue);
            }
            catch (Exception e)
            {
                Entry.Log($"[SetWorkerSpeed] id={_worker.worker_unique_id} error {e}");
            }
        }
    }
    
    [HarmonyPatch(typeof(Worker))]
    [HarmonyPatch("GetWorkerEfficiencyText")]
    internal class PatchWorkerEfficiencyText
    {
        [HarmonyPrefix]
        static bool PatchPrefix(Worker __instance, ref string __result)
        {
            __instance.UpdateWorkerLevel();
            __instance.worker_wgo.data.GetBodySkulls(out int negative, out int positive, out int posAvailable, true);
            var speed = __instance.worker_wgo.data.GetParam("speed");
            var percentage = Mathf.RoundToInt(__instance.worker_wgo.data.GetParam("working_k", 0.0f) * 100f).ToString() + "%";
            var id = __instance.worker_wgo.worker_unique_id;
            string extraTooltip;
            
            if(!GUIElements.me.autopsy.is_just_opened) 
                extraTooltip = $"ID: {id} ({percentage})\nQuality:[(rskull){negative}|(skull){positive}|a{posAvailable}]\nSpeed: {speed}\n";
            else 
                extraTooltip = $"ID: {id} ({percentage}) [(rskull){negative}|(skull){positive}|a{posAvailable}] [s{speed}]";

            __result = extraTooltip;
            // __result = extraTooltip + GJL.L("work_effeciency", percentage);
            return false;
        }
    }

    [HarmonyPatch(typeof(SavedWorkersList))]
    [HarmonyPatch("GetWorker")]
    internal class PatchSavedWorkersList
    {
        [HarmonyPostfix]
        static void PatchPostfix(SavedWorkersList __instance, ref long worker_unique_id, ref List<Worker> ____workers)
        {
            HelperWorker.SavedWorkers = ____workers;
        }
    }

    [HarmonyPatch(typeof(Worker))]
    [HarmonyPatch("UpdateWorkerSkin")]
    [HarmonyPatch(new Type[]{typeof(Worker.WorkerActivity)})]
    internal class PatchWorkerSkin
    {
        [HarmonyPrefix]
        public static void Prefix(Worker __instance, ref Worker.WorkerActivity worker_activity)
        {
            switch (worker_activity)
            {
                case Worker.WorkerActivity.None:
                    HelperWorker.ZombieSaid(new List<string>()
                    {
                        "zZzZz..",
                    }, __instance.worker_wgo, true, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
                    break;
                case Worker.WorkerActivity.Worker:
                    HelperWorker.ZombieSaid(new List<string>()
                    {
                        "I work fo breens...",
                    }, __instance.worker_wgo, true, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
                    break;
                case Worker.WorkerActivity.Porter:
                    HelperWorker.ZombieSaid(new List<string>()
                    {
                        "I zransport fo voss",
                    }, __instance.worker_wgo, true, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
                    break;
            }
        }
    }
    
    [HarmonyPatch(typeof(PorterStation))]
    [HarmonyPatch("OnCameToDestination")]
    // [HarmonyPatch(new Type[]{typeof(WorldGameObject)})]
    internal class PatchOnCameToDestination
    {
        [HarmonyPostfix]
        public static void PatchOnCameToDestinationPostfix(PorterStation __instance, ref WorldGameObject ____wgo)
        {
            HelperWorker.ZombieSaid(
                ____wgo.linked_worker.unique_id.ToString(),
                HelperWorker.ZPorterEndDialogue, 
                ____wgo.linked_worker, 
                __instance.HasLinkedWorker()
                , Entry.Config.Zombie_Dialogue_Chances, 
                Entry.Config.Zombie_Dialogue_WaitSec);
            // HelperWorker.ZombieSaid("test", ____wgo.linked_worker_unique_id);
            ____wgo.linked_worker.components.character.SetSpeed(Entry.Config.Zombie_MovementSpeed);
        }
    }
    
    [HarmonyPatch(typeof(PorterStation))]
    [HarmonyPatch("TrySendPorter")]
    // [HarmonyPatch(new Type[]{typeof(WorldGameObject)})]
    internal class PatchTrySendPorter
    {
        [HarmonyPostfix]
        public static void PatchTrySendPorterPostfix(PorterStation __instance, ref WorldGameObject ____wgo)
        {
            if(__instance.state == PorterStation.PorterState.Waiting || __instance.state == PorterStation.PorterState.None) 
                HelperWorker.ZombieSaid(____wgo.linked_worker.unique_id.ToString(),
                    HelperWorker.ZPorterStartDialogue, ____wgo.linked_worker, 
                    __instance.HasLinkedWorker(), 
                    Entry.Config.Zombie_Dialogue_Chances, 
                    Entry.Config.Zombie_Dialogue_WaitSec);
            
            ____wgo.linked_worker.components.character.SetSpeed(Entry.Config.Zombie_MovementSpeed);
        }    
    }
    
    [HarmonyPatch(typeof(PorterStation))]
    [HarmonyPatch("Update")]
    // [HarmonyPatch(new Type[]{typeof(WorldGameObject)})]
    internal class PatchUpdate
    {
        [HarmonyPostfix]
        public static void PatchUpdate_Postfix(PorterStation __instance, ref WorldGameObject ____wgo, ref WorldZone ____destination, ref WorldZone ____source)
        {
            // if (!__instance.is_correctly_inited || __instance.state == PorterStation.PorterState.None ||
            //     (!__instance.has_linked_worker || __instance.state != PorterStation.PorterState.Waiting))
            //     return;
            if (__instance.has_linked_worker 
                && __instance.is_correctly_inited 
                && (__instance.state == PorterStation.PorterState.GoingToDestination 
                    || __instance.state == PorterStation.PorterState.GoingToSource))
            {
                var zombId = ____wgo.linked_worker?.unique_id.ToString();
                    HelperWorker.ZombieSaid($"{zombId}", new List<string>()
                    {
                        // $"Vroom to {string.Join(" ",____destination?.gameObject?.name.Split('_'))}",
                        // $"Vroom to {string.Join(" ",____destination?.id?.Split('_'))}",
                        "Vroom vroom zel go!...",
                        "'Dear girls, We like you for your brains, not your body. Sincerely, Zombies.'",
                        "'A mind is a terrible thing to waste'",
                        "Chop~ Chop~ Chop~",
                        "Me eet breens, ye ar safe.",
                        "Vroom.. zel to go mang!...",
                        // $"Vroom to {string.Join(" ",____destination?.name?.Split('_'))}",
                    }, ____wgo?.linked_worker, 
                        __instance.state == PorterStation.PorterState.GoingToDestination, 
                        Entry.Config.Zombie_Dialogue_Chances, 
                        Entry.Config.Zombie_Dialogue_WaitSec);
                    HelperWorker.ZombieSaid($"{zombId}", new List<string>()
                    {
                        // $"I vroom {string.Join(" ",____destination?.gameObject?.name.Split('_'))}",
                        // $"Vroom to {string.Join(" ",____source?.id?.Split('_'))}",
                        "Vroom vroom.. zomb wan sleep...",
                        "'Dear girls, We like you for your brains, not your body. Sincerely, Zombies.'",
                        "'A mind is a terrible thing to waste'",
                        "Chop~ Chop~ Chop~",
                        "Me eet breens, ye ar safe.",
                        "Vroom.. zel to go mang!...",
                        "Vroom.. zomb need sleep mang...",
                        // $"Zooin to {string.Join(" ",____source?.name?.Split('_'))}",
                    }, ____wgo?.linked_worker, 
                        __instance.state == PorterStation.PorterState.GoingToSource, 
                        Entry.Config.Zombie_Dialogue_Chances, 
                        Entry.Config.Zombie_Dialogue_WaitSec);
            }

            // ____wgo.linked_worker.components.character.SetSpeed(Entry.Config.Zombie_MovementSpeed);
        }
    }

    [HarmonyPatch(typeof(Worker))]
    [HarmonyPatch("UpdateWorkerLevel")]
    internal class PatchWorker
    {
        // [HarmonyPrefix]
        // public static void PatchWorkerLevelPrefix(Worker __instance)
        // {
        //     var paramData = __instance.worker_wgo.data;
        //     if (Entry.Config.zombieDebug)
        //     {
        //         Entry.Log("=========[Prefix 1]");
        //         Entry.Log("Z_ID:" + __instance.worker_unique_id + "");
        //         Entry.Log("Efficiency:" + paramData.GetParam("working_k") + ",");
        //     }
        //     paramData.SetParam("working_k", paramData.GetParam("working_k") + (float)Entry.Config.baseZombieEfficiency);
        //     if (Entry.Config.zombieDebug)
        //     {
        //         Entry.Log("=========[Prefix 2]");
        //         Entry.Log("Z_ID:" + __instance.worker_unique_id + "");
        //         Entry.Log("Efficiency:" + paramData.GetParam("working_k") + ",");
        //     }
        // }

        public static void writeLog(Worker __instance, string title)
        {
            if (!Entry.Config.Debug_Enabled) return;
            int positive;
            var currentEfficiency = __instance.worker_wgo.data.GetParam("working_k");
            var currentSpeed = __instance.worker_wgo.data.GetParam("speed");
            __instance.worker_wgo.data.GetBodySkulls(out int negative, out positive, out int posAvailable, true);
            Entry.Log("=========[]> " + title);
                Entry.Log("Z_ID:" + __instance.worker_unique_id + "");
                Entry.Log("Z_Backpack:" + __instance.GetBackpack().GetItemName() + "");
                Entry.Log("Is_worker:" + __instance.worker_wgo.data.is_worker + "");
                Entry.Log("Efficiency:" + currentEfficiency + ",");
                Entry.Log("Negative:" + negative + ",");
                Entry.Log("Positive:" + positive + ",");
                Entry.Log("Available:" + posAvailable + ",");
                Entry.Log("Speed:" + currentSpeed + ",");
                // Entry.Log("Available:" + __instance.worker_wgo.GetParam() + ",");
        } 
        
        [HarmonyPostfix]
        public static void PatchWorkerLevelPostfix(Worker __instance)
        {
            HelperWorker.SetWorkerSpeed(__instance);
            HelperWorker.SetWorkerEfficiency(__instance);
            // var paramData = __instance.worker_wgo.data;
            // int positive;
            // __instance.worker_wgo.data.GetBodySkulls(out int _, out positive, out int _, true);
            // var patchedBaseValue = (float) Entry.Config.Zombie_BaseEfficiency;
            // var patchedMaxValue = (patchedBaseValue * 100) / (float) Entry.Config.Zombie_MaxEfficiency;
            // var patchedExtraValue = positive + (float)Entry.Config.Zombie_ExtraEfficiency;
            // paramData.SetParam("working_k", patchedExtraValue / patchedMaxValue);
            // paramData.SetParam("speed", Entry.Config.Zombie_MovementSpeed);
            // if (Input.GetKey(KeyCode.P))
            // {
            //     GUIElements.me.dialog.OpenOK($@"
            //         UniqID: {__instance.worker_unique_id.ToString()}|
            //         speed: {paramData.GetParam("speed")} |
            //         params: {paramData.GetParams().ToFormattedString()} |
            //     ");
            // }
            // writeLog(__instance, "Postfix After");
        }
    }
}