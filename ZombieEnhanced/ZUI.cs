// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Harmony;
// using UnityEngine;
//
// namespace ZombieEnhanced
// {
//     [HarmonyPatch(typeof(GUIElements))]
//     [HarmonyPatch("Init")]
//     internal class PatchInit
//     {
//         private ZUI zui;
//         [HarmonyPostfix]
//         public static void Postfix()
//         {
//             ZUI.me();
//         } 
//     }
//     public class ZUI : GUIElements
//     {
//         public int height_with_price = 144;
//         public int height_without_price = 116;
//         public UILabel header_label;
//         private DialogButtonsGUI _dialog_buttons;
//         private BaseItemCellGUI _item_gui;
//         private SmartSlider _slider;
//         private System.Action<int> _on_confirm;
//         public UILabel price;
//         public UIWidget window;
//         private ItemCountGUI.PriceCalculateDelegate _price_calculate_delegate;
//         private Dictionary<string, string> _buffs;
//         private static readonly Lazy<ZUI> _instance = new Lazy<ZUI>(() => gameObject.AddComponent<ZUI>());
//         public static IniConfig me => _instance.Value;
//         public void Init()
//         {
//             this._dialog_buttons = this.GetComponentInChildren<DialogButtonsGUI>(true);
//             this._dialog_buttons.Init();
//             // this._item_gui = this.GetComponentInChildren<BaseItemCellGUI>();
//             this._slider = this.GetComponentInChildren<SmartSlider>(true);
//             this._slider.Init();
//             base.Init();
//             _buffs.Add("1", "Difficult Corpses");
//         }
//
//         public void Open(
//             // string item_id,
//             // System.Action<int> on_confirm,
//             int slider_step_for_keyboard = 1,
//             ItemCountGUI.PriceCalculateDelegate price_calculate_delegate = null)
//         {
//             Open();
//             // bool flag = price_calculate_delegate != null;
//             // _item_gui.DrawItem(item_id, 1, true, false);
//             // _item_gui.interaction_enabled = false;
//             header_label.text = _buffs.ElementAt(_slider.value).Value;
//             window.height = height_with_price;
//             _slider.Open(1, 1, _buffs.Count, 
//                 (System.Action<int>) (n => RedrawBuff()), 
//                 true, 
//                 true,
//                 1);
//             // _on_confirm = on_confirm;
//             _dialog_buttons.Set(
//                 "ok", 
//                 new GJCommons.VoidDelegate(OnConfirm),
//                 !BaseGUI.for_gamepad ? (string) null : "back",
//                 (GJCommons.VoidDelegate) (() => OnPressedBack()), 
//                 (string) null, 
//                 (GJCommons.VoidDelegate) null,
//                 GameKey.Select, GameKey.Back);
//             RedrawBuff();
//         }
//
//         private void RedrawBuff()
//         {
//             this.price.text = _buffs.ElementAt(_slider.value).Key;
//         }
//
//         private void OnConfirm()
//         {
//             BuffsLogics.AddBuff(_buffs.ElementAt(_slider.value).Key);
//             // if (this._on_confirm != null) {
//             //     this._on_confirm(this._slider.value);
//             //     BuffsLogics.AddBuff(_buffs.ElementAt(_slider.value).Key);
//             // }
//             this.Hide(true);
//         }
//
//         protected override bool OnPressedBack()
//         {
//             this.OnClosePressed();
//             return true;
//         }
//
//         public delegate float PriceCalculateDelegate(int amount);
//     }
// }