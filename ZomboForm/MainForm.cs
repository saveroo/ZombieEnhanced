using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using ZombieEnhanced;

namespace ZomboForm
{
    public partial class ZomboForm : Form
    {
        private Options Config; 
        public ZomboForm()
        {
            InitializeComponent();
            this.Text = $"(Balance) Zombie Enhanced Configuration {Application.ProductVersion}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ZombieEnhanced.Entry.Config.Buffs_Corpse_Enabled = !ZombieEnhanced.Entry.Config.Buffs_Corpse_Enabled;
        }
        
        private void PrependToTitle(string str)
        {
            this.Text = $"({str}) Zombie Enhanced Configuration {Application.ProductVersion}";
        }
    
        private Options ModeChanger(Options config)
        {
            switch (config.Config_Preset)
            {
                case Mode.Default:
                    config.Debug_Enabled = false;
                    config.Zombie_MovementSpeed = 1.5f;
                    config.Zombie_BaseEfficiency = 16f;
                    config.Zombie_ExtraEfficiency = 0;
                    config.Zombie_MaxEfficiency = 40;
                    config.InGame_ReloadConfig_Rerun = true;
                    config.Item_Spawner_Key = "j";
                    config.Item_Spawner_Qty = 1;
                    config.Item_Spawner_Package_Key = "b";
                    config.Item_Organs_Overhaul_Enabled = false;
                    config.Item_Organs_Overhaul_StackCount = 1;
                    config.Item_Organs_Additional_Value = 0;
                    config.Item_Embalm_Overhaul_Enabled = false;
                    config.Item_Embalm_Additional_Value = 0;
                    config.Craft_ZombieFarm_ProduceWaste_Enabled = false;
                    config.Craft_ZombieFarm_ProduceWaste_Max = 0;
                    config.Craft_ZombieFarm_ProduceWaste_Min = 0;
                    config.Craft_ZombieFarm_SeedsNeed_Enabled = false;
                    config.Craft_ZombieFarm_Garden_Needs_Value = 24;
                    this.Enable(false);
                    // btnSave.Enabled = true;
                    // comboPreset.Enabled = true;
                    PrependToTitle("Default");
                    // linkLbl.Enabled = true;
                    // lblInfo.Enabled = true;
                    // cboxItemSpawner.Enabled = true;
                    // buffComboBox.Enabled = true;
                    // tboxBuffKey.Enabled = true;
                    // tboxPrayerKey.Enabled = true;
                    // tboxDropOrganPackageKey.Enabled = true;
                    // tboxReloadKey.Enabled = true;
                    // tboxItemSpawnerKey.Enabled = true;
                    // tboxItemSpawnerQty.Enabled = true;
                    break;
                case Mode.Custom:
                    this.Enable(true);
                    PrependToTitle("Custom");
                    break;
                case Mode.Balance:
                    config.Debug_Enabled = false;
                    config.Zombie_MovementSpeed = 2.5f;
                    config.Zombie_BaseEfficiency = 16f;
                    config.Zombie_ExtraEfficiency = 0;
                    config.Zombie_MaxEfficiency = 90;
                    config.InGame_ReloadConfig_Rerun = true;
                    config.Item_Spawner_Key = "j";
                    config.Item_Spawner_Qty = 1;
                    config.Item_Spawner_Package_Key = "b";
                    config.Item_Organs_Overhaul_Enabled = true;
                    config.Item_Organs_Overhaul_StackCount = 3;
                    config.Item_Organs_Additional_Value = 1;
                    config.Item_Embalm_Overhaul_Enabled = true;
                    config.Item_Embalm_Additional_Value = 1;
                    config.Craft_ZombieFarm_ProduceWaste_Enabled = true;
                    config.Craft_ZombieFarm_ProduceWaste_Max = 5;
                    config.Craft_ZombieFarm_ProduceWaste_Min = 0;
                    config.Craft_ZombieFarm_SeedsNeed_Enabled = false;
                    config.Craft_ZombieFarm_Garden_Needs_Value = 23;
                    this.Enable(false);
                    // btnSave.Enabled = true;
                    // comboPreset.Enabled = true;
                    PrependToTitle("Balance");
                    // linkLbl.Enabled = true;
                    // lblInfo.Enabled = true;
                    // cboxItemSpawner.Enabled = true;
                    // buffComboBox.Enabled = true;
                    // tboxBuffKey.Enabled = true;
                    // tboxPrayerKey.Enabled = true;
                    // tboxDropOrganPackageKey.Enabled = true;
                    // tboxReloadKey.Enabled = true;
                    // tboxItemSpawnerKey.Enabled = true;
                    // tboxItemSpawnerQty.Enabled = true;
                    break;
                case Mode.BalancePlus:
                    config.Debug_Enabled = false;
                    config.Zombie_MovementSpeed = 3.2f;
                    config.Zombie_BaseEfficiency = 16f;
                    config.Zombie_ExtraEfficiency = 0;
                    config.Zombie_MaxEfficiency = 100;
                    config.InGame_ReloadConfig_Rerun = false;
                    config.Item_Spawner_Key = "j";
                    config.Item_Spawner_Qty = 1;
                    config.Item_Spawner_Package_Key = "b";
                    config.Item_Organs_Overhaul_Enabled = true;
                    config.Item_Organs_Overhaul_StackCount = 1;
                    config.Item_Organs_Additional_Value = 1;
                    config.Item_Embalm_Overhaul_Enabled = true;
                    config.Item_Embalm_Additional_Value = 2;
                    config.Craft_ZombieFarm_ProduceWaste_Enabled = true;
                    config.Craft_ZombieFarm_ProduceWaste_Max = 7;
                    config.Craft_ZombieFarm_ProduceWaste_Min = 0;
                    config.Craft_ZombieFarm_SeedsNeed_Enabled = true;
                    config.Craft_ZombieFarm_Garden_Needs_Value = 22;
                    this.Enable(false);
                    // btnSave.Enabled = true;
                    // comboPreset.Enabled = true;
                    PrependToTitle("Balance+");
                    // linkLbl.Enabled = true;
                    // lblInfo.Enabled = true;
                    // cboxItemSpawner.Enabled = true;
                    // buffComboBox.Enabled = true;
                    // tboxBuffKey.Enabled = true;
                    // tboxPrayerKey.Enabled = true;
                    // tboxDropOrganPackageKey.Enabled = true;
                    // tboxReloadKey.Enabled = true;
                    // tboxItemSpawnerKey.Enabled = true;
                    // tboxItemSpawnerQty.Enabled = true;
                    break;
                case Mode.Super:
                    config.Debug_Enabled = false;
                    config.Zombie_MovementSpeed = 1000f;
                    config.Zombie_BaseEfficiency = 16f;
                    config.Zombie_ExtraEfficiency = 0;
                    config.Zombie_MaxEfficiency = 1000f;
                    config.InGame_ReloadConfig_Rerun = false;
                    config.Item_Spawner_Key = "j";
                    config.Item_Spawner_Qty = 1;
                    config.Item_Spawner_Package_Key = "b";
                    config.Item_Organs_Overhaul_Enabled = false;
                    config.Item_Organs_Overhaul_StackCount = 20;
                    config.Item_Organs_Additional_Value = 5;
                    config.Item_Embalm_Overhaul_Enabled = false;
                    config.Item_Embalm_Additional_Value = 5;
                    config.Craft_ZombieFarm_ProduceWaste_Enabled = false;
                    config.Craft_ZombieFarm_ProduceWaste_Max = 100;
                    config.Craft_ZombieFarm_ProduceWaste_Min = 0;
                    config.Craft_ZombieFarm_SeedsNeed_Enabled = false;
                    config.Craft_ZombieFarm_Garden_Needs_Value = 1;
                    this.Enable(false);
                    // btnSave.Enabled = true;
                    // comboPreset.Enabled = true;
                    PrependToTitle("Super");
                    // linkLbl.Enabled = true;
                    // lblInfo.Enabled = true;
                    // cboxItemSpawner.Enabled = true;
                    // buffComboBox.Enabled = true;
                    // tboxBuffKey.Enabled = true;
                    // tboxPrayerKey.Enabled = true;
                    // tboxDropOrganPackageKey.Enabled = true;
                    // tboxReloadKey.Enabled = true;
                    // tboxItemSpawnerKey.Enabled = true;
                    // tboxItemSpawnerQty.Enabled = true;
                    break;
            }
            
            Enabled = true;
            
            btnSave.Enable(true);
            comboPreset.Enable(true);
            linkLbl.Enable(true);
            label22.Enable(true);
            lblConfigKey.Enable(true);
            lblInfo.Enable(true);
            cboxItemSpawner.Enable(true);
            buffComboBox.Enable(true);
            tboxBuffKey.Enable(true);
            tboxPrayerKey.Enable(true);
            tboxDropOrganPackageKey.Enable(true);
            tboxReloadKey.Enable(true);
            tboxItemSpawnerKey.Enable(true);
            numItemSpawnerQty.Enable(true);
    
            numZombieDialogueChance.Enable(true);
            numZombieDialogueWait.Enable(true);
            comboZombieDialogueVoiceID.Enable(true);
            cbZombieDialogueEnabled.Enable(true);
            groupBox1.Enable(true);
            groupBox3.Enable(true);
            groupBox4.Enable(true);
    
            rtbBuffDescription.Enable(false);
            rtbItemSpawner.Enable(false);
            Entry.IniInstance.WriteJsonConfig(config);
            return config;
        }
        private void InitLabelConfigKey(Options opts)
        {
            lblConfigKey.Text = "";
            foreach (PropertyInfo property in opts.GetType().GetProperties())
            {
                // var dot = property.GetValue((object) objConfig).ToString().Replace(",", ".");
    
                if (property.Name.ToLower().Contains("key"))
                {
                    var txt = $@"{(object) property.Name}: {property.GetValue((object) opts)?.ToString().ToUpper()}";
                    txt += "\n";
                    lblConfigKey.Text += txt;
                }
                // text.WriteLine($"{(object) property.Name}={property.GetValue(dot)}");
            }
        }
    
