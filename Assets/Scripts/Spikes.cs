using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int spikesAmount;
    [SerializeField] private float spikeDamage;
    [SerializeField] private float spikeForce;
    [SerializeField] private float tileScale;

    [SerializeField] private GameObject spikeSprite;
    [SerializeField] private GameObject spikeCollider; 

    
    private void Start()
    {
        for (int i = 1; i < spikesAmount; i++)
        {
            GameObject newSpike = Instantiate(spikeSprite, transform);
            newSpike.transform.localPosition = new Vector3(tileScale * i, 0, 0);

            spikeCollider.transform.localScale = new Vector3(tileScale * i + tileScale, 0.8f, 1);
            spikeCollider.transform.localPosition = new Vector3(tileScale / 2 * i, 0, 0);
        }

        if (!spikeCollider.TryGetComponent<SpikeCollider>(out var spikeColliderScript))
            return;

        spikeColliderScript.OnCollisionEnter += DealDamage;
    }

    private void DealDamage(GameObject collision)
    {
        if (!collision.TryGetComponent<PlayerHealth>(out var health))
            return;

        health.TakeDamage(spikeDamage);
    }
}
