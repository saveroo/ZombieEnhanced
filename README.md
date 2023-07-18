# [Not Just] Zombie Enhanced - 2.2.0

![Image Description](/LastUpdatedForm.png)

Nexus Link to this mod: [Not Just Zombie Enhanced - 2.2.0](https://www.nexusmods.com/graveyardkeeper/mods/edit/?id=24&game_id=2696&step=details)


**2.2.0: Zombie's Dialogue**

GUI Configurable Zombie/Worker Walk/Carry Speed, Efficiency, with several extra Features.

**:: REQUIREMENTS ::**

- QModManager
- Replace with updated [Harmony 1.2](https://github.com/pardeike/Harmony/releases/tag/v1.2.0.1)﻿ (**MANDATORY**)


> Important **Notes!:**
- Install **QModManager**first.
- Then download [Harmony 1.2](https://github.com/pardeike/Harmony/releases/tag/v1.2.0.1)
- After that, open folder *net472*under *Release.zip*
- Then put **0Harmony.dll**(*_112kb_*) in (X:\[YourFolder]*\Graveyard.Keeper\Graveyard Keeper_Data\Managed\**0Harmony.dll**)*
- Replacing **QModManager**0harmony.dll wont break other mods that needs QModManager, it just an upgrade with feature addition,
- There's used feature used by this mod within Harmony 1.2


(**BUG**) Extracted Organs can't be picked up workaround
> The bug was caused by Item_Organs_Overhaul_StackCount (**Max Stack**)** **config, it should be fine if its stay at **1**.
Do it **before EXTRACTION**in **Autopsy**table**:**
1. set **Max Stack** to **1**, 
2. **press L (default hotkey)** in game to **reload **config,
3. Extract the bodies
4. set **Max Stack **to your desired value again
   5. **press L (default hotkey) **in game to **reload **config.
6. After that you can stack the **organs** again in **Mortuary Rack**or any **chest/inventory**




**:: FEATURES ::

- [Zombie]'s Talks
- [Zombie] Farm/Vineyard Seeds Reducer
- [Zombie] Farm/Vineyard Produce Crop Waste!
- [Zombie] Speed, Zombie Porter Speed
- [Zombie] Efficiency, Efficiency Multiplier
- [Tools] GUI Configurable, Change config with ease
- [Tools] GUI Config multi Presets!
- [Tools] In Game Reloader, Press desired key to reflect config change.
- [Spawner] Item Spawner
- [Spawner] Best Organs Package Dropper (Tune up your Zombees!)
- [Embalment] Gold Embalment Improvement
- [Tooltip] Extra Zombie Tooltips, Autopsy/Near
- [Tooltip] Organ Tooltips!
- [Buff] At Will!, Most Buffs can be activated at will
- [Sermon] At Will!, Preach Everytime!

**
**:: CONFIG NOTES ::**

- Floating number should use commas instead of point
- Currently XX_Key are limited to 1 keyboard key



**:: CONFLICTS ::**

- I don't know, if you find one please let me know.
- I only use "Keeper's need", "more storage.", "Build anywhere"


**:: CONFIG OVERVIEW [Not Updated] ::**

- **Zombie_MovementSpeed**, default = 1,5,
- **Zombie_BaseEfficiency**, default = 16, according to max skull., shouldn't get edited, due to confusion.
- **Zombie_ExtraEfficiency**, extra efficiency added to calculation.
- **Zombie_MaxEfficiency**, In %, maximal efficiency, default game value = 40%, it's better to edit/increase this to have balanced/natural feeling,
- **Prayer_Reset_Enabled**, Turn On/Off Feature, by setting to false/true
- **Prayer_Key**, True/False, Key to activate in game. (In-Game dialog will show up to confirm)
- **Buffs_Activator_Enabled, **True/False, Turn On/Off Feature, by setting to false/true
- **Buffs_Activator_Key**, default = O, Key to activate in game, (In-Game dialog will show up to confirm)
- **InGame_ReloadConfig_Key**, default = L, press in-game to reload config change


**::  KNOWN ISSUES ::**

- For those encounter an error, it need [Harmony 1.2](https://github.com/pardeike/Harmony/releases/tag/v1.2.0.1)
- Messed up Vine Press Craft Menu, download the HOTFIX 2.0.1.0, Fixed 2.2.0
- Messed up Zombie farm menu within tier I/II, except 3, fixed 2.2.0


# License
This project is licensed under the Creative Commons Zero v1.0 Universal license (CC0 1.0).

For details, please refer to the [LICENSE](LICENSE) file.