        private void Redraw(Options cfg)
        {
            boxZombieMovementSpeed.Value = (decimal) cfg.Zombie_MovementSpeed;
            boxZombieBaseEff.Value = (decimal) cfg.Zombie_BaseEfficiency;
            boxZombieExtraEff.Value = (decimal) cfg.Zombie_ExtraEfficiency;
            boxZombieMaxEff.Value = (decimal) cfg.Zombie_MaxEfficiency;
            buffComboBox.SelectedItem = (decimal) cfg.Zombie_MaxEfficiency;
            cboxItemSpawner.SelectedItem = (decimal) cfg.Zombie_MaxEfficiency;
    
            cboxItemEmbalmOverhaul.Checked = cfg.Item_Embalm_Overhaul_Enabled;
            numEmbalmGoldPlus.Value = cfg.Item_Embalm_Additional_Value;
            
            cboxItemOrgansOverhaul.Checked = cfg.Item_Embalm_Overhaul_Enabled;
            numItemOrgansMaxStack.Value = cfg.Item_Organs_Overhaul_StackCount;
            numOrganPlus.Value = cfg.Item_Organs_Additional_Value;
            
            numZFProduceCropWaste.Value = cfg.Craft_ZombieFarm_ProduceWaste_Max;
            numZFProduceSeedsNeed.Value = cfg.Craft_ZombieFarm_Garden_Needs_Value;
            numZFVineyardSeedValue.Value = cfg.Craft_ZombieFarm_Vineyard_Needs_Value;
            cboxZFSeedsNeed.Checked = cfg.Craft_ZombieFarm_SeedsNeed_Enabled;
            cboxZFProduceCropWaste.Checked = cfg.Item_Embalm_Overhaul_Enabled;
    
            cbDebugLog.Checked = cfg.Debug_Enabled;
            cbInGameRerun.Checked = cfg.InGame_ReloadConfig_Rerun;
    
            cbBuffEnabled.Checked = cfg.Buffs_Activator_Enabled;
            
            tboxPrayerKey.Text = cfg.Prayer_Key;
            tboxBuffKey.Text = cfg.Buffs_Activator_Key;
            tboxReloadKey.Text = cfg.InGame_ReloadConfig_Key;
            tboxItemSpawnerKey.Text = cfg.Item_Spawner_Key;
            numItemSpawnerQty.Value = cfg.Item_Spawner_Qty;
            tboxDropOrganPackageKey.Text = cfg.Item_Spawner_Package_Key;
    
            numZombieDialogueChance.Value = (decimal) cfg.Zombie_Dialogue_Chances;
            numZombieDialogueWait.Value = (decimal) cfg.Zombie_Dialogue_WaitSec;
    
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Entry.LoadIniSettings();
            Config = IniConfig.Instance.ReadJsonConfig();
            ModeChanger(Config);
            MainLogic(Config);
    
            comboPreset.DataSource = Config.Config_Preset.GetType().GetEnumValues();
            comboPreset.SelectedItem = Config.Config_Preset;
    
            btnSave.Click += (o, args) =>
            {
                Config.Config_Preset =
                    comboPreset.SelectedItem is Mode ? (Mode) comboPreset.SelectedItem : Mode.Default;
                ModeChanger(Config);
                Config = IniConfig.Instance.ReadJsonConfig();
                Redraw(Config);
            };
        }
    
