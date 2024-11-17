using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    public event Action<GameObject> OnCollisionEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.ToString());
        OnCollisionEnter?.Invoke(collision.gameObject);
    }
}
