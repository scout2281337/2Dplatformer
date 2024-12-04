using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponSheets : MonoBehaviour
{
    [SerializeField] private UIDocument[] documents;
    private WeaponSheet focusedWeaponSheet = new WeaponSheet();
    private WeaponSheet currentWeaponSheet = new WeaponSheet();
    private WeaponSheet[] weaponSheets = new WeaponSheet[3];
    
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            weaponSheets[i] = new WeaponSheet();
            weaponSheets[i].InitWeaponSheet(documents[i]);
        }

        focusedWeaponSheet.InitWeaponSheet(documents[3]);
        focusedWeaponSheet.frame.style.left = 600;

        currentWeaponSheet.InitWeaponSheet(documents[4]);
        currentWeaponSheet.frame.style.left = -600;

        PlayerManager.Instance.playerInteraction.OnFocusInteractable += OpenFocusedWeaponSheets;
        PlayerManager.Instance.playerInteraction.OnUnFocusInteractable += CloseWeaponSheets;
        PlayerManager.Instance.playerCombat.OnWeaponAdd += AddNewWeaponSheet;
        PlayerManager.Instance.playerCombat.OnWeaponEquip += SwitchCurrentWeaponSheet;

        CloseWeaponSheets();
    }

    public void OpenFocusedWeaponSheets(Interactable interactable)
    {
        if (interactable.GetType() != typeof(WeaponHandler))
            return;

        focusedWeaponSheet.SetWeaponSheet(interactable);
        focusedWeaponSheet.OpenWeaponSheet();

        currentWeaponSheet.OpenWeaponSheet();
    }

    public void CloseWeaponSheets()
    {
        focusedWeaponSheet.CloseWeaponSheet();
        currentWeaponSheet.CloseWeaponSheet();
    }

    private void AddNewWeaponSheet(int index, GameObject weaponObject)
    {
        WeaponHandler weaponHandler = weaponObject.GetComponent<Weapon>().weaponHandler.GetComponent<WeaponHandler>();
        weaponSheets[index].SetWeaponSheet(weaponHandler);
        weaponSheets[index].weaponObject = weaponHandler;
        Debug.Log(weaponSheets[index].weaponObject);

        SwitchCurrentWeaponSheet(index);
    }

    private void SwitchCurrentWeaponSheet(int index)
    {
        if (weaponSheets[index].weaponObject == null)
            return;

        currentWeaponSheet.SetWeaponSheet(weaponSheets[index].weaponObject);
    }
}

public class WeaponSheet
{
    public bool isEnabled = false;

    public Interactable weaponObject;

    public UIDocument document;
    public VisualElement frame;
    public Label weaponName;
    public VisualElement icon;
    public Label mainStats;
    public Label shotType;
    public Label onFire;
    public Label onActive;
    public Label onImpact;

    public void InitWeaponSheet(UIDocument newDocument)
    {
        document = newDocument;
        frame = document.rootVisualElement.Q<VisualElement>("Frame");
        icon = document.rootVisualElement.Q<VisualElement>("Icon");
        weaponName = document.rootVisualElement.Q<Label>("Name");
        mainStats = document.rootVisualElement.Q<Label>("MainStats");
        shotType = document.rootVisualElement.Q<Label>("ShotType");
        onFire = document.rootVisualElement.Q<Label>("OnFire");
        onActive = document.rootVisualElement.Q<Label>("OnActive");
        onImpact = document.rootVisualElement.Q<Label>("OnImpact");

        CloseWeaponSheet();
    }

    public void SetWeaponSheet(Interactable interactable)
    {
        if (!interactable.gameObject.TryGetComponent<WeaponHandler>(out WeaponHandler weaponHandler))
            return;

        Weapon weapon = weaponHandler.weapon;
        WeaponStats_SO weaponStats = weapon.weaponStats;

        icon.style.backgroundImage = new StyleBackground(weapon.spriteRenderer.GetComponent<SpriteRenderer>().sprite);
        weaponName.text = weaponStats.weaponName;
        mainStats.text = "Shot Type: ";
        shotType.text = weaponStats.shotTypeComponent.GetDesription();
        onFire.text = "On Fire:";
        foreach (BaseFireComponent component in weaponStats.fireComponents)
        {
            onFire.text += component.GetDesription();
        }
        onActive.text = "On Active: ";
        foreach (BaseActiveComponent component in weaponStats.activeComponents)
        {
            onActive.text += component.GetDesription();
        }
        onImpact.text = "On Impact: ";
        foreach (BaseImpactComponent component in weaponStats.impactComponents)
        {
            onImpact.text += component.GetDesription();
        }
    }

    public void OpenWeaponSheet()
    {
        frame.SetEnabled(true);
        isEnabled = true;
    }

    public void CloseWeaponSheet()
    {
        frame.SetEnabled(false);
        isEnabled = false;
    }
}
