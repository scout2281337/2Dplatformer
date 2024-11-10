using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public WeaponStats_SO weaponStats;
    public float currentHeat;

    protected float lastTimeShot;
    private const float maxHeat = 100f;
    private const float jamTime = 2f;
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

    public virtual bool WeaponAttack(Vector2 diraction, PlayerMovement playerMovement)
    {
        if (isJamed)
            return false;
        if (Time.time < lastTimeShot + weaponStats.fireRate)
            return false;
        
        weaponStats.shotTypeComponent.Shoot(diraction, transform.position, weaponStats, playerMovement);
        lastTimeShot = Time.time;
        AddHeat();

        return true;
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
        currentHeat += weaponStats.heatGain;
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
        Invoke(nameof(UnJamWeapon), jamTime);

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
