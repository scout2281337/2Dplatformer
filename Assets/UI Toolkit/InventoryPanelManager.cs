using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class InventoryPanelManager : MonoBehaviour
{
    [SerializeField] private Sprite noWeapon;

    private PlayerCombat playerCombat;
    private UIDocument document;
    private Slot[] slots = new Slot[3];
    private ProgressBar progressBar;

    [Inject] 
    public void Construct(PlayerCombat playerCombat) 
    {
        this.playerCombat = playerCombat;
    
    }

    private void Start()
    {
        document = GetComponent<UIDocument>();

        slots[0] = new Slot();
        slots[1] = new Slot();
        slots[2] = new Slot();

        slots[0].SetSlot(document.rootVisualElement.Q<VisualElement>("Slot0"));
        slots[1].SetSlot(document.rootVisualElement.Q<VisualElement>("Slot1"));
        slots[2].SetSlot(document.rootVisualElement.Q<VisualElement>("Slot2"));

        progressBar = document.rootVisualElement.Q<ProgressBar>();

        //playerCombat = PlayerManager.Instance.playerCombat;

        playerCombat.OnWeaponEquip += EquipWeapon;
        playerCombat.OnWeaponAdd += AddWeapon;
        playerCombat.OnWeaponDrop += DropWeapon;
    }

    private void Update()
    {
        progressBar.value = (playerCombat.steamCurrent / playerCombat.steamMax) * 100;
        progressBar.title = Mathf.RoundToInt(playerCombat.steamCurrent).ToString();
    }

    #region Weapon - slot linking
    private void AddWeapon(int index, GameObject weapon)
    {
        playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponJam += slots[index].JamSlot;
        playerCombat.weaponInventory[index].GetComponent<Weapon>().OnWeaponUnJam += slots[index].UnJamSlot;

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
        slots[slotIndex].UpdateSlotSprite(newSprite);
    }

    void DeleteSlotSprite(int slotIndex)
    {
        slots[slotIndex].UpdateSlotSprite(noWeapon);
    }
    #endregion

    #region Color indication
    void EquipWeapon(int index)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == index)
            {
                slots[i].ActivateSlot();
            }
            else
            {
                slots[i].DeactivateSlot();
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
