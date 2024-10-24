using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public string WeaponName;
    public float damage;
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectileType;
    protected float lastTimeShot;

    public float steamCost;

    public float heatGain;
    public float currentHeat;
    private float maxHeat = 100;
    private float jamTime = 2f;
    private bool isJamed = false;
    public event Action OnWeaponJam;
    public event Action OnWeaponUnJam;

    [Header("Modules")]
    public GameObject weaponHandler;
    public GameObject spriteRenderer;


    private void Update()
    {
        DecreaseHeat(100 * Time.deltaTime);
    }

    public virtual bool WeaponAttack(Vector2 diraction, GameObject player)
    {
        if (!isJamed && Time.time > lastTimeShot + fireRate)
        {
            lastTimeShot = Time.time;

            AddHeat();

            return true;
        }
        else return false;

    }

    public void DropWeapon()
    {
        transform.parent = null; 
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        weaponHandler.SetActive(true);
    }

    public void ActivateWeapon()
    {
        spriteRenderer.SetActive(true);
    }

    public void DeactivateWeapon()
    {
        spriteRenderer.SetActive(false);
    }

    #region Heat Handling
    private void AddHeat()
    {
        currentHeat += heatGain;
        if (currentHeat > maxHeat) JamWeapon();
    }

    public void DecreaseHeat(float heatDecrease)
    {
        if (currentHeat <= 0) return;

        currentHeat -= heatDecrease;
        currentHeat = Mathf.Clamp(currentHeat, 0, 100);

    }

    private void JamWeapon()
    {
        isJamed = true;
        Invoke("UnJamWeapon", jamTime);

        Debug.Log("jam");
        OnWeaponJam();
    }

    private void UnJamWeapon()
    {
        isJamed = false;

        Debug.Log("unjam");
        OnWeaponUnJam();
    }
    #endregion
}
