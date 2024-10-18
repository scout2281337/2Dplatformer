using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPanelManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Sprite noWeapon;
    private UIDocument _document;
    private VisualElement[] _slots = new VisualElement[3];
    private ProgressBar _progressBar;


    private void Start()
    {
        _document = GetComponent<UIDocument>();

        _slots[0] = _document.rootVisualElement.Q<VisualElement>("Slot0");
        _slots[1] = _document.rootVisualElement.Q<VisualElement>("Slot1");
        _slots[2] = _document.rootVisualElement.Q<VisualElement>("Slot2");


        //// Query all Label elements and convert to a list
        //var slotList = _document.rootVisualElement.Q<VisualElement>("Slot0");

        //// Convert the list to an array
        //_slots = slotList.ToArray();

        _progressBar = _document.rootVisualElement.Q<ProgressBar>();
    }

    private void Update()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (playerCombat.weaponInventory[i] != null)
            {
                UpdateSlotSprite(i, playerCombat.weaponInventory[i].GetComponent<Weapon>().spriteRenderer.GetComponent<SpriteRenderer>().sprite);
            }
            else
            {
                UpdateSlotSprite(i, noWeapon);
            }
        }

        switch (playerCombat.currentWeaponIndex)
        {
            case 0:
                EquipWeapon(0);  // Equip weapon 1

                UnEquipWeapon(1);
                UnEquipWeapon(2);
                break;

            case 1:
                EquipWeapon(1);  // Equip weapon 2

                UnEquipWeapon(0);
                UnEquipWeapon(2);
                break;

            case 2:
                EquipWeapon(2);  // Equip weapon 3

                UnEquipWeapon(0);
                UnEquipWeapon(1);
                break;
        }

        _progressBar.value = (playerCombat.steamCurrent / playerCombat.steamMax) * 100;
        _progressBar.title = Mathf.RoundToInt(playerCombat.steamCurrent).ToString();
    }

    void UpdateSlotSprite(int slotIndex, Sprite newSprite)
    {
        _slots[slotIndex].style.backgroundImage = new StyleBackground(newSprite.texture);
    }

    void EquipWeapon(int index)
    {
        _slots[index].style.backgroundColor = Color.white;
    }

    void UnEquipWeapon(int index)
    {
        _slots[index].style.backgroundColor = Color.black;
    }
}
