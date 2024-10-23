using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPanelManager : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public Sprite noWeapon;
    private UIDocument _document;
    private Slot[] _slots = new Slot[3];
    private ProgressBar _progressBar;


    private void Start()
    {
        _document = GetComponent<UIDocument>();

        _slots[0] = new Slot();
        _slots[1] = new Slot();
        _slots[2] = new Slot();

        _slots[0].SetSlot(_document.rootVisualElement.Q<VisualElement>("Slot0"));
        _slots[1].SetSlot(_document.rootVisualElement.Q<VisualElement>("Slot1"));
        _slots[2].SetSlot(_document.rootVisualElement.Q<VisualElement>("Slot2"));

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

    #region Weapon - slot linking
    private void AddWeapon(int index, GameObject weapon)
    {
        playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponJam += _slots[index].JamSlot;
        playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponUnJam += _slots[index].UnJamSlot;

        //playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponUnJam += Deac;
        UpdateSlotSprite(index, weapon.GetComponent<Weapon>().spriteRenderer.GetComponent<SpriteRenderer>().sprite);
    }

    private void DropWeapon(int index, GameObject weapon)
    {
        DeleteSlotSprite(index);
    }
    #endregion

    #region Slots sprite handling
    void UpdateSlotSprite(int slotIndex, Sprite newSprite)
    {
        _slots[slotIndex].UpdateSlotSprite( newSprite);
    }

    void DeleteSlotSprite(int slotIndex)
    {
        _slots[slotIndex].UpdateSlotSprite(noWeapon);
    }
    #endregion

    #region Color indication
    void EquipWeapon(int index)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i == index)
            {
                _slots[i].ActivateSlot();
            }
            else
            {
                _slots[i].DeactivateSlot();
            }
        }
    }
    #endregion
}

public class Slot
{
    private VisualElement _visualElement;
    private bool isActive = false;
    private bool isJamed = false;

    public void SetSlot(VisualElement visualElement)
    {
        _visualElement = visualElement;
    }

    public void UpdateSlotSprite(Sprite newSprite)
    {
        _visualElement.style.backgroundImage = new StyleBackground(newSprite.texture);
    }

    #region Slot Color Indication
    public void ActivateSlot()
    {
        isActive = true;

        if (isJamed) return;

        _visualElement.style.backgroundColor = Color.white;
    }

    public void DeactivateSlot()
    {
        isActive = false;

        if (isJamed) return;

        _visualElement.style.backgroundColor = Color.gray;
    }

    public void JamSlot()
    {
        isJamed = true;

        _visualElement.style.backgroundColor = Color.red;
    }

    public void UnJamSlot()
    {
        isJamed = false;

        if (isActive)
        {
            ActivateSlot();
        }
        else
        {
            DeactivateSlot();
        }
    }
    #endregion
}
