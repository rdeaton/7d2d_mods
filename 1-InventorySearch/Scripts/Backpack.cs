using System;
using System.Xml;
using UnityEngine;

public class XUiC_mirashii_BackpackWindow : XUiC_BackpackWindow
{
    // TODO: Disable handlers/reset colors on window close?

    private XUiC_TextInput txtInput;
    private XUiC_Backpack backpackGrid;

    private static Color32 highlight_color = new Color32(255, 0, 0, 255);
    // TODO: Maybe don't hardcode this
    private static Color32 normal_color = new Color32(96, 96, 96, 255);

    public override void Init()
    {
        base.Init();
        this.backpackGrid = base.GetChildByType<XUiC_Backpack>();
        this.txtInput = (XUiC_TextInput)this.windowGroup.Controller.GetChildById("searchInput");
        if (this.txtInput != null) {
            this.txtInput.OnChangeHandler += this.HandleOnChangeHandler;
            this.txtInput.OnSubmitHandler += this.HandleOnSubmitHandler;
        }  
    }

    private void highlight(string _text) {
        int i = 0;
        XUiController[] controllers = this.backpackGrid.GetItemStackControllers();
        ItemStack[] backpack_stacks = this.backpackGrid.GetSlots();
        // Debug.Log("# controllers:" + controllers.Length);
        // Debug.Log("# backpack_stacks:" + backpack_stacks.Length);
        while (i < controllers.Length) {
            // is_ = backpack_stacks[i];
            XUiC_ItemStack controller = (XUiC_ItemStack) controllers[i];
            if (controller.ItemStack.itemValue.ItemClass != null) {
                String name = controller.ItemStack.itemValue.ItemClass.GetLocalizedItemName().ToLower();
                if (_text.Length > 0 && name.Contains(_text.ToLower())) {
                    controller.backgroundColor = XUiC_mirashii_BackpackWindow.highlight_color;
                } else {
                    controller.backgroundColor = XUiC_mirashii_BackpackWindow.normal_color;
                }
            }
            i += 1;
        }

    }

    private void HandleOnChangeHandler(XUiController _sender, string _text, bool _changeFromCode) {
        this.highlight(_text);
    }

    private void HandleOnSubmitHandler(XUiController _sender, string _text)
    {
        this.highlight(_text);
    }

}