        private void MainLogic(Options config)
        {
            List<Buffs> buffs = new List<Buffs> { 
                new Buffs("buff_skull", 
                    "Difficult corpses", 
                    "Donkey brings corpses with more White Skull and Red Skull."),
                new Buffs("buff_plant", "[Pray] Fertility", "Vegetables grow faster."),
                new Buffs("buff_sins", "[Pray] Repentance", "More people come during the week to confess their sins."),
                new Buffs("buff_pen", "[Pray] Inspiration", "You have a better chance to write something of good quality."),
                new Buffs("buff_star", "[Pray] Handyman mood", "You have a better chance to craft something of good quality."),
                new Buffs("buff_sword", "[Pray] Rage", "You deal additional damage to monsters."),
                new Buffs("buff_shield", "Tough mood", "Incoming damage is reduced."),
                new Buffs("buff_garlic_poison", "Eaten garlic", null),
                new Buffs("buff_pot_damage", "[Pot] Rage potion", null),
                new Buffs("buff_pot_berserk_damage", "[Pot] Berserk Damage", null),
                new Buffs("buff_pot_berserk_poison", "[Pot] Berserk Poison", null),
                new Buffs("buff_pot_armor", "[Pot] Protection potion", null),
                new Buffs("buff_pot_heal_long", "[Pot] Restoring potion", "Restore your Health over time."),
                new Buffs("buff_pot_appetite", "[Pot] Digestion potion", "Food gives you more energy."),
                new Buffs("buff_pot_speed", "[Pot] Speed", "Increases your speed."),
                new Buffs("buff_tired", "Tired", null),
                new Buffs("buff_tired_tech", "Tired Tech", null),
                new Buffs("buff_fishing", "Fast reflexes", "You have additional time to hook a fish."),
                new Buffs("buff_fishing2", "Good fisherman", "You don't let fish off the hook easily."),
                new Buffs("buff_survay", "Circumspect", "You get extra points when studying new items."),
                new Buffs("buff_beer", "Beer thirst", "Beer tastes much better now!"),
                new Buffs("buff_hardwork", "Hard worker", "You spend less energy working with axes and pickaxes."),
                new Buffs("buff_longtimer", "Slow metabolism", "All buffs last longer."),
                new Buffs("buff_sleep", "Deep sleep", "Energy regeneration while sleeping is increased."),
                new Buffs("buff_cleancut", "Steady hand", "A steady hand reduces the chances of making a surgical error."),
                new Buffs("buff_pen_food", "[Food] Inspiration", null),
                new Buffs("buff_star_food", "[Food] Handyman mood", null),
                new Buffs("buff_sword_food", "[Food] Rage", null),
                new Buffs("buff_shield_food", "[Food] Tough mood", "Incoming damage is reduced."),
                new Buffs("buff_dlc_refugee_persistence", "[DLC] Refugee Persistence", null)
            };
    
            List<ItemSpawner> itemSpawners = ItemSpawner.Instance.InitItemSpawner();
            // using (StreamReader file = File.OpenText(@"Items.json"))
            // {
            //     itemSpawners = JsonConvert.DeserializeObject<List<ItemSpawner>>(file.ReadToEnd());
            // }
    
            
            var dotToCommas = new Func<float, string>((float flt) =>
            {
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                return flt.ToString().Replace(".", ",");
            });
            
            // Create Binding Source
            var bs = new BindingSource();
            bs.DataSource = buffs;
            buffComboBox.DataSource = bs.DataSource;
            buffComboBox.DisplayMember = "title";
            buffComboBox.ValueMember = "id";
            
            // Item Binding Source
            var itemSource = new BindingSource();
            itemSource.DataSource = itemSpawners;
            cboxItemSpawner.DataSource = itemSource;
            cboxItemSpawner.DisplayMember = "Name";
            cboxItemSpawner.ValueMember = "Id";
            
            // Deserialize Ini
            // ZombieEnhanced.Options option =
            //     ZombieEnhanced
            //         .IniConfig
            //         .Instance
            //         .ReadJsonConfig();
            // Config = option;
            //
            // // Local Var for ease
            // var config = Config;
            var save = new Action<ZombieEnhanced.Options>((cfg) =>
            {
                InitLabelConfigKey(config);
                ZombieEnhanced.IniConfig.Instance.WriteJsonConfig(cfg);
            });
            
            // InitLabelInfo
    
    
            List<UIControl> ctrlList = new List<UIControl>();
            ctrlList.Add(new UIControl(nameof(config.Zombie_MovementSpeed), boxZombieMovementSpeed, label1));
            ctrlList.Add(new UIControl(nameof(config.Zombie_BaseEfficiency), boxZombieBaseEff, label2));
            ctrlList.Add(new UIControl(nameof(config.Zombie_ExtraEfficiency), boxZombieExtraEff, label3));
            ctrlList.Add(new UIControl(nameof(config.Zombie_MaxEfficiency), boxZombieMaxEff, label4));
            // d.Add(boxZombieBaseEff, option.Zombie_BaseEfficiency);
            // d.Add(boxZombieExtraEff, option.Zombie_ExtraEfficiency);
            // d.Add(boxZombieMaxEff, option.Zombie_MaxEfficiency);
            
            
            ctrlList.ForEach(pair =>
            {
                PropertyInfo props = typeof(Options).GetProperty((string) pair.propsName);
                pair.ctrLabel.Text = pair.propsName.ToString();
                pair.ctrBox.Value = 0;
                if (props != null)
                {
                    Console.WriteLine(config);
                    var val = dotToCommas((float) props.GetValue(config));
                    // var val = (float) props.GetValue(config);
                    pair.ctrBox.Value = Convert.ToDecimal(val);
                    pair.ctrBox.Increment = 0.1M;
                    pair.ctrBox.ValueChanged += (o, args) =>
                    {
                        NumericUpDown oo = o as NumericUpDown;
                        var toSingle = Convert.ToSingle(oo.Value);
                        props.SetValue(config, toSingle);
                        save(config);
                    };
                }
            });
    
            // Buffs
            var buffExist = buffs.FirstOrDefault(k => k.Id == config.Buffs_Activator_Selected);
            if (buffExist != null)
            {
                buffComboBox.SelectedItem = buffExist;
                rtbBuffDescription.Text = buffExist.Desc;
            }
            buffComboBox.SelectedIndexChanged += (o, args) =>
            {
                ComboBox cb = o as ComboBox;
                config.Buffs_Activator_Selected = cb.SelectedValue.ToString();
                config.Buffs_Activator_Title = (cb.SelectedItem as Buffs).Title;
                rtbBuffDescription.Text = (cb.SelectedItem as Buffs).Desc;
                save(config);
            };
            
            // Buffs
            cbBuffEnabled.Checked = config.Buffs_Activator_Enabled;
            cbBuffEnabled.CheckedChanged += (o, args) =>
            {
                config.Buffs_Activator_Enabled = ((CheckBox) o).Checked;
                save(config);
            };
            
            // Zombie
            cbZombieDialogueEnabled.Checked = config.Zombie_Dialogue_Enabled;
            cbZombieDialogueEnabled.CheckedChanged += (o, args) =>
            {
                config.Zombie_Dialogue_Enabled = ((CheckBox) o).Checked;
                save(config);
            };
            
            // ItemSpawner
            var itemExist = itemSpawners.FirstOrDefault((k) => k.Id == config.Item_Spawner_Id);
            if (itemExist != null)
            {
                cboxItemSpawner.SelectedItem = itemExist;
                rtbItemSpawner.Text = itemExist.Description;
            }
            numItemSpawnerQty.Value = config.Item_Spawner_Qty;
            cboxItemSpawner.SelectedIndexChanged += (o, args) =>
            {
                ComboBox cb = o as ComboBox;
                config.Item_Spawner_Id = cb.SelectedValue.ToString();
                config.Item_Spawner_Qty = (int) numItemSpawnerQty.Value;
                groupBox3.Text = $"Item Spawner ({config.Item_Spawner_Id})";
                rtbItemSpawner.Text = (cb.SelectedItem as ItemSpawner).Description;
                save(config);
            };
            
            //Voice
            comboZombieDialogueVoiceID.DataSource = Config.Zombie_Dialogue_Voice.GetType().GetEnumValues();
            comboZombieDialogueVoiceID.SelectedItem = Config.Zombie_Dialogue_Voice;
            
            comboZombieDialogueVoiceID.SelectedIndexChanged += (o, args) =>
            {
                var oo = o as ComboBox;
                config.Zombie_Dialogue_Voice = (ZEVoiceID) oo.SelectedItem;
                save(config);
            };
            
            // Voice chance
            numZombieDialogueChance.Value = (decimal) config.Zombie_Dialogue_Chances;
            numZombieDialogueChance.ValueChanged += (sender, args) =>
            {
                var obj = sender as NumericUpDown;
                config.Zombie_Dialogue_Chances = (float) obj.Value;
                save(config);
            };
            
            numZombieDialogueWait.Value = (decimal) config.Zombie_Dialogue_WaitSec;
            numZombieDialogueWait.ValueChanged += (sender, args) =>
            {
                var obj = sender as NumericUpDown;
                config.Zombie_Dialogue_WaitSec = (float) obj.Value;
                save(config);
            };
            
            
            // Organs
            numItemOrgansMaxStack.Value = config.Item_Organs_Overhaul_StackCount;
            numItemOrgansMaxStack.ValueChanged += (o, args) =>
            {
                config.Item_Organs_Overhaul_StackCount = (int) ((NumericUpDown) o).Value;
                save(config);
            };
            numOrganPlus.Value = config.Item_Organs_Additional_Value;
            numOrganPlus.ValueChanged += (o, args) =>
            {
                config.Item_Organs_Additional_Value = (int) ((NumericUpDown) o).Value;
                save(config);
            };
            
            // Embalm
            numEmbalmGoldPlus.Value = config.Item_Embalm_Additional_Value;
            numEmbalmGoldPlus.ValueChanged += (o, args) =>
            {
                config.Item_Embalm_Additional_Value = (int) ((NumericUpDown) o).Value;
                save(config);
            };
            // ZombieFarm
            numZFProduceCropWaste.Value = config.Craft_ZombieFarm_ProduceWaste_Max;
            numZFProduceCropWaste.ValueChanged += (o, args) =>
            {
                config.Craft_ZombieFarm_ProduceWaste_Max = (int) ((NumericUpDown) o).Value;
                save(config);
            };
            numZFProduceSeedsNeed.Value = config.Craft_ZombieFarm_Garden_Needs_Value;
            numZFProduceSeedsNeed.ValueChanged += (o, args) =>
            {
                config.Craft_ZombieFarm_Garden_Needs_Value = (int) ((NumericUpDown) o).Value;
                save(config);
            };
            numZFVineyardSeedValue.Value = config.Craft_ZombieFarm_Vineyard_Needs_Value;
            numZFVineyardSeedValue.ValueChanged += (o, args) =>
            {
                config.Craft_ZombieFarm_Vineyard_Needs_Value = (int) ((NumericUpDown) o).Value;
                save(config);
            };
            
            var cbSaver = new Action<
                CheckBox,
                string>(
                (t, cfg) =>
                {
                    PropertyInfo prop = null;
                    foreach (var info in config.GetType()
                        .GetProperties()
                        .Where(f => { return f.Name == cfg; }))
                    {
                        prop = info;
                        break;
                    }
    
                    t.Checked = prop.GetValue(config) is bool ? (bool) prop.GetValue(config) : false;
                    t.CheckedChanged += (o, args) =>
                    {
                        prop.SetValue(config, (o as CheckBox).Checked);
                        save(config);
                    };
                });
            cbSaver(cbPrayerResetEnabled, nameof(config.Prayer_Reset_Enabled));
            cbSaver(cboxItemOrgansOverhaul, nameof(config.Item_Organs_Overhaul_Enabled));
            cbSaver(cboxItemEmbalmOverhaul, nameof(config.Item_Embalm_Overhaul_Enabled));
            cbSaver(cboxZFSeedsNeed, nameof(config.Craft_ZombieFarm_SeedsNeed_Enabled));
            cbSaver(cboxZFProduceCropWaste, nameof(config.Craft_ZombieFarm_ProduceWaste_Enabled));
            cbSaver(cbInGameRerun, nameof(config.InGame_ReloadConfig_Rerun));
            // Prayer
            // cbPrayerResetEnabled.Checked = config.Prayer_Reset_Enabled;
            // cbPrayerResetEnabled.CheckedChanged += (o, args) =>
            // {
            //     config.Prayer_Reset_Enabled = ((CheckBox) o).Checked;
            //     save(config);
            // };
            //
            // cboxItemOrgansOverhaul.Checked = config.Item_Organs_Overhaul_Enabled;
            // cboxItemOrgansOverhaul.CheckedChanged += (o, args) =>
            // {
            //     config.Item_Organs_Overhaul_Enabled = ((CheckBox) o).Checked;
            // };
            //
            // cboxItemEmbalmOverhaul.Checked = config.Item_Embalm_Overhaul_Enabled;
            // cboxItemEmbalmOverhaul.CheckedChanged += (o, args) =>
            // {
            //     config.Item_Embalm_Overhaul_Enabled = ((CheckBox) o).Checked;
            // };
            //
            // cboxZFSeedsNeed.Checked = config.Craft_ZombieFarm_SeedsNeed_Enabled;
            // cboxZFSeedsNeed.CheckedChanged += (o, args) =>
            // {
            //     config.Craft_ZombieFarm_SeedsNeed_Enabled = ((CheckBox) o).Checked;
            // };
            //
            // cboxZFProduceCropWaste.Checked = config.Craft_ZombieFarm_ProduceWaste_Enabled;
            // cboxZFProduceCropWaste.CheckedChanged += (o, args) =>
            // {
            //     config.Craft_ZombieFarm_ProduceWaste_Enabled = ((CheckBox) o).Checked;
            // };
    
            var keySaver = new Action<
                TextBox,
                string>(
                (t, cfg) =>
                {
                    var prop = config.GetType()
                        .GetProperties()
                        .FirstOrDefault(f =>
                    {
                        return f.Name == cfg;
                    });
                    t.Text = prop.GetValue(config).ToString();
                    t.TextChanged += (o, args) =>
                    {
                        prop.SetValue(config, (o as TextBox).Text);
                        save(config);
                    };
                });
    
            // Key
            keySaver(tboxBuffKey, nameof(config.Buffs_Activator_Key));
            keySaver(tboxPrayerKey, nameof(config.Prayer_Key));
            keySaver(tboxReloadKey, nameof(config.InGame_ReloadConfig_Key));
            keySaver(tboxItemSpawnerKey, nameof(config.Item_Spawner_Key));
            keySaver(tboxDropOrganPackageKey, nameof(config.Item_Spawner_Package_Key));
    
            // Debug
            cbDebugLog.Checked = config.Debug_Enabled;
            cbDebugLog.CheckedChanged += (o, args) =>
            {
                config.Debug_Enabled = ((CheckBox) o).Checked;
                save(config);
            };
            Config = config;
        }
    
