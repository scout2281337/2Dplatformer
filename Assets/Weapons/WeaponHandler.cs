using TMPro;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    private void OnEnable()
    {
        SetWeaponHandler(transform.parent.gameObject);
    }


    public void SetWeaponHandler(GameObject Weapon)
    {
        string name = Weapon.GetComponent<Weapon>().WeaponName;
        textMeshPro.text = name;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCombat playercombat = collision.GetComponent<PlayerCombat>();
        if (playercombat != null)
        {
            transform.parent.GetComponent<Weapon>().WeaponPickUp(collision);
        }
    }
}
