using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private int amountOfStats;
    [SerializeField] private GameObject[] WeaponType = new GameObject[4];

    private GameObject weapon;


    public GameObject SpawnWeapon()
    {
        if (weapon != null)
        {
            Destroy(weapon);
        }

        GameObject randomWeapon = WeaponType[Random.Range(0, WeaponType.Length)];
        weapon = Instantiate(randomWeapon);
        Weapon weaponComponent = weapon.GetComponent<Weapon>();

        int rang = 0;
        int pontsAmount = 4 + rang * 2;

        for (int i = 0; i < pontsAmount; i++)
        {
            switch (Random.Range(0, amountOfStats))
            {
                case 0:
                    //weaponComponent.weaponStats.damage *= 1.5f;
                    //Debug.Log("damage: " + weaponComponent.weaponStats.damage);
                    break;
                case 1:
                    weaponComponent.weaponStats.fireRate /= 1.5f;
                    Debug.Log("firerate: " + weaponComponent.weaponStats.fireRate);
                    break;
                case 2:
                    weaponComponent.weaponStats.steamCost /= 1.5f;
                    Debug.Log("steamcost: " + weaponComponent.weaponStats.steamCost);
                    break;
                case 3:
                    weaponComponent.weaponStats.heatGain /= 1.5f;
                    Debug.Log("heatgain: " + weaponComponent.weaponStats.heatGain);
                    break;
                case 4:
                    Debug.Log("adadaa");
                    break;
            }
        }

        weapon.transform.position = transform.position;

        weaponComponent.weaponHandler.GetComponent<WeaponHandler>().OnAddWeapon += WeaponDetach;

        return weapon;
    }

    private void WeaponDetach()
    {
        weapon = null;
    }
}