        private void linkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLbl.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.nexusmods.com/graveyardkeeper/mods/24");
        }
    }
    
    public class UIControl
    {
        public UIControl(object propsName, NumericUpDown ctrBox, Label ctrLabel)
        {
            this.propsName = propsName;
            this.ctrBox = ctrBox;
            this.ctrLabel = ctrLabel;
        }
    
        public object propsName { get; set; }
        public NumericUpDown ctrBox { get; set; }
        public Label ctrLabel { get; set; }
    }
    
    public class Buffs
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    
        public Buffs(string id, string title, string description)
        {
            this.Id = id;
            this.Title = title;
            this.Desc = description;
        }
    }
    
    public static class GuiExtensionMethods
    {
        public static void Enable(this Control con, bool enable)
        {
            if (con != null)
            {
                if (enable)
                {
                    Control original = con;
    
                    List<Control> parents = new List<Control>();
                    do
                    {
                        parents.Add(con);
    
                        if (con.Parent != null)
                            con = con.Parent;
                    } while (con.Parent != null && con.Parent.Enabled == false);
    
                    if (con.Enabled == false)
                        parents.Add(con); // added last control without parent
    
                    for (int x = parents.Count - 1; x >= 0; x--)
                    {
                        parents[x].Enabled = enable;
                    }
    
                    con = original;
                    parents = null;
                }
    
                foreach (Control c in con.Controls)
                {
                    c.Enable(enable);
                }
    
                try
                {
                    con.Invoke((MethodInvoker)(() => con.Enabled = enable));
                }
                catch
                {
                }
            }
        }
    }
}
