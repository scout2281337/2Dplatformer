using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public int amountOfStats;
    public GameObject[] WeaponType = new GameObject[4];

    private GameObject weapon;


    public GameObject SpawnWeapon()
    {
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
                    weaponComponent.damage *= 1.5f;
                    Debug.Log("damage: " + weaponComponent.damage);
                    break;
                case 1:
                    weaponComponent.fireRate /= 1.5f;
                    Debug.Log("firerate: " + weaponComponent.fireRate);
                    break;
                case 2:
                    weaponComponent.steamCost /= 1.5f;
                    Debug.Log("steamcost: " + weaponComponent.steamCost);
                    break;
                case 3:
                    weaponComponent.heatGain /= 1.5f;
                    Debug.Log("heatgain: " + weaponComponent.heatGain);
                    break;
                case 4:
                    Debug.Log("adadaa");
                    break;
            }
        }

        weapon.transform.position = transform.position;
        return weapon;
    }
}

