using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPanelManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    private UIDocument _document;
    private Label[] _slots = new Label[3];
    private ProgressBar _progressBar;


    private void Start()
    {
        _document = GetComponent<UIDocument>();

        // Query all Label elements and convert to a list
        var buttonList = _document.rootVisualElement.Query<Label>().ToList();

        // Convert the list to an array
        _slots = buttonList.ToArray();

        _progressBar = _document.rootVisualElement.Q<ProgressBar>();
    }

    private void Update()
    {
        //for (int i = 0; i < _slots.Length; i++)
        //{
        //    if (playerCombat.weaponInventory[i] != null)
        //    {
        //        UpdateSlotText(i, playerCombat.weaponInventory[i].GetComponent<Weapon>().WeaponName);
        //    }
        //    else
        //    {
        //        UpdateSlotText(i, "-");
        //    }
        //}

        _progressBar.value = (playerCombat.currentSteam / playerCombat.maxSteam) * 100;
        _progressBar.title = Mathf.RoundToInt(playerCombat.currentSteam).ToString();
    }

    void UpdateSlotText(int slotIndex, string newText)
    {
        _slots[slotIndex].text = newText;
    }
}
