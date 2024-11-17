using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void Push(Vector2 forceVector, float forceStrength);
}

public interface IDamageable
{
    void TakeDamage(float damage);
}

