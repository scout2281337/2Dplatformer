using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerCombat : SoundManager
{
    //public Animator animator;
    //public Transform AttackPoint;
    //public LayerMask enemyLayer;

    //public float AttackRange;
    //public int attackDamage = 20;

    [Header("Aiming")]
    public Camera cam;
    public GameObject hand;
    private Vector3 mousePos;
    private Vector3 directionVector;

    [Header("Weapon")]
    private int currentWeaponIndex = 0;
    public GameObject[] weaponInventory = new GameObject[3];  // Array to hold weapons


    void Update()
    {
        AimAtMouse();

        if (Input.GetMouseButton(0))
        {
            UseWeapon();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropCurrentWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);  // Equip weapon 1
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);  // Equip weapon 2
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);  // Equip weapon 3
        }
    }

    private void AimAtMouse()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        directionVector = (mousePos - hand.transform.position).normalized;
        float rotZ = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void UseWeapon()
    {
        if (weaponInventory[currentWeaponIndex] != null) //Check for weapon, because player might not have any
        {
            weaponInventory[currentWeaponIndex].GetComponent<Weapon>().WeaponAttack(directionVector, gameObject);
        }
    }

    private void DropCurrentWeapon()
    {
        if (weaponInventory[currentWeaponIndex] != null) // Check for weapon, because player might not have any
        {
            weaponInventory[currentWeaponIndex].GetComponent<Weapon>().DropWeapon();
            weaponInventory[currentWeaponIndex] = null;

            // Equips first avalable weapon
            for (int i = weaponInventory.Length - 1; i > - 1; i--)
            {
                EquipWeapon(i);
            }
        }
    }

    //Adds weapon to free weaponInventory slot
    public bool AddWeapon(GameObject newWeapon)
    {
        for (int i = 0; i < weaponInventory.Length; i++)
        {
            if (weaponInventory[i] == null)  // Found an empty slot
            {
                weaponInventory[i] = newWeapon;

                // Assign the weapon's parent to the hand
                weaponInventory[i].transform.parent = hand.transform;

                // Set the default position and rotation for the weapon in hand
                weaponInventory[i].transform.localPosition = new Vector3(1, 0, 0);
                weaponInventory[i].transform.localRotation = Quaternion.Euler(0, 0, 0);

                // Equips added weapon, so that player could instantly use it
                EquipWeapon(i);

                return true;  // Successfully added the weapon
            }
        }

        // Return false if no empty slot was found after checking all slots
        return false;
    }

    // Equips a weapon at index if its avalable
    private void EquipWeapon(int index)
    {
        // Ensure the requested weapon is valid before continuing
        if (weaponInventory[index] == null)
        {
            Debug.Log("Weapon not available in the specified slot.");
            return;
        }

        // Loop through all weapons and activate the one at the given index, disable others
        for (int i = 0; i < weaponInventory.Length; i++)
        {
            // Checks if this slot has any weapon, player might not have all slots taken
            if (weaponInventory[i] == null) continue;

            if (i == index)
            {
                // Activate the selected weapon
                weaponInventory[i].GetComponent<Weapon>().ActivateWeapon();
                currentWeaponIndex = index;
            }
            else
            {
                // Deactivate other weapons
                weaponInventory[i].GetComponent<Weapon>().DeactivateWeapon();
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (AttackPoint == null)
    //        return;
        
    //    Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    //}

    //private void Attack()
    //{
    //    animator.SetTrigger("attack");
    //    PlaySound(sounds[0]);

    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayer);

    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        // Проверяем, что коллайдер не является триггером
    //        if (!enemy.isTrigger)
    //        {
    //            Debug.Log(enemy.name);
    //            EnemyHealth enemyComponent = enemy.GetComponent<EnemyHealth>();

    //            if (enemyComponent != null)
    //            {
    //                enemyComponent.TakeDamage(attackDamage);
    //            }
    //            else
    //            {
    //                Debug.Log("нет врагов");
    //            }
    //        }
    //    }
    //}
}
