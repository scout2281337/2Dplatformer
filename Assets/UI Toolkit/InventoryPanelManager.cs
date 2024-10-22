using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
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

        _progressBar = _document.rootVisualElement.Q<ProgressBar>();

        playerCombat.OnWeaponEquip += EquipWeapon;
        playerCombat.OnWeaponAdd += AddWeapon;
        playerCombat.OnWeaponDrop += DropWeapon;
    }

    private void Update()
    {

        _progressBar.value = (playerCombat.steamCurrent / playerCombat.steamMax) * 100;
        _progressBar.title = Mathf.RoundToInt(playerCombat.steamCurrent).ToString();
    }

    private void AddWeapon(int index, GameObject weapon)
    {
        playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponJam += JamSlot;
        //playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponUnJam += Deac;
        UpdateSlotSprite(index, weapon.GetComponent<Weapon>().spriteRenderer.GetComponent<SpriteRenderer>().sprite);
    }

    private void DropWeapon(int index, GameObject weapon)
    {
        
        DeleteSlotSprite(index);
    }

    #region Sprite handling
    void UpdateSlotSprite(int slotIndex, Sprite newSprite)
    {
        _slots[slotIndex].style.backgroundImage = new StyleBackground(newSprite.texture);
    }

    void DeleteSlotSprite(int slotIndex)
    {
        UpdateSlotSprite(slotIndex, noWeapon);
    }
    #endregion

    #region Current slot indication
    void EquipWeapon(int index)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i == index)
            {
                ActivateSlot(i);
            }
            else
            {
                DeactivateSlot(i);
            }
        }
    }

    void ActivateSlot(int index)
    {
        _slots[index].style.backgroundColor = Color.white;

    }

    void DeactivateSlot(int index)
    {
        _slots[index].style.backgroundColor = Color.black;
    }

    void JamSlot()
    {
        _slots[playerCombat.currentWeaponIndex].style.backgroundColor = Color.red;
    }

    void UnJamSlot()
    {

    }
    #endregion

}
