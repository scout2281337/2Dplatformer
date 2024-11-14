using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPickUp : MonoBehaviour
{
    public float steamAmount;

    private void Update()
    {
        DecreaseSteamAmount();
    }

    private void DecreaseSteamAmount()
    {
        if (steamAmount < 0)
            Destroy(gameObject);

        steamAmount -= Time.deltaTime * 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCombat>(out PlayerCombat playerCombat))
        {
            playerCombat.RestoreSteam(steamAmount); 
            Destroy(gameObject);
        }
    }
}
