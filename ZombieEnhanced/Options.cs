// Decompiled with JetBrains decompiler
// Type: MoreStorage.Options
// Assembly: MoreStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8143B4D2-383B-420B-9E1A-B5DCD285777A
// Assembly location: V:\Games\Graveyard.Keeper.v1.310\Graveyard.Keeper.v1.310\QMods\MoreStorage\MoreStorage.dll

using System.Collections.Generic;
using UnityEngine;

namespace ZombieEnhanced
{
    public class Options
    {
        public float Zombie_MovementSpeed { get; set; } = 2.55f;
        public float Zombie_BaseEfficiency { get; set; } = 16f;
        public float Zombie_ExtraEfficiency { get; set; } = 0f;
        public float Zombie_MaxEfficiency { get; set; } = 100f;

        public bool Prayer_Reset_Enabled { get; set; } = true;
        public string Prayer_Key { get; set; } = "p";
        public bool Buffs_Activator_Enabled { get; set; } = true;
        public string Buffs_Activator_Key { get; set; } = "o";
        public string Buffs_Activator_Remover_Key { get; set; } = null;
        public string Buffs_Activator_Selected { get; set; } = "buff_skull";
        public string Buffs_Activator_Title { get; set; } = "Difficult corpses";
        public bool Debug_Enabled { get; set; } = false;
        public string Debug_Log_PropName { get; set; } = "Buffs";
        public string InGame_ReloadConfig_Key { get; set; } = "l";
        public bool InGame_ReloadConfig_Rerun { get; set; } = true;
        

        public string Item_Spawner_Id { get; set; } = "alchemy_2_d_green";
        public int Item_Spawner_Qty { get; set; } = 1;
        public string Item_Spawner_Key { get; set; } = "j";
        public string Item_Spawner_Package_Key { get; set; } = "b";
        public bool Item_Organs_Overhaul_Enabled { get; set; } = true;
        public int Item_Organs_Overhaul_StackCount { get; set; } = 10;
        public int Item_Organs_Additional_Value { get; set; } = 1;
        public bool Item_Embalm_Overhaul_Enabled { get; set; } = true;
        public int Item_Embalm_Additional_Value { get; set; } = 1;

        public bool Craft_ZombieFarm_ProduceWaste_Enabled { get; set; } = true;
        public int Craft_ZombieFarm_ProduceWaste_Min { get; set; } = 0;
        public int Craft_ZombieFarm_ProduceWaste_Max { get; set; } = 8;
        public bool Craft_ZombieFarm_SeedsNeed_Enabled { get; set; } = true;
        public int Craft_ZombieFarm_Garden_Needs_Value { get; set; } = 22;
        public int Craft_ZombieFarm_Vineyard_Needs_Value { get; set; } = 11;
        public float Craft_ZombieFarm_ProduceWaste_Chance { get; set; } = 0.7f;
        public bool Drops_AutoStore_Enabled { get; set; } = true;
        public string[] Drops_AutoStore_Items_Item { get; set; } = {"wood", "stone", "marble", "ore_metal"};
        public string[] Drops_AutoStore_Items_Places { get; set; } = {"mf_timber_1", "mf_stones_1", "mf_ore_1_complete"};
        public bool Drops_StoreThenCleaned_OnReload { get; set; } = false;
        public bool Zombie_Dialogue_Enabled { get; set; } = true;
        public float Zombie_Dialogue_Chances { get; set; } = 25f;
        public float Zombie_Dialogue_WaitSec { get; set; } = 3f;
        public ZEVoiceID Zombie_Dialogue_Voice { get; set; } = ZEVoiceID.Gunter;
        public bool Tooltip_Enabled { get; set; } = true;
        public Mode Config_Preset { get; set; } = Mode.Balance;
        
    }
    
    public enum ZEVoiceID
    {
        None = 0,
        Skull = 1,
        Bishop = 2,
        Inquisitor = 3,
        Actress = 4,
        Merchant = 5,
        Cultist = 6,
        Astrologer = 7,
        Guard = 8,
        Donkey = 9,
        CellPhone = 10, // 0x0000000A
        TavernKeeper = 11, // 0x0000000B
        Blacksmith = 12, // 0x0000000C
        Actor = 13, // 0x0000000D
        RedEye = 14, // 0x0000000E
        Dig = 15, // 0x0000000F
        Carpenter = 16, // 0x00000010
        FarmersSon = 17, // 0x00000011
        FarmersDaughter = 18, // 0x00000012
        Engineer = 19, // 0x00000013
        Capitan = 20, // 0x00000014
        Witch = 21, // 0x00000015
        LightKeeper = 22, // 0x00000016
        Miller = 23, // 0x00000017
        Farmer = 24, // 0x00000018
        WoodCutter = 25, // 0x00000019
        Ghost = 26, // 0x0000001A
        Zombie = 27, // 0x0000001B
        LordCommander = 28, // 0x0000001C
        Satyr = 29, // 0x0000001D
        Gypsy = 30, // 0x0000001E
        MsChain = 31, // 0x0000001F
        Gunter = 32, // 0x00000020
        Bella = 33, // 0x00000021
        Jove = 34, // 0x00000022
        Lucius = 35, // 0x00000023
        Teacher = 36, // 0x00000024
        WitchYoung = 37, // 0x00000025
        MasterAlarich = 38, // 0x00000026
        Beatris = 39, // 0x00000027
        MarquisTeodoroJr = 40, // 0x00000028
        Hunchback = 41, // 0x00000029
        GhostPriest = 42, // 0x0000002A
        WomanBlackGold = 43, // 0x0000002B
        Shepherd = 44, // 0x0000002C
        VampireCommon = 45, // 0x0000002D
        RefugeeCoffinMaker = 46, // 0x0000002E
        RefugeeCook = 47, // 0x0000002F
        RefugeeTanner = 48, // 0x00000030
        MarquisTeodoroJrAfflicted = 49, // 0x00000031
        RefugeeMoneylender = 50, // 0x00000032
        RefugeeMan = 51, // 0x00000033
        ShepherdsWife = 52, // 0x00000034
        VampireCarl = 53, // 0x00000035
        Potter = 54, // 0x00000036
        BeeKeeper = 55, // 0x00000037
        Player = 100, // 0x00000064
        Unused = 101, // 0x00000065
        Unused2 = 102, // 0x00000066
        Unused3 = 103, // 0x00000067
        Unused4 = 104, // 0x00000068
        Unused5 = 105, // 0x00000069
    }

    public enum Mode
    {
        Default = 0,
        Custom = 1,
        Balance = 2,
        BalancePlus = 3,
        Super = 4,
    }
